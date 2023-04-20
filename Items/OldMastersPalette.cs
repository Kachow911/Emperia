using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Emperia.UI;
using Terraria.GameContent;
using static Terraria.Audio.SoundEngine;
using Terraria.ModLoader.IO;
using Emperia.Projectiles;

namespace Emperia.Items
{
    public class OldMastersPalette : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old Master's Palette");
            Tooltip.SetDefault("Allows ultimate control over paint!\nRight Click while holding to select paints and brush\n'The world is your canvas... literally!'");
            ItemID.Sets.SortingPriorityPainting[Item.type] = 101;
            //GamepadWholeScreenUseRange
            //GamepadExtraRange
        }
        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.width = 20;
            Item.height = 20;
            Item.useAnimation = 15;
            Item.useTime = 5;
            Item.useStyle = 1;
            Item.value = 10000;
            Item.rare = 2;
            Item.autoReuse = true;
            //Item.UseSound = SoundID.Item1;
            Item.tileBoost = 3;
            Item.noUseGraphic = true;
            Item.paint = 0;
        }

        public static Rectangle[] paintPosition = { new Rectangle(16, 14, 34, 30), new Rectangle(10, 16, 34, 30), new Rectangle(4, 14, 34, 30), new Rectangle(0, 6, 34, 30), new Rectangle(2, 0, 34, 30) };
        //public List<int> selectedColors = new List<int>();
        public List<int> selectedColors = new List<int> { 26, 3, 1, 5, 8 };
        public List<int> selectedColorsBackup = new List<int>();
        //public Item[] specialPaintSlots = { null, null, null };
        public int[] unlockedSpecialPaints = { 0, 0, 0 };
        public int brushMode = 0;
        public bool curatedMode = false;
        public int curatedColor = 0;
        public byte color;
        public string visualMode;
        public bool spectreUpgrade = false;

        public override void HoldItem(Player player)
        {
            //if (Player.cursorItemIconID != 0) PaintUISystem.cursorUIActive = false; is in ModPlayer.PreItemCheck

            /*for (int i = 29; i <= 31; ++i)
            {
                Item item = FindPaintItem(player, i);
                specialPaintSlots[i - 29] = item;
                if (item == null)
                {
                    if (selectedColors.Contains(i)) selectedColors.Remove(i);
                    if (curatedColor == i) curatedColor = 0;
                }
            }*/
            color = (byte)selectedColors.LastOrDefault();
            if (curatedMode) color = (byte)curatedColor;
            if (selectedColors.Any()) visualMode = "Blank";

            for (int i = 0; i < 251; ++i) //makes visual use sprite
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == ModContent.ProjectileType<OldMastersPaletteVisual>()) break;
                if (i == 250) Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<OldMastersPaletteVisual>(), 0, 0, Main.myPlayer, 0, 0);
            }
            player.GetModPlayer<MyPlayer>().noShieldSprite = true;
        }
        public override void UpdateInventory(Player player)
        {
            //paint sprayer compatability
            if (player.HeldItem.type == ItemID.Paintbrush || player.HeldItem.type == ItemID.SpectrePaintbrush || player.HeldItem.type == ItemID.PaintRoller || player.HeldItem.type == ItemID.SpectrePaintRoller) Item.paint = 0;
            else Item.paint = color;
        }

        public static void OverlayType_OldMastersPaletteBrush(Player player, ref Color lightColor, ref Texture2D overlayTexture, ref Vector2 position, ref Rectangle? rectangle, ref Color color, ref float rotation, ref Vector2 origin, ref float scale, ref SpriteEffects direction)
        {
            OldMastersPalette mastersPalette = player.HeldItem.ModItem as OldMastersPalette; //this can return an object reference not set to isntance of object error
            if (!mastersPalette.selectedColors.Any()) return;
            int paintType = mastersPalette.color;
            if (paintType == 0 || mastersPalette.brushMode == 2) return;

            overlayTexture = mastersPalette.GetPaintBlobTexture(paintType, 0, true);
            color = mastersPalette.PaintToColor(paintType);
            if (paintType != 31) color = color.MultiplyRGB(lightColor);
            rectangle = new Rectangle(8, 0, 26, 30);
            return;
        }
        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax) ItemSwingVisual.NewItemSwingVisual(player, Item, "Emperia/Items/Palette/OldMastersPalette_Brush", OverlayType_OldMastersPaletteBrush);

            if (Item.GetGlobalItem<GItem>().TileInRange(Item, player) == false) return false;
            if (player.itemAnimation % Item.useTime != 0 || !player.controlUseItem) return false;

            int i = Player.tileTargetX;
            int j = Player.tileTargetY;
            if (color != 0)
            {
                if (brushMode == 0)
                {
                    if (Framing.GetTileSafely(i, j).TileColor != color)
                    {
                        WorldGen.paintTile(i, j, color);
                        //TryConsumePaint(color, player);
                    }
                }
                if (brushMode == 1)
                {
                    if (Framing.GetTileSafely(i, j).WallColor != color)
                    {
                        WorldGen.paintWall(i, j, color);
                        //TryConsumePaint(color, player);
                    }
                }
            }
            if (brushMode == 2)
            {
                if (Framing.GetTileSafely(i, j).HasTile && Framing.GetTileSafely(i, j).TileColor != 0) WorldGen.paintTile(i, j, 0);
                else if (Framing.GetTileSafely(i, j).WallType != 0 && Framing.GetTileSafely(i, j).WallColor != 0) WorldGen.paintWall(i, j, 0);
            }
            return true;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            return false;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = ModContent.Request<Texture2D>("Emperia/Items/Palette/OldMastersPalette_Item" + visualMode, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            //position -= new Vector2(2, 0);
            spriteBatch.Draw(texture, position, null, drawColor, 0f, Vector2.Zero, Main.inventoryScale, SpriteEffects.None, 0f);

            if (selectedColors.Any())
            {
                int colorCount = CuratedColorList(selectedColors).Count;
                for (int i = 0; i < colorCount; i++)
                {
                    int paintType = GetPaletteVisualOrder(colorCount, i);
                    if (paintType == 0) break;
                    Texture2D paintTexture = GetPaintBlobTexture(paintType, i);
                    Color color = PaintToColor(paintType);

                    spriteBatch.Draw(paintTexture, position, (Rectangle)paintPosition.GetValue(i), color, 0f, Vector2.Zero, Main.inventoryScale, SpriteEffects.None, 0f);
                }
                {
                    int paintType = color;
                    if (paintType != 0)
                    {
                        Texture2D paintTexture = GetPaintBlobTexture(paintType, 0, true);
                        Color color = PaintToColor(paintType);

                        spriteBatch.Draw(paintTexture, position, null, color, 0f, Vector2.Zero, Main.inventoryScale, SpriteEffects.None, 0f);
                    }
                }
            }
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            return false;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>("Emperia/Items/Palette/OldMastersPalette_Item" + visualMode, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Vector2 position = new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f);
            spriteBatch.Draw(texture, position, null, lightColor, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);

            if (selectedColors.Any())
            {
                int colorCount = CuratedColorList(selectedColors).Count;
                for (int i = 0; i < colorCount; i++)
                {
                    int paintType = GetPaletteVisualOrder(colorCount, i);
                    if (paintType == 0) break;
                    Texture2D paintTexture = GetPaintBlobTexture(paintType, i);
                    Color color = PaintToColor(paintType);

                    if (paintType != 31) color = color.MultiplyRGB(lightColor);
                    spriteBatch.Draw(paintTexture, position, (Rectangle)paintPosition.GetValue(i), color, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                }
                {
                    int paintType = color;
                    if (paintType != 0)
                    {
                        Texture2D paintTexture = GetPaintBlobTexture(paintType, 0, true);
                        Color color = PaintToColor(paintType);

                        if (paintType != 31) color = color.MultiplyRGB(lightColor);
                        spriteBatch.Draw(paintTexture, position, null, color, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }
        public int GetPaletteVisualOrder(int colorCount, int i)
        {
            int curatedColorSlotOffset;
            int paintType = 0;
            if (colorCount > i)
            {
                curatedColorSlotOffset = CuratedColorList(selectedColors).IndexOf(curatedColor);
                int paletteColorOrder = i;
                if (curatedColorSlotOffset == -1 || !curatedMode) paintType = CuratedColorList(selectedColors)[paletteColorOrder];
                else
                {
                    paletteColorOrder = (paletteColorOrder + curatedColorSlotOffset) % colorCount;
                    paintType = CuratedColorList(selectedColors)[paletteColorOrder];
                }
            }
            return paintType;
        }
        public Texture2D GetPaintBlobTexture(int paintType, int i, bool forBrush = false)
        {
            int paintShape = 0;
            if (i == 2) paintShape = 1;
            if (forBrush) paintShape = 2;
            string specialType = SpecialVFX(paintType, true);
            return ModContent.Request<Texture2D>("Emperia/Items/Palette/OldMastersPalette_Paint" + paintShape + specialType).Value;
        }
        public string SpecialVFX(int paintType, bool noDeep = false)
        {
            if (!noDeep && (paintType >= 13 && paintType <= 24 || paintType == 29)) return "Deep";
            else if (paintType == 30) return "Neg";
            else if (paintType == 31) return "Illum";
            else return "";
        }
        public Color PaintToColor(int paintType, bool noPitchBlack = false)
        {
            if (paintType == 30) return Color.White;
            if (paintType == 29 && noPitchBlack) return WorldGen.paintColor(25);
            else return WorldGen.paintColor(paintType);
        }
        public List<int> CuratedColorList(List<int> selectedColors)
        {
            List<int> curatedColorList = new List<int>();
            if (selectedColors.Any())
            {
                if (selectedColors.Count() > 5)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        curatedColorList.Add(selectedColors[selectedColors.Count - 1 - (4 - i)]); //gets the last 5 colors
                        //Main.NewText("f", PaintToColor(selectedColors[selectedColors.Count - 1 - (4 - i)]));
                    }
                }
                else curatedColorList = selectedColors.ToList();
            }
            curatedColorList.Reverse();
            return curatedColorList;
        }
        /*internal Item FindPaintItem(Player player, int paintType)
        {
            Item item = null;
            for (int i = 54; i < 58; i++)
            {
                if (player.inventory[i].stack > 0 && player.inventory[i].paint == paintType && player.inventory[i].type != ModContent.ItemType<OldMastersPalette>())
                {
                    item = player.inventory[i];
                    return item;
                }
            }
            for (int j = 0; j < 58; j++)
            {
                if (player.inventory[j].stack > 0 && player.inventory[j].paint == paintType && player.inventory[j].type != ModContent.ItemType<OldMastersPalette>())
                {
                    item = player.inventory[j];
                    return item;
                }
            }
            return item;
        }*/
        internal List<Item> FindPaintToSacrifice(Player player, int paintType, ref bool fullStack)
        {
            List<Item> paintItems = new List<Item>();
            int paintCount = 0;
            for (int i = 54; i < 58; i++)
            {
                if (player.inventory[i].stack > 0 && player.inventory[i].paint == paintType && player.inventory[i].type != ModContent.ItemType<OldMastersPalette>())
                {
                    paintItems.Add(player.inventory[i]);
                    paintCount += player.inventory[i].stack;
                    if (paintCount >= 999)
                    {
                        fullStack = true;
                        return paintItems;
                    }
                }
            }
            for (int j = 0; j < 58; j++)
            {
                if (player.inventory[j].stack > 0 && player.inventory[j].paint == paintType && player.inventory[j].type != ModContent.ItemType<OldMastersPalette>())
                {
                    paintItems.Add(player.inventory[j]);
                    paintCount += player.inventory[j].stack;
                    if (paintCount >= 999)
                    {
                        fullStack = true;
                        return paintItems;
                    }
                }
            }
            return paintItems;
        }
        /*internal void TryConsumePaint(byte color, Player player)
        {
            if (color < 29) return;
            Item item = specialPaintSlots[color - 29];
            if (ItemLoader.ConsumeItem(item, player))
            {
                item.stack--;
            }
            if (item.stack <= 0)
            {
                item.SetDefaults();
            }
        }*/
        public static void SmartCursorLookup(Player player, Item item) //ayyyyy i can barely comprehend what some of this does and im shocked it works, thanks vanilla source and gadgetbox mod open source
        {
            OldMastersPalette mastersPalette = Main.LocalPlayer.HeldItem.ModItem as OldMastersPalette;

            if (mastersPalette.color == 0 && mastersPalette.brushMode != 2 || !Main.SmartCursorIsUsed) return;

            int reachableStartX = (int)(player.position.X / 16f) - Player.tileRangeX - item.tileBoost + 1;
            int reachableEndX = (int)((player.position.X + player.width) / 16f) + Player.tileRangeX + item.tileBoost - 1;
            int reachableStartY = (int)(player.position.Y / 16f) - Player.tileRangeY - item.tileBoost + 1;
            int reachableEndY = (int)((player.position.Y + player.height) / 16f) + Player.tileRangeY + item.tileBoost - 2;
            reachableStartX = Utils.Clamp(reachableStartX, 10, Main.maxTilesX - 10);
            reachableEndX = Utils.Clamp(reachableEndX, 10, Main.maxTilesX - 10);
            reachableStartY = Utils.Clamp(reachableStartY, 10, Main.maxTilesY - 10);
            reachableEndY = Utils.Clamp(reachableEndY, 10, Main.maxTilesY - 10);

            int smartX = -1;
            int smartY = -1;

            List<Tuple<int, int>> targets = new List<Tuple<int, int>>();

            for (int i = reachableStartX; i <= reachableEndX; i++)
            {
                for (int j = reachableStartY; j <= reachableEndY; j++)
                {
                    Tile tile = Main.tile[i, j];
                    bool tileIsSuitable = false;
                    switch (mastersPalette.brushMode)
                    {
                        case 0: if (tile.HasTile && tile.TileColor != mastersPalette.color) tileIsSuitable = true; break;
                        case 1: if (tile.WallType > 0 && tile.WallColor != mastersPalette.color && (!tile.HasTile || !Main.tileSolid[tile.TileType] || Main.tileSolidTop[tile.TileType])) tileIsSuitable = true; break;
                        case 2: if ((tile.HasTile && tile.TileColor > 0) || (tile.WallType > 0 && tile.WallColor > 0)) tileIsSuitable = true; break;
                    }
                    if (tileIsSuitable)
                    {
                        targets.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            if (targets.Count > 0)
            {
                float num = -1f;
                Tuple<int, int> tuple = targets[0];
                for (int k = 0; k < targets.Count; k++)
                {
                    float num2 = Vector2.Distance(new Vector2(targets[k].Item1, targets[k].Item2) * 16f + Vector2.One * 8f, Main.MouseWorld);
                    if (num == -1f || num2 < num)
                    {
                        num = num2;
                        tuple = targets[k];
                    }
                }
                if (Collision.InTileBounds(tuple.Item1, tuple.Item2, reachableStartX, reachableStartY, reachableEndX, reachableEndY))
                {
                    smartX = tuple.Item1;
                    smartY = tuple.Item2;
                }
            }
            targets.Clear();
            if (smartX != -1 && smartY != -1)
            {
                Main.SmartCursorX = (Player.tileTargetX = smartX);
                Main.SmartCursorY = (Player.tileTargetY = smartY);
                Main.SmartCursorShowing = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Paintbrush);
            recipe.AddIngredient(ItemID.PaintRoller);
            recipe.AddIngredient(ItemID.PaintScraper);
            /*for (int i = 0; i < 16; i++)
            {
                if (i < 12) recipe.AddIngredient(1073 + i, 400);
                else if (i < 15) recipe.AddIngredient(1097 + (i - 12), 400);
                else recipe.AddIngredient(1966, 400);
            }*/
            recipe.AddIngredient(ItemID.RedPaint, 999);
            recipe.AddIngredient(ItemID.OrangePaint, 999);
            recipe.AddIngredient(ItemID.YellowPaint, 999);
            recipe.AddIngredient(ItemID.GreenPaint, 999);
            recipe.AddIngredient(ItemID.BluePaint, 999);
            recipe.AddIngredient(ItemID.PurplePaint, 999);

            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
        public override bool ConsumeItem(Player player)
        {
            return false;
        }
        public override void SaveData(TagCompound tag)
        {
            tag["unlockedShadow"] = unlockedSpecialPaints[0] == 1;
            tag["unlockedNeg"] = unlockedSpecialPaints[1] == 1;
            tag["unlockedIllum"] = unlockedSpecialPaints[2] == 1;

            tag["selectedColors"] = selectedColors.ToArray();

            tag["spectreUpgrade"] = spectreUpgrade;
        }
        public override void LoadData(TagCompound tag)
        {
            if (tag.GetBool("unlockedShadow")) unlockedSpecialPaints[0] = 1;
            else unlockedSpecialPaints[0] = 0;
            if (tag.GetBool("unlockedNeg")) unlockedSpecialPaints[1] = 1;
            else unlockedSpecialPaints[1] = 0;
            if (tag.GetBool("unlockedIllum")) unlockedSpecialPaints[2] = 1;
            else unlockedSpecialPaints[2] = 0;

            selectedColors = tag.GetIntArray("selectedColors").ToList();

            color = (byte)selectedColors.LastOrDefault();
            Item.paint = color;
            visualMode = "Blank";

            spectreUpgrade = tag.GetBool("spectreUpgrade");
            if (spectreUpgrade)
            {
                Item.tileBoost += 2;
                Item.useAnimation = 12;
                Item.useTime = 4;
            }

        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (spectreUpgrade)
            {
                TooltipLine line = new TooltipLine(Mod, "Upgrade", "Spectral Paint Kit"); //no clue what the first string does here, gives the tooltip a name for other code to reference?
                //line.OverrideColor = new Color(95, 230, 255);
                line.OverrideColor = new Color(130, 255, 255);
                TooltipLine line2 = new TooltipLine(Mod, "UpgradeInfo", "Increased brush range and speed\nRight Click to detach");
                tooltips.Add(line);
                tooltips.Add(line2);
            }
        }
        public sealed override bool CanRightClick()
        {
            if (spectreUpgrade) return true;
            else return base.CanRightClick();
        }
        public override void RightClick(Player player)
        {
            if (spectreUpgrade)
            {
                spectreUpgrade = false;
                Item.tileBoost -= 2;
                Item.useAnimation = 15;
                Item.useTime = 5;
                Item.NewItem(player.GetSource_OpenItem(Item.type), player.getRect(), ModContent.ItemType<SpectrePaintKit>()); 
            }
        }
        /*public override float UseSpeedMultiplier(Player player)
        {
            if (spectreUpgrade) return 2.5f;
            return base.UseSpeedMultiplier(player);
        }*/
    }
    public class OldMastersPaletteVisual : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.damage = 0;
            Projectile.width = 30;
            Projectile.height = 26;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.HeldItem.type != ModContent.ItemType<OldMastersPalette>() || player.dead) Projectile.Kill();
            //player.heldProj = Projectile.whoAmI;

            Vector2 offset = new Vector2(10 + player.direction * 21, player.gravDir * 23);
            Projectile.rotation = 1.57f;
            if (player.gravDir == -1f)
            {
                offset.Y = 16;
                Projectile.rotation = 4.712f;
            }
            Projectile.rotation += player.fullRotation;
            float armRotation = -0.35f;
            int stretchAmount = 3;

            int bodyFrame = (player.bodyFrame.Y / player.bodyFrame.Height);
            Projectile.GetGlobalProjectile<GProj>().ApplyHeldProjOffsets(player, bodyFrame, ref offset, ref Projectile.rotation, ref armRotation, ref stretchAmount);

            player.SetCompositeArmBack(enabled: true, (Player.CompositeArmStretchAmount)stretchAmount, (float)Math.PI * armRotation * player.direction);


            //code beneath this adapted from vanilla medusa head projectile

            //if (velocity.X != base.velocity.X || velocity.Y != base.velocity.Y)
            //{
            //	this.netUpdate = true;
            //}
            
            Projectile.velocity = player.GetModPlayer<MyPlayer>().MouseDirection(); //no idea why this works, maybe OffsetsPlayerOnhand code checks projectile velocity to decide its direction?
            Vector2 rotationOffset = new Vector2(-10, -16);
            offset = (offset + rotationOffset).RotatedBy(player.fullRotation) - rotationOffset;
            Projectile.Center = player.position + offset;
            if (player.sleeping.isSleeping)
            {
                Vector2 posOffset;
                player.sleeping.GetSleepingOffsetInfo(player, out posOffset);
                Projectile.Center += posOffset;
            }
            Projectile.Center = (Projectile.Center - player.GetModPlayer<MyPlayer>().MouseDirection()).Floor();
            Projectile.gfxOffY = player.gfxOffY;
            Projectile.spriteDirection = player.direction;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            OldMastersPalette mastersPalette = Main.player[Projectile.owner].HeldItem.ModItem as OldMastersPalette; //this can return an object reference not set to isntance of object error
            Player player = Main.player[Projectile.owner];
            SpriteEffects direction = SpriteEffects.None;
            if (player.direction != player.gravDir) direction = SpriteEffects.FlipVertically; //more compact way of checking player direction and gravity direction at once

            //mastersPalette.CuratedColorList(mastersPalette.selectedColors);
            Texture2D texture = ModContent.Request<Texture2D>("Emperia/Items/Palette/OldMastersPalette_Palette" + mastersPalette.visualMode, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Vector2 position = new Vector2(Projectile.position.X - Main.screenPosition.X + texture.Width * 0.5f, Projectile.position.Y - Main.screenPosition.Y + Projectile.height - texture.Height * 0.5f + Projectile.gfxOffY + 2f); //not sure why 2f
            Main.EntitySpriteDraw(texture, position, null, lightColor, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, direction, 1);

            if (mastersPalette.selectedColors.Any())
            {
                Rectangle[] paintPosition = { new Rectangle(16, 18, 30, 26), new Rectangle(10, 20, 30, 26), new Rectangle(4, 18, 30, 26), new Rectangle(0, 10, 30, 26), new Rectangle(2, 4, 30, 26) };
                int colorCount = mastersPalette.CuratedColorList(mastersPalette.selectedColors).Count;
                for (int i = 0; i < colorCount; i++)
                {
                    int paintType = mastersPalette.GetPaletteVisualOrder(colorCount, i);
                    if (paintType == 0) break;
                    Texture2D paintTexture = mastersPalette.GetPaintBlobTexture(paintType, 0);
                    Color color = mastersPalette.PaintToColor(paintType);

                    if (paintType != 31) color = color.MultiplyRGB(lightColor);
                    Main.EntitySpriteDraw(paintTexture, position, (Rectangle)paintPosition.GetValue(i), color, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, direction, 1);
                }
            }
            return true;
        }
    }
    public class SpectrePaintKit : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spectral Paint Kit");
            Tooltip.SetDefault("Right Click while holding an Old Master's Palette to increase its range and speed");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = 8;
        }

        public virtual bool CanApply(Item Item)
        {
            if (Item.type == ModContent.ItemType<OldMastersPalette>() && (Item.ModItem as OldMastersPalette).spectreUpgrade == false)
            {
                return true;
            }
            else return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(lightColor.R, lightColor.G, lightColor.B, 80); //200 light, 10 alpha
        }
        public sealed override bool CanRightClick()
        {
            Item Item = Main.LocalPlayer.HeldItem;
            return CanApply(Item);
        }

        public override void RightClick(Player player)
        {
            Item Item = Main.LocalPlayer.HeldItem;
            (Item.ModItem as OldMastersPalette).spectreUpgrade = true;
            Item.tileBoost += 2;
            Item.useAnimation = 12;
            Item.useTime = 4;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SpectrePaintbrush);
            recipe.AddIngredient(ItemID.SpectrePaintRoller);
            recipe.AddIngredient(ItemID.SpectrePaintScraper);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}

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

namespace Emperia.Items
{
    public class OldMastersPalette : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old Master's Palette");
            Tooltip.SetDefault("Allows ultimate control over paint!\nRight Click while holding to select paints and brush\n'The world is your canvas... literally!'");
            //GamepadWholeScreenUseRange
            //GamepadExtraRange
        }
        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.width = 20;
            Item.height = 20;
            //Item.useTime = 10;
            //Item.useAnimation = 10;
            Item.useAnimation = 15;
            //Item.useAnimation = 60;
            Item.useTime = 5;
            Item.useStyle = 1;
            //Item.useStyle = 5;
            //Item.channel = true;
            Item.value = 10000;
            Item.rare = 2;
            Item.autoReuse = true;
            //Item.UseSound = SoundID.Item64;
            Item.tileBoost = 3;
            Item.noUseGraphic = true;
        }

        public static Rectangle[] paintPosition = { new Rectangle(16, 14, 34, 30), new Rectangle(10, 16, 34, 30), new Rectangle(4, 14, 34, 30), new Rectangle(0, 6, 34, 30), new Rectangle(2, 0, 34, 30) };
        public List<int> selectedColors = new List<int>();
        public int brushMode = 0;
        public bool curatedMode = false;
        public int curatedColor = 0;
        public byte color;
        public string visualMode;

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            return false;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            //CuratedColorList(selectedColors);
            Texture2D texture = ModContent.Request<Texture2D>("Emperia/Items/Palette/OldMastersPalette_Item" + visualMode, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            position -= new Vector2(2, 0);
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
            //CuratedColorList(selectedColors);
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

        public override void HoldItem(Player player)
        {
            if (player.cursorItemIconID == 0 && Item.GetGlobalItem<GItem>().TileInRange(Item, player)) EmperiaSystem.canStartDrawingCursorUI = true;
            //if (Player.cursorItemIconID != 0) EmperiaSystem.cursorUIActive = false; is in ModPlayer.PreItemCheck
            color = (byte)selectedColors.LastOrDefault();
            if (curatedMode) color = (byte)curatedColor;
            if (selectedColors.Any()) visualMode = "Blank";

            for (int i = 0; i < 251; ++i) //makes visual use sprite
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == ModContent.ProjectileType<OldMastersPaletteVisual>()) break;
                if (i == 250) Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<OldMastersPaletteVisual>(), 0, 0, Main.myPlayer, 0, 0);
            }
        }
        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax)
            {
                int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<OldMastersPaletteBrushVisual>(), 0, 0, Main.myPlayer, 0, 0);
                (Main.projectile[p].ModProjectile as OldMastersPaletteBrushVisual).useAnimationMax = Item.useAnimation;
                Main.projectile[p].timeLeft = Item.useAnimation;
            }

            if (Item.GetGlobalItem<GItem>().TileInRange(Item, player) == false) return false;
            if (player.itemAnimation % Item.useTime != 0 || !player.controlUseItem) return false;

            int i = Player.tileTargetX;
            int j = Player.tileTargetY;
            if (color != 0)
            {
                if (brushMode == 0)
                {
                    if (Framing.GetTileSafely(i, j).TileColor != color) WorldGen.paintTile(i, j, color);
                }
                if (brushMode == 1)
                {
                    if (Framing.GetTileSafely(i, j).WallColor != color) WorldGen.paintWall(i, j, color);
                }
            }
            if (brushMode == 2)
            {
                if (Framing.GetTileSafely(i, j).HasTile && Framing.GetTileSafely(i, j).TileColor != 0) WorldGen.paintTile(i, j, 0);
                else if (Framing.GetTileSafely(i, j).WallType != 0 && Framing.GetTileSafely(i, j).WallColor != 0) WorldGen.paintWall(i, j, 0);
            }
            return true;
        }
        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Seashell, 2);
            recipe.AddIngredient(ItemID.Coral, 2);
            recipe.AddIngredient(null, "SeaCrystal", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
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
    }
    public class OldMastersPaletteVisual : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.damage = 0;
            Projectile.width = 30;
            Projectile.height = 26;
            Projectile.tileCollide = false;
            //Main.projFrames[Projectile.type] = 6;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.HeldItem.type != ModContent.ItemType<OldMastersPalette>()) Projectile.Kill();
            //player.heldProj = Projectile.whoAmI;

            Vector2 offset = new Vector2(10 + player.direction * 21, player.gravDir * 23);
            Projectile.rotation = 1.57f;
            if (player.gravDir == -1f)
            {
                offset.Y = 16;
                Projectile.rotation = 4.712f;
            }
            float armRotation = -0.35f;
            int stretchAmount = 3;

            int bodyFrame = (player.bodyFrame.Y / player.bodyFrame.Height);
            Projectile.GetGlobalProjectile<MyProjectile>().ApplyHeldProjOffsets(player, bodyFrame, ref offset, ref Projectile.rotation, ref armRotation, ref stretchAmount);

            player.SetCompositeArmBack(enabled: true, (Player.CompositeArmStretchAmount)stretchAmount, (float)Math.PI * armRotation * player.direction);


            //code beneath this adapted from vanilla medusa head projectile

            //if (velocity.X != base.velocity.X || velocity.Y != base.velocity.Y)
            //{
            //	this.netUpdate = true;
            //}
            
            Projectile.velocity = player.GetModPlayer<MyPlayer>().MouseDirection(); //no idea why this works, maybe OffsetsPlayerOnhand code checks projectile velocity to decide its direction?
            Projectile.Center = (player.position /*+ value*/ + offset - player.GetModPlayer<MyPlayer>().MouseDirection()).Floor();
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
    public class OldMastersPaletteBrushVisual : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.damage = 0;
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
        }
        public float useAnimationMax = 0;
        int meleeFrame = 0;
        static Vector2[] handPosition = { new Vector2(-7, -10), new Vector2(2, -11), new Vector2(2, 3) };


        public override void OnSpawn(IEntitySource source)
        {
            DrawOriginOffsetX = -12 * Main.player[Projectile.owner].direction;
            DrawOriginOffsetY = -30; //* (int)Main.player[Projectile.owner].gravDir;
            if (Main.player[Projectile.owner].direction == -1) DrawOffsetX = -26;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            //if (!player.ItemAnimationActive) Projectile.Kill();
            player.heldProj = Projectile.whoAmI;

            switch (player.bodyFrame.Y / player.bodyFrame.Height)
            {
                case 1: meleeFrame = 0; break;
                case 2: meleeFrame = 1; break;
                case 3: meleeFrame = 2; break;
                default: meleeFrame = 0; break;
            }
            Projectile.Center = player.Center + new Vector2(((Vector2)handPosition.GetValue(meleeFrame)).X * player.direction, ((Vector2)handPosition.GetValue(meleeFrame)).Y * player.gravDir);
            Projectile.rotation = MathHelper.ToRadians(((Projectile.timeLeft - useAnimationMax / 2) / useAnimationMax * 198f) + 15) * -player.direction * player.gravDir;
            if (player.gravDir == -1) Projectile.rotation += 1.57f * player.direction;

            //code beneath this adapted from vanilla medusa head projectile

            //if (velocity.X != base.velocity.X || velocity.Y != base.velocity.Y)
            //{
            //	this.netUpdate = true;
            //}

            Projectile.velocity = player.GetModPlayer<MyPlayer>().MouseDirection();
            Projectile.Center = (Projectile.Center - player.GetModPlayer<MyPlayer>().MouseDirection()).Floor();
            Projectile.gfxOffY = player.gfxOffY;
            Projectile.spriteDirection = player.direction;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            OldMastersPalette mastersPalette = Main.player[Projectile.owner].HeldItem.ModItem as OldMastersPalette; //this can return an object reference not set to isntance of object error
            Player player = Main.player[Projectile.owner];
            SpriteEffects direction = SpriteEffects.None;
            if (player.direction == -1) direction = SpriteEffects.FlipHorizontally;

            Texture2D texture = ModContent.Request<Texture2D>("Emperia/Items/Palette/OldMastersPalette_Brush" + mastersPalette.visualMode, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Vector2 position = Projectile.position + new Vector2(texture.Width * 0.5f * player.direction, -texture.Height * 0.5f * player.gravDir).RotatedBy(Projectile.rotation) - Main.screenPosition; //not sure why 2f
            Main.EntitySpriteDraw(texture, position, null, lightColor, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, direction, 1);

            if (mastersPalette.selectedColors.Any())
            {
                int paintType = mastersPalette.color;
                if (paintType != 0 && mastersPalette.brushMode != 2)
                {
                    Texture2D paintTexture = mastersPalette.GetPaintBlobTexture(paintType, 0, true);
                    Color color = mastersPalette.PaintToColor(paintType);

                    if (paintType != 31) color = color.MultiplyRGB(lightColor);
                    Main.EntitySpriteDraw(paintTexture, position, new Rectangle(8, 0, 26, 30), color, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, direction, 1);
                }
            }
            return true;
        }
    }
}

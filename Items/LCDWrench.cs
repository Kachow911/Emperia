using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;
using System.Collections.Generic;
using Terraria.ModLoader.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using ReLogic.Content;
using static Emperia.UISystem;
using Emperia.Items;
using Terraria.GameInput;
using Terraria.GameContent.UI.States;
using Terraria.GameContent;
using Terraria.DataStructures;

namespace Emperia.Items
{
    public class LCDWrench : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("LCD Wrench");
            Tooltip.SetDefault("Configures colors of LCD displays");
        }
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 28;
            Item.height = 34;
            Item.useTime = 8;
            Item.useAnimation = 16;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
        }
        public int toolMode = 0;
        public int selectedBulb = 0;
        public List<Color> selectedColors = new List<Color> { new Color(255, 0, 0), new Color(255, 255, 0), new Color(0, 255, 0), new Color(0, 0, 255), new Color(170, 0, 255), new Color(255, 255, 255), new Color(150, 150, 150), new Color(50, 50, 50) };
        //public Color selectedColor = selectedColors.First();
        public Color selectedColor = Color.Black;

        public static void OverlayType_LCDWrench(Player player, ref Color lightColor, ref Texture2D overlayTexture, ref Vector2 position, ref Rectangle? rectangle, ref Color color, ref float rotation, ref Vector2 origin, ref float scale, ref SpriteEffects direction)
        {
            LCDWrench lcdWrench = player.HeldItem.ModItem as LCDWrench; //this can return an object reference not set to isntance of object error
            if (lcdWrench.toolMode == 0 && lcdWrench.selectedColor == Color.Black) return;
            overlayTexture = ModContent.Request<Texture2D>("Emperia/Items/LCDWrenchScreen", AssetRequestMode.ImmediateLoad).Value;
            color = lcdWrench.selectedColor;
            return;
        }
        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax)
            {
                ItemSwingVisual p = (Main.projectile[Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<ItemSwingVisual>(), 0, 0, Main.myPlayer, 0, 0)].ModProjectile as ItemSwingVisual);
                p.useAnimationMax = p.Projectile.timeLeft = Item.useAnimation;
                p.overlayType = OverlayType_LCDWrench;
                p.texture = ModContent.Request<Texture2D>("Emperia/Items/LCDWrench", AssetRequestMode.ImmediateLoad).Value;
            }

            if (Item.GetGlobalItem<GItem>().TileInRange(Item, player) == false) return false;
            if (player.itemAnimation % Item.useTime != 0 || !player.controlUseItem) return false;

            int i = Player.tileTargetX;
            int j = Player.tileTargetY;

            int subTile = 0; // equals something 0 to 3 depending on quadrant of tile
            if (Main.MouseWorld.X % 16 >= 8) subTile += 1;
            if (Main.MouseWorld.Y % 16 >= 8) subTile += 2;

            //if (color != 0)
            {
                if (Framing.GetTileSafely(i, j).TileType == ModContent.TileType<Tiles.LCDScreenTile>())
                {
                    switch (toolMode)
                    {
                        case 0:
                            if (!LCDSystem.activeLCDs.ContainsKey((i, j)))
                            {
                                LCDSystem.activeLCDs.Add((i, j), new Color[4] { Color.Black, Color.Black, Color.Black, Color.Black }); //if an entry ever gets set to all black, this needs to be undone
                            }
                            LCDSystem.activeLCDs[(i, j)][subTile] = selectedColor;
                            break;
                        case 1:
                            LCDSystem.activeLCDs[(i, j)] = new Color[4] { selectedColor, selectedColor, selectedColor, selectedColor };
                            break;
                            //(Main.tile[i,j] as Tiles.LCDScreenTile)
                    }
                }
            }
            return true;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (toolMode == 0 && selectedColor == Color.Black) return;
            position.X -= 1;
            position.Y -= 2; //unsure why its off lmao. maybe cus of the un-evenness of the wrench sprite
            Texture2D screenTexture = ModContent.Request<Texture2D>("Emperia/Items/LCDWrenchScreen", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw(screenTexture, position, null, selectedColor, 0f, Vector2.Zero, Main.inventoryScale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (toolMode == 0 && selectedColor == Color.Black) return;
            Texture2D screenTexture = ModContent.Request<Texture2D>("Emperia/Items/LCDWrenchScreen", AssetRequestMode.ImmediateLoad).Value;
            Vector2 position = new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - screenTexture.Height * 0.5f + 2f);
            position.Y -= 2; //unsure why its off lmao. maybe cus of the un-evenness of the wrench sprite
            spriteBatch.Draw(screenTexture, position, null, selectedColor, rotation, screenTexture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, new Vector3(selectedColor.R, selectedColor.G, selectedColor.B) / (255 * 2.25f));
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("Emperia:Wrench");
            recipe.AddIngredient(null, "LCDScreen", 1);
            recipe.AddIngredient(ItemID.Wire, 10);

            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
        public override void SaveData(TagCompound tag)
        {
            var list = new List<TagCompound>();
            for (int i = 0; i < selectedColors.Count; i++)
            {
                list.Add(new TagCompound() {
                        { "color" + i.ToString(), selectedColors[i]}, //color[] has too many generic arguments to be saved
                    });
            }
            tag["LCDWrenchColors"] = list;
        }
        public override void LoadData(TagCompound tag)
        {
            var list = tag.GetList<TagCompound>("LCDWrenchColors");
            List<Color> colors = new List<Color>();
            for (int i = 0; i < list.Count; i++)
            {
                colors.Add(list[i].Get<Color>("color" + i.ToString()));                    
            }
            selectedColors = colors;
        }
    }
    public class LCDSystem : ModSystem
    {
        public static Dictionary<(int, int), Color[]> activeLCDs = new Dictionary<(int, int), Color[]>();
        public override void LoadWorldData(TagCompound tag)
        {
            var list = tag.GetList<TagCompound>("activeLCDs");
            foreach (var item in list)
            {
                int tileX = item.GetInt("tileX");
                int tileY = item.GetInt("tileY");
                Color[] colors;
                if (item.ContainsKey("color"))
                {
                    Color color = item.Get<Color>("color");
                    colors = new[] { color, color, color, color }; //decompresses (see saveworlddata)
                }
                else
                {
                    Color color0 = item.Get<Color>("color0");
                    Color color1 = item.Get<Color>("color1");
                    Color color2 = item.Get<Color>("color2");
                    Color color3 = item.Get<Color>("color3");
                    colors = new[] { color0, color1, color2, color3 };
                }
                    
                activeLCDs[(tileX, tileY)] = colors;
            }
        }
        public override void SaveWorldData(TagCompound tag) // this WILL run when the game autosaves! it's not the same as onworldunload!
        {
            var list = new List<TagCompound>();
            foreach (var lcd in activeLCDs)
            {
                Color[] colors = lcd.Value;
                if (lcd.Value.All(x => x.Equals(lcd.Value[0]))) { colors = new[] { lcd.Value[0] }; } //compresses to an array of 1 length if all colors are the same

                if (colors.Length == 1)
                {
                    list.Add(new TagCompound() {
                        { "tileX", lcd.Key.Item1 },
                        { "tileY", lcd.Key.Item2 },
                        { "color", colors[0]},
                    });
                }

                else
                {
                    list.Add(new TagCompound() {
                        { "tileX", lcd.Key.Item1 },
                        { "tileY", lcd.Key.Item2 },
                        { "color0", colors[0]}, //color[] has too many generic arguments to be saved
                        { "color1", colors[1]},
                        { "color2", colors[2]},
                        { "color3", colors[3]},
                    });
                }
                //Emperia.DebugInfo += $"Saved {cycle.source}, ";
            }
            tag["activeLCDs"] = list;
        }
        public override void OnWorldUnload()
        {
            activeLCDs.Clear();
        }
    }

    public class LCDWrenchVisual : ModProjectile
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
        Texture2D texture = ModContent.Request<Texture2D>("Emperia/Items/LCDWrench", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        public override void OnSpawn(IEntitySource source)
        {
            DrawOriginOffsetY = -texture.Height;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.ItemAnimationActive) Projectile.Kill();
            player.heldProj = Projectile.whoAmI;

            Projectile.Center = player.itemLocation;
            Projectile.rotation = MathHelper.ToRadians(((Projectile.timeLeft - useAnimationMax / 2) / useAnimationMax * 198f) + 15) * -player.direction * player.gravDir; //rotation cannot be used in place of spriteeffects
            Projectile.rotation += player.fullRotation;
            //code beneath this adapted from vanilla medusa head projectile

            //if (velocity.X != base.velocity.X || velocity.Y != base.velocity.Y)
            //{
            //	this.netUpdate = true;
            //}

            Projectile.velocity = player.GetModPlayer<MyPlayer>().MouseDirection();

            Vector2 rotationOffset = new Vector2(-11.5f, -texture.Height);//experimentally setting Y to texture height
            if (player.sleeping.isSleeping) rotationOffset.Y = -11.5f;
            Projectile.Center = ((Projectile.Center - player.position) + rotationOffset).RotatedBy(player.fullRotation) + player.position - rotationOffset;
            if (player.sleeping.isSleeping)
            {
                Vector2 posOffset;
                player.sleeping.GetSleepingOffsetInfo(player, out posOffset);
                Projectile.Center += posOffset * 2.4f;
                Projectile.Center += new Vector2(0, 10 + (-2 * player.direction));
            }
            Projectile.Center = (Projectile.Center - player.GetModPlayer<MyPlayer>().MouseDirection()).Floor();
            Projectile.position.Y += Projectile.gfxOffY = player.gfxOffY;
            Projectile.spriteDirection = player.direction;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            LCDWrench lcdWrench = Main.player[Projectile.owner].HeldItem.ModItem as LCDWrench; //this can return an object reference not set to isntance of object error
            Player player = Main.player[Projectile.owner];
            SpriteEffects direction = SpriteEffects.None;
            if (player.direction != player.gravDir) direction = SpriteEffects.FlipHorizontally; //more compact way of checking player direction and gravity direction at once
            if (player.gravDir == -1) direction = 1 - direction | SpriteEffects.FlipVertically; //flips both horizontally and vertically if upside down

            Vector2 position = Projectile.position + new Vector2(texture.Width * 0.5f * player.direction, -texture.Height * 0.5f * player.gravDir).RotatedBy(Projectile.rotation) - Main.screenPosition; //not sure why 2f
            Main.EntitySpriteDraw(texture, position, null, lightColor, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, direction, 1);

            if (lcdWrench.toolMode == 0 && lcdWrench.selectedColor == Color.Black) return true;

            Texture2D screenTexture = ModContent.Request<Texture2D>("Emperia/Items/LCDWrenchScreen", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Main.EntitySpriteDraw(screenTexture, position, null, lcdWrench.selectedColor, Projectile.rotation, texture.Size() * 0.5f, Projectile.scale, direction, 1);
            return true;
        }
    }
}
namespace Emperia.UI
{
    class LcdUI : EmperiaUIState
    {
        Texture2D iconTexture;
        public bool mousedOverAny = false;
        public bool canScroll = true;
        LCDWrench lcdWrench;

        public LcdUI(Vector2? activationPosition = null) : base(activationPosition)
        {
            if (activationPosition == null) activationPosition = Vector2.Zero;
        }

        public override void OnInitialize()
        {
            heldItemType = ModContent.ItemType<Items.LCDWrench>();
            if (Main.gameMenu) return;
            lcdWrench = Main.LocalPlayer.HeldItem.ModItem as LCDWrench;

            iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_0", AssetRequestMode.ImmediateLoad).Value;
            MakeLargeIcons();
            MakeValueBars();
            Vector2 position = activationPosition - new Vector2(iconTexture.Width / 2, iconTexture.Height / 2);
            UIElement mainIcon = new LcdToolUI(position); //ModContent.Request<Texture2D>("Emperia/UI/Icon_0")
            MakeIcon(mainIcon, position, 40);
        }

        public void MakeLargeIcons()
        {
            int iconCount = lcdWrench.selectedColors.Count; //mastersPalette.selectedColors.Count;
            int distance = -49; //(int)Emperia.MouseControlledFloatX(false, 30) + 30; //-45;
            for (int i = 0; i < iconCount; i++)
            {
                Vector2 iconCenter = new Vector2(0, distance).RotatedBy(MathHelper.ToRadians((360 / iconCount) * i)) + activationPosition;
                Vector2 iconPosition = iconCenter - new Vector2(iconTexture.Width / 2, iconTexture.Height / 2);
                iconPosition.X = (int)Math.Round(iconPosition.X / 2) * 2;
                iconPosition.Y = (int)Math.Round(iconPosition.Y / 2) * 2;
                UIElement largeIcon = new LcdColorOption(i, iconPosition);
                MakeIcon(largeIcon, iconPosition, 40);
            }
        }
        public void MakeValueBars()
        {
            int iconCount = 3; //mastersPalette.selectedColors.Count;
            for (int i = 0; i < iconCount; i++)
            {
                Vector2 iconPosition = activationPosition + new Vector2(-(178 / 2), 70 + 30 * i);

                UIElement valueBar = new ValueBarUI(i, iconPosition);
                MakeIcon(valueBar, iconPosition, TextureAssets.ColorBar.Value.Width, TextureAssets.ColorBar.Value.Height);
            }
        }
        public void MakeIcon(UIElement icon, Vector2 position, int width, int height = -1)
        {
            if (height == -1) height = width;
            icon.Left.Set(position.X, 0);
            icon.Top.Set(position.Y, 0);
            icon.Width.Set(width, 0);
            icon.Height.Set(height, 0);
            Append(icon);
        }
    }
    class LcdUIElement : UIElement
    {
        internal Vector2 position;
        internal Texture2D iconTexture;
        internal int iconType;
        internal int iconIndex;
        internal bool mousedOver = false;
        internal bool canBeClicked = true;
        public bool visible = true;
        public void GeneralUpdate()
        {
            iconType = 0;
            //if (mastersPalette.brushMode == 2) iconType += 2;
            if (Main.mouseLeftRelease) canBeClicked = true;
        }
        public void MouseOver(UIElement element)
        {
            mousedOver = true;
            (Parent as LcdUI).mousedOverAny = true;
            Main.LocalPlayer.mouseInterface = true;
            iconType += 1;
        }
    }
    class LcdToolUI : LcdUIElement
    {
        public LcdToolUI(Vector2 pos) => position = pos;
        LCDWrench lcdWrench = Main.LocalPlayer.HeldItem.ModItem as LCDWrench;
        public override void OnInitialize()
        {
            iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_0", AssetRequestMode.ImmediateLoad).Value;
        }
        public override void Update(GameTime gameTime)
        {
            GeneralUpdate();

            (Parent as LcdUI).canScroll = true; //this should probably not be here but you cant run update in the main PaintUI
            Main.LocalPlayer.GetModPlayer<MyPlayer>().scrollingInUI = true;

            if (Vector2.Distance((Parent as LcdUI).activationPosition, Main.MouseScreen) < 19f)
            {
                MouseOver(this);
                if (Main.mouseLeft && canBeClicked)
                {
                    if (Main.mouseLeft && canBeClicked)
                    {
                        lcdWrench.toolMode = (lcdWrench.toolMode + 1) % 3;
                        canBeClicked = false;
                    }
                }
            }
            else
            {
                mousedOver = false;
                (Parent as LcdUI).mousedOverAny = false;
            }
            iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_" + iconType, AssetRequestMode.ImmediateLoad).Value;
            if (Main.LocalPlayer.HeldItem.ModItem is not LCDWrench || lcdWrench != (Main.LocalPlayer.HeldItem.ModItem as LCDWrench))
            {
                (Parent as EmperiaUIState).TryDeactivate(); //this check only seems to work in PaintUI
            } 

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Main.LocalPlayer.HeldItem.ModItem is not LCDWrench) return;
            
            base.Draw(spriteBatch);
            spriteBatch.Draw(iconTexture, position, null, Color.White);
            Texture2D brushTexture = ModContent.Request<Texture2D>("Emperia/UI/Brush_" + lcdWrench.toolMode, AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw(brushTexture, position, null, Color.White);

            //IngameOptions.DrawValueBar(spriteBatch, 1f, 0.5f, 1, DelegateMethods.ColorLerp_HSL_S);
        }
    }
    class LcdColorOption : LcdUIElement
    {
        public LcdColorOption(int index, Vector2 pos)
        {
            iconIndex = index;
            position = pos;
        }
        //public Color color = Color.Black;
        public Color Color
        {
            get { return lcdWrench.selectedColors[iconIndex]; }
            set { lcdWrench.selectedColors[iconIndex] = value; }
        }

        public Vector3 HSL;
        Texture2D bucketTexture;
        Texture2D paintTexture;
        LCDWrench lcdWrench = Main.LocalPlayer.HeldItem.ModItem as LCDWrench;

        public override void OnInitialize()
        {
            iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_0", AssetRequestMode.ImmediateLoad).Value;
            /*switch (iconIndex)
            {
                case 0: color = Color.Red; break;
                case 1: color = Color.Green; break;
                case 2: color = Color.Blue; break;
                case 3: color = Color.Orange; break;
                case 4 : color = Color.Purple; break;
            }*/
            //color = Color.Black;
            if (!Main.gameMenu) HSL = Main.rgbToHsl(Color);
            bucketTexture = ModContent.Request<Texture2D>("Emperia/UI/Bucket", AssetRequestMode.ImmediateLoad).Value;
            paintTexture = ModContent.Request<Texture2D>("Emperia/UI/PaintSplatter", AssetRequestMode.ImmediateLoad).Value;
        }
        public override void Update(GameTime gameTime)
        {
            if (!visible) return;
            GeneralUpdate();

            //color = Main.hslToRgb(HSL);

            //UISystem.AddIconScrollWheelFunctionality(ref lcdWrench.selectedColor, color, lcdWrench.selectedColors, iconIndex, ref (Parent as LcdUI).canScroll;
            if (lcdWrench.selectedBulb == iconIndex && PlayerInput.ScrollWheelDeltaForUI != 0 && (Parent as LcdUI).canScroll)
            {
                int iconToScrollTo = (iconIndex - Math.Sign(PlayerInput.ScrollWheelDeltaForUI)) % lcdWrench.selectedColors.Count;
                if (iconToScrollTo < 0) iconToScrollTo += lcdWrench.selectedColors.Count;
                lcdWrench.selectedBulb = iconToScrollTo;
                (Parent as LcdUI).canScroll = false;
                // PlayerInput.ScrollWheelDelta = 0; is set in ModPlayer PreUpdate()
            }
            if (Vector2.Distance(position + new Vector2(iconTexture.Width / 2, iconTexture.Height / 2), Main.MouseScreen) < 19f)
            {
                MouseOver(this);
                if (Main.mouseLeft)
                {
                    if (lcdWrench.selectedBulb != iconIndex)
                    {
                        lcdWrench.selectedBulb = iconIndex;
                    }
                    //else mastersPalette.curatedColor = 0;
                }
            }
            else
            {
                mousedOver = false;
                (Parent as LcdUI).mousedOverAny = false;
            }

            if (lcdWrench.selectedBulb == iconIndex)
            {
                //Main.NewText(lcdWrench.selectedColor);
                //Main.NewText(iconIndex);
                lcdWrench.selectedColor = Color;
                //Main.NewText(lcdWrench.selectedColor);
            }

            iconTexture = ModContent.Request<Texture2D>("Emperia/UI/Icon_" + iconType).Value;

            bucketTexture = ModContent.Request<Texture2D>("Emperia/UI/Bucket").Value;
            paintTexture = ModContent.Request<Texture2D>("Emperia/UI/PaintSplatter").Value;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Main.LocalPlayer.HeldItem.ModItem is not LCDWrench) return;

            if (!Main.gameMenu) visible = true;
            else visible = false;
            if (!visible) return;

            base.Draw(spriteBatch);
            Color brightness = new Color(255, 255, 255);
            if (lcdWrench.selectedBulb != iconIndex)
            {
                brightness = new Color(150, 150, 150);
                if (!mousedOver) brightness = new Color(80, 80, 80);
            }
            spriteBatch.Draw(iconTexture, position, null, brightness);
            if (brightness == new Color(150, 150, 150)) brightness = new Color(190, 190, 190); //buckets need to be brighter to be distinguishable
            else if (brightness == new Color(80, 80, 80)) brightness = new Color(140, 140, 140);
            spriteBatch.Draw(bucketTexture, position, null, brightness);
            spriteBatch.Draw(paintTexture, position, null, Color.MultiplyRGB(brightness));
        }
    }
    class ValueBarUI : LcdUIElement
    {
        public ValueBarUI(int index, Vector2 pos)
        {
            iconIndex = index;
            position = pos;
        }
        LCDWrench lcdWrench = Main.LocalPlayer.HeldItem.ModItem as LCDWrench;
        Rectangle bar;
        public override void OnInitialize()
        {
            bar = new Rectangle((int)position.X, (int)position.Y, TextureAssets.ColorBar.Value.Width, TextureAssets.ColorBar.Value.Height);
        }
        public override void Update(GameTime gameTime)
        {
            //(Parent as LcdUI).canScroll = true; //this should probably not be here but you cant run update in the main PaintUI

            if (bar.Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                MouseOver(this);
                if (Main.mouseLeft && canBeClicked)
                {
                    float percent = (Main.MouseScreen.X - bar.X) / bar.Width;

                    //LcdColorOption bulb = bulbIconList[lcdWrench.selectedBulb] as LcdColorOption;
                    switch (iconIndex)
                    {
                        case 0: GetBulb(lcdWrench.selectedBulb).HSL.X = percent; break;
                        case 1: GetBulb(lcdWrench.selectedBulb).HSL.Y = percent; break;
                        case 2: GetBulb(lcdWrench.selectedBulb).HSL.Z = ScaledLuminosity(percent); break;
                    }
                    GetBulb(lcdWrench.selectedBulb).Color = Main.hslToRgb(GetBulb(lcdWrench.selectedBulb).HSL);
                    //Main.NewText(bulb.color);
                    lcdWrench.selectedColor = GetBulb(lcdWrench.selectedBulb).Color;
                    //Main.NewText(lcdWrench.selectedColor, Color.Green);
                }
            }
            else
            {
                if (mousedOver && Main.mouseLeft) //trying to drag the slider to the very end without mousing off is almost impossible, this should make that easier.
                {
                    float percent = -1f;
                    if (Main.MouseScreen.X < bar.X) percent = 0f;
                    if (Main.MouseScreen.X > bar.X + bar.Width) percent = 1f;
                    if (percent > -1f)
                    {
                        switch (iconIndex)
                        {
                            case 0: GetBulb(lcdWrench.selectedBulb).HSL.X = percent; break;
                            case 1: GetBulb(lcdWrench.selectedBulb).HSL.Y = percent; break;
                            case 2: GetBulb(lcdWrench.selectedBulb).HSL.Z = ScaledLuminosity(percent); break;
                        }
                    }

                }
                mousedOver = false;
                (Parent as LcdUI).mousedOverAny = false;
            }
        }
        public static float ScaledLuminosity(float percent)
        {
            return 0.15f + 0.85f * percent;
        }
        public static float UnscaleLuminosity(float percent)
        {
            return (percent - 0.15f) / 0.85f;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Main.LocalPlayer.HeldItem.ModItem is not LCDWrench) return;

            //Utils.ColorLerpMethod hslMethod = DelegateMethods.ColorLerp_HSL_H;
            LerpDelegate hslMethod = null;
            float percent = 0.3f;
            //float percent = GetBulbAndValue();

            //Main.NewText((bulbIconList[lcdWrench.selectedBulb] as LcdColorOption).HSL);
            //Main.NewText(percent);
            switch (iconIndex)
            {
                case 0: hslMethod = ColorLerp_HSL_H; percent = GetBulb(lcdWrench.selectedBulb).HSL.X; break;
                case 1: hslMethod = ColorLerp_HSL_S; percent = GetBulb(lcdWrench.selectedBulb).HSL.Y; break;
                case 2: hslMethod = ColorLerp_HSL_L; percent = UnscaleLuminosity(GetBulb(lcdWrench.selectedBulb).HSL.Z); break;
            }
            DrawValueBar(spriteBatch, 1f, percent, position, hslMethod, GetBulb(lcdWrench.selectedBulb).HSL, 0);
        }
        public static float DrawValueBar(SpriteBatch sb, float scale, float perc, Vector2 position, LerpDelegate colorMethod, Vector3 HSL, int lockState = 0)
        {
            Texture2D barTexture = TextureAssets.ColorBar.Value;
            Vector2 barDimensions = new Vector2(barTexture.Width, barTexture.Height) * scale;
            IngameOptions.valuePosition.X -= (int)barDimensions.X;
            Rectangle rectangle = new Rectangle((int)position.X, (int)position.Y, (int)barDimensions.X, (int)barDimensions.Y);
            Rectangle destinationRectangle = rectangle;
            sb.Draw(barTexture, rectangle, Color.White);
            int barWidth = 167;
            float barSegmentWidth = rectangle.X + 5f * scale;
            float barHeightCenter = rectangle.Y + 4f * scale;
            for (float i = 0f; i < barWidth; i += 1f)
            {
                float percent = i / barWidth;
                sb.Draw(TextureAssets.ColorBlip.Value, new Vector2(barSegmentWidth + i * scale, barHeightCenter), null, colorMethod(percent, HSL), 0f, Vector2.Zero, scale, (SpriteEffects)0, 0f);
            }
            rectangle.Inflate((int)(-5f * scale), 0);
            bool mousedOver = rectangle.Contains(new Point(Main.mouseX, Main.mouseY));
            if (lockState == 2)
            {
                mousedOver = false;
            }
            if (mousedOver || lockState == 1)
            {
                sb.Draw(TextureAssets.ColorHighlight.Value, destinationRectangle, new Color(255, 210, 0));//Main.OurFavoriteColor);
            }
            sb.Draw(TextureAssets.ColorSlider.Value, new Vector2(barSegmentWidth + 167f * scale * perc, barHeightCenter + 4f * scale), (Rectangle?)null, Color.White, 0f, new Vector2(0.5f * (float)TextureAssets.ColorSlider.Width(), 0.5f * (float)TextureAssets.ColorSlider.Height()), scale, (SpriteEffects)0, 0f);
            if (Main.mouseX >= rectangle.X && Main.mouseX <= rectangle.X + rectangle.Width)
            {
                IngameOptions.inBar = mousedOver;
                return (float)(Main.mouseX - rectangle.X) / (float)rectangle.Width;
            }
            IngameOptions.inBar = false;
            if (rectangle.X >= Main.mouseX)
            {
                return 0f;
            }
            return 1f;
        }

        public delegate Color LerpDelegate(float percent, Vector3 HSL);

        public static Color ColorLerp_HSL_H(float percent, Vector3 HSL)
        {
            return Main.hslToRgb(percent, 1f, 0.5f);
        }
        public static Color ColorLerp_HSL_S(float percent, Vector3 HSL)
        {
            return Main.hslToRgb(HSL.X, percent, HSL.Z);
        }
        public static Color ColorLerp_HSL_L(float percent, Vector3 HSL)
        {
            return Main.hslToRgb(HSL.X, HSL.Y, ScaledLuminosity(percent));
        }
        public LcdColorOption GetBulb(int iconIndex)
        {
            foreach (var bulb in Parent.Children)
            {
                if (bulb is not LcdColorOption) continue;
                if ((bulb as LcdColorOption).iconIndex == iconIndex) return (bulb as LcdColorOption);
            }
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;

namespace Emperia.Items {
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
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 1;
            //Item.useStyle = 5;
            //Item.channel = true;
            Item.value = 10000;
            Item.rare = 2;
            Item.autoReuse = true;
            //Item.UseSound = SoundID.Item64;
        }

        public List<int> selectedColors = new List<int>();
        public int tileSelectedColor;
        public int wallSelectedColor;
        public int brushMode = 0;
        public bool curatedPalette = false;
        //public List<int> selectedCuratedColors = new List<int>();
        public int curatedColor;

        /*public override void HoldItem(Player player)
        {
            if (Main.LocalPlayer.mouseInterface && (Math.Abs(Main.MouseScreen.X - paintUIActivationPosition.X) > 84 || Math.Abs(Main.MouseScreen.Y - paintUIActivationPosition.Y) > 84) || canBeClosed && Main.mouseRight) EmperiaSystem.paintUIActive = false;
        }*/
        public override bool? UseItem(Player player)
        {
            Item.noUseGraphic = false;
            int i = Player.tileTargetX;
            int j = Player.tileTargetY;
            byte color = (byte)selectedColors.LastOrDefault();
            if (brushMode == 0 && tileSelectedColor != 0) color = (byte)tileSelectedColor;
            if (brushMode == 1 && wallSelectedColor != 0) color = (byte)wallSelectedColor;

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
    }
}

using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Granite
{
	public class GraniteBar : ModItem
	{
		public override void SetDefaults()
		{

			item.width = 8;
			item.height = 8;
			item.maxStack = 999;
			item.value = 14000;
			item.rare = 1;

        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Granite Bar");
      Tooltip.SetDefault("Contains pure energy");
    }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldBar, 1);
			recipe.AddIngredient(ItemID.Granite, 10);
			recipe.AddTile(TileID.Furnaces);  
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumBar, 1);
			recipe.AddIngredient(ItemID.Granite, 10);
			recipe.AddTile(TileID.Furnaces);  
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}

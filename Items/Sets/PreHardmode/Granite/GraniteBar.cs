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

			Item.width = 8;
			Item.height = 8;
						Item.value = 17000;
			Item.rare = ItemRarityID.Blue;

        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Granite Bar");
      // Tooltip.SetDefault("’Energy flows through it like a living thing’");
    }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Granite, 10);
			recipe.AddRecipeGroup("Emperia:EvilOre", 2);
			recipe.AddTile(TileID.Furnaces);  
			recipe.Register();
			
			
		
        }
    }
}

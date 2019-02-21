using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.MageProg
{
    public class SorcerorCrystal : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sorceror's Crystal");
			Tooltip.SetDefault("+20 max life and mana");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.statLifeMax2 += 20;
			player.statManaMax2 += 20;
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Sapphire, 5);
			recipe.AddIngredient(ItemID.FallenStar, 2);
			recipe.AddIngredient(ItemID.Silk, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
	
		}
		
    }
}

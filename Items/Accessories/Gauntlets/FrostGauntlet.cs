using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class FrostGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gauntlet of Frost");
			Tooltip.SetDefault("Chance to freeze enemies on hit\nEnemies shatter on death, sending 2-3 ice shards in random directions");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.defense = 0;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>(mod).frostGauntlet = true;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(ItemID.IceBlock, 20);
			recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

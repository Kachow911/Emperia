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
			// DisplayName.SetDefault("Gauntlet of Frost");
			// Tooltip.SetDefault("Chance to freeze enemies on hit\nEnemies shatter on death, sending 2-3 frost bolts in random directions");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 2;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.defense = 0;
            Item.accessory = true;
            Item.GetGlobalItem<GItem>().gauntletPower = 0.30f; //temp
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().frostGauntlet = true;
        }
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(ItemID.IceBlock, 20);
			recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
    }
}

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class FerocityGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gauntlet of Ferocity");
			Tooltip.SetDefault("Attacks have a chance to deal double damage\nEnemies take Double Knockback\nWhen you strike an enemy, you have a chance to momentarily gain increased Damage, Speed and Defense\nIncreased Max Mana.");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 4;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>(mod).doubleKnockback = true;
			player.GetModPlayer<MyPlayer>(mod).ferocityGauntlet = true;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MetallurgyGauntlet", 1);
            recipe.AddIngredient(null, "EnchantedGauntlet", 1);
            recipe.AddIngredient(null, "SpeedGauntlet", 1);
            recipe.AddRecipeGroup("Emperia:AdBar", 10);
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
			
        }
    }
}

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
            Item.width = 30;
            Item.height = 28;
            Item.rare = 4;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.accessory = true;
            Item.GetGlobalItem<GItem>().gauntletPower = 0.30f; // temp
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().doubleKnockback = true;
			player.GetModPlayer<MyPlayer>().ferocityGauntlet = true;
			player.statManaMax2 += 40;
        }
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "MetallurgyGauntlet", 1);
            recipe.AddIngredient(null, "EnchantedGauntlet", 1);
            recipe.AddIngredient(null, "SpeedGauntlet", 1);
            recipe.AddRecipeGroup("Emperia:AdBar", 10);
			recipe.AddIngredient(ItemID.SoulofNight, 5);
			recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            
			
        }
    }
}

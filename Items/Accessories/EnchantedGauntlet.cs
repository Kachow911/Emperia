using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
{
    public class EnchantedGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Gauntlet");
			Tooltip.SetDefault("Slightly increased knockback, mana, magic damage and movement speed");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 1;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.defense = 1;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>(mod).slightKnockback = true;
			player.statManaMax2 += 10;
			player.magicDamage *= 1.02f;
			player.moveSpeed += 0.1f;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
			recipe.AddIngredient(null, "AetherBar", 3);
			recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

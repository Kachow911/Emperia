using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class SpeedGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gauntlet of Speed");
			Tooltip.SetDefault("15% increased movement speed\nSlightly increased knockback");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.defense = 3;
            item.accessory = true;
            item.GetGlobalItem<GItem>().isGauntlet = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>().slightKnockback = true;
            player.moveSpeed += 0.15f;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(ItemID.Aglet, 1);
			recipe.AddIngredient(ItemID.Feather, 7);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

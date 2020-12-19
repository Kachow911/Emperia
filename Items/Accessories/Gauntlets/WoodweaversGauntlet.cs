using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class WoodweaversGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Woodweaver's Gauntlet");
			Tooltip.SetDefault("Sword damage increased by up to 10% at close range, 20% on large foes\nSword strikes may fire a tiny splinter of wood at foes");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 1;
            item.value = 1500;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			//player.GetModPlayer<MyPlayer>().slightKnockback = true;
            player.GetModPlayer<MyPlayer>().gauntletBonus = 0.20f;
            player.GetModPlayer<MyPlayer>().woodGauntlet = true;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 20);
			recipe.AddRecipeGroup("IronBar", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

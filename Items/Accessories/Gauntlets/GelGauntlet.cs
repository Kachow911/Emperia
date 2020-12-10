using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class GelGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gelatinous Gauntlet");
			Tooltip.SetDefault("Sword strikes deal increased damage at close range, especially on large targets\nSword strikes on knockback immune foes will make you bounce back safely\nAllows you to turn around while walking backwards with all swords");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 1;
            item.value = 3750;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>().gauntletBonus = 0.25f;
            player.GetModPlayer<MyPlayer>().gelGauntlet = true;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(ItemID.Gel, 40);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

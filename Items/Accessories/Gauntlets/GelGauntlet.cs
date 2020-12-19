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
			Tooltip.SetDefault("Sword damage increased by up to 12% at close range, 25% on large foes\nSword strikes on knockback immune foes bounce you back safely\nAllows you to swing swords while walking backwards");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 1;
            item.value = 3750;
            item.accessory = true;
            //item.alpha = 50;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>().gauntletBonus = 0.25f;
            player.GetModPlayer<MyPlayer>().gelGauntlet = true;
            player.GetModPlayer<MyPlayer>().wristBrace = true;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(null, "WristBrace", 1);
            recipe.AddIngredient(ItemID.Gel, 40);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

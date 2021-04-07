using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class BloodGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloodstained Gauntlet");
			Tooltip.SetDefault("Sword damage increased by up to 15% at close range, 30% on large foes\nSword strikes may empower you, allowing you to heal back the damage taken from one hit by attacking\nAllows you to swing swords while walking backwards");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 1;
            item.value = 32000;
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>().gauntletBonus = 0.30f;
			player.GetModPlayer<MyPlayer>().bloodGauntlet = true;
            player.GetModPlayer<MyPlayer>().wristBrace = true;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(null, "WristBrace", 1);
			recipe.AddIngredient(null, "AetherBar", 8);
			recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

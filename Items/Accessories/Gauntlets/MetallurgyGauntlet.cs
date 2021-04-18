using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class MetallurgyGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Metallurgy Gauntlet");
			Tooltip.SetDefault("Sword damage increased by up to 12% at close range, 25% on large foes\nSword strikes increase defense briefly\nAllows you to swing swords while walking backwards");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 1;
            item.value = 4500;
            item.accessory = true;
            item.GetGlobalItem<GItem>().isGauntlet = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>().gauntletBonus = 0.25f;
			player.GetModPlayer<MyPlayer>().metalGauntlet = true;
            player.GetModPlayer<MyPlayer>().wristBrace = true;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(null, "WristBrace", 1);
            recipe.AddRecipeGroup("Emperia:AnyCopperBar", 8);
            recipe.AddRecipeGroup("Emperia:AnySilverBar", 6);
            recipe.AddRecipeGroup("Emperia:AnyGoldBar", 4);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

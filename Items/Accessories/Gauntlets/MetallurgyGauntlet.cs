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
			// DisplayName.SetDefault("Metallurgy Gauntlet");
			// Tooltip.SetDefault("Sword damage increased by up to 12% at close range, 25% on large foes\nSword strikes increase defense briefly\nAllows you to swing swords while walking backwards");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 1;
            Item.value = 4500;
            Item.accessory = true;
            Item.GetGlobalItem<GItem>().gauntletPower = 0.25f;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().metalGauntlet = true;
            player.GetModPlayer<MyPlayer>().wristBrace = true;
        }
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(null, "WristBrace", 1);
            recipe.AddRecipeGroup("Emperia:CopperBar", 8);
            recipe.AddRecipeGroup("Emperia:SilverBar", 6);
            recipe.AddRecipeGroup("Emperia:GoldBar", 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
    }
}

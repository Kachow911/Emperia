using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Marble
{
    public class MarbleBow : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marble Bow");
		}
        public override void SetDefaults()
        {
            item.damage = 11;
            item.noMelee = true;
            item.ranged = true;
            item.width = 69;
            item.height = 40;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = ItemID.WoodenArrow;
            item.knockBack = 1;
            item.value = 1000;
            item.rare = 2;
            item.autoReuse = true;
            item.shootSpeed = 11f;
			item.UseSound = SoundID.Item5; 
        }
        public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "MarbleBar", 8);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
    }
}
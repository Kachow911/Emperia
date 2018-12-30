using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Granite
{
    public class GraniteBow : ModItem
    {
		int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Bow");
			Tooltip.SetDefault("Fires a granite energy arrow every 3 shots");
		}
        public override void SetDefaults()
        {
            item.damage = 15;
            item.noMelee = true;
            item.ranged = true;
            item.width = 69;
            item.height = 40;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = ItemID.WoodenArrow;
            item.knockBack = 1;
            item.value = 1000;
            item.rare = 2;
            item.autoReuse = false;
            item.shootSpeed = 15f;
			item.UseSound = SoundID.Item5; 
        }
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (counter == 2)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("GraniteArrow"), damage, knockBack, player.whoAmI);
				counter = 0;
				return false;
			}
			counter++;
			return true;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "GraniteBar", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
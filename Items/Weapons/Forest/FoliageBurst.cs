using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Forest
{
    public class FoliageBurst : ModItem
    {
		private int notchedArrows = 1;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Foliage Burst");
			Tooltip.SetDefault("Allows you to notch arrows by right clicking\nYou can notch an additional 4 arrows");
		}
        public override void SetDefaults()
        {
            item.damage = 27;
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
            item.rare = 3;
            item.autoReuse = false;
            item.shootSpeed = 8f;
			item.UseSound = SoundID.Item5; 
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (!(player.altFunctionUse == 2))
			{
				int numberProjectiles = notchedArrows; 
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = Vector2.Zero;
					if (!(numberProjectiles == 1))
						perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(-10 + i * 20 / numberProjectiles));
					else
						perturbedSpeed = new Vector2(speedX, speedY);
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 2, perturbedSpeed.Y * 2, type, damage, knockBack, player.whoAmI);
				}
				notchedArrows = 1;
			}
			return false;  
		}
		public override bool AltFunctionUse(Player player)
		{
			Color rgb = new Color(0, 255, 0);
			int index2 = Dust.NewDust(player.position, player.width, player.height, 76, (float) 0, (float) 0, 0, rgb, 0.5f);
			if (notchedArrows < 5)
				notchedArrows++;
			return true;
		}
		public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				return false;
			}
			return true;
		}
        /*public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 8);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }*/
    }
}
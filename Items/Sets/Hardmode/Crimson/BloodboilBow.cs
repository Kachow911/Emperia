using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Crimson
{
    public class BloodboilBow : ModItem
    {
		//int counter = 0;
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Granite Bow");
			Tooltip.SetDefault("Replaces wooden arrows with columns of ichor bubbles\n33% chance not to consume ammo");
		}
        public override void SetDefaults()
        {
            item.damage = 25;
            item.noMelee = true;
            item.ranged = true;
            item.width = 30;
            item.height = 40;
            item.useTime = 28;
            item.useAnimation = 29;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = ItemID.WoodenArrow;
            item.knockBack = 1;
            item.value = 22500;
            item.rare = 4;
            item.autoReuse = false;
            item.shootSpeed = 12f;
			item.UseSound = SoundID.Item5; 
        }

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = mod.ProjectileType("BigBubble");
				int numberProjectiles = 3;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY);
					//float speedFact = (float)Main.rand.Next(2, 15) / 10;
					Projectile.NewProjectile(position.X - (speedX * i), position.Y - (speedY * i), perturbedSpeed.X, perturbedSpeed.Y , type, damage, knockBack, player.whoAmI);
				}
			}
			return true;

		}

		public override bool ConsumeAmmo(Player player)
		{
			return !(Main.rand.Next(3) == 0);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
        public override void AddRecipes()
		{
			/*ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "GraniteBar", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();*/
		}
    }
}

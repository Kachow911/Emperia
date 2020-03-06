using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Desert
{
    public class CarapaceCruise : ModItem
    {
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Wave");
			Tooltip.SetDefault("Summons worms from the sky to plague your foes");
		}


        public override void SetDefaults()
        {
            item.damage = 15;
            item.magic = true;
            item.mana = 9;
            item.width = 52;
            item.height = 60;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item34;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("DesertWorm");
            item.shootSpeed = 8f;
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type1, ref int damage, ref float knockBack)
		{
			float speedFactor;
			float damageFactor;
			for (int i = 0; i <= 3; i++)
			{
				Vector2 placePosition = new Vector2(Main.MouseWorld.X + Main.rand.Next(-400, 400), player.Center.Y - 800);
				Vector2 direction = Main.MouseWorld - placePosition;
				direction.Normalize();
					speedFactor = 15.5f;
					damageFactor = 1f;

				Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * speedFactor, direction.Y * speedFactor, type1, damage, 1, Main.myPlayer, 0, 0);
			}
			return false;
		}
	}
}

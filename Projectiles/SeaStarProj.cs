using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	public class SeaStarProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sea Star");

		}
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.PainterPaintball);
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.timeLeft = 200;
			projectile.height = 24;
			projectile.width = 24;
			projectile.penetrate = 4;
			projectile.extraUpdates = 1;
			projectile.alpha = 0;
		}

		public override void AI()
		{

			projectile.rotation += 0.2f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
				projectile.Kill();
			else
			{
				projectile.ai[0] += 0.1f;
				if (projectile.velocity.X != oldVelocity.X)
					projectile.velocity.X = -oldVelocity.X;

				if (projectile.velocity.Y != oldVelocity.Y)
					projectile.velocity.Y = -oldVelocity.Y;

				projectile.velocity *= 0.5f;
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			if (Main.rand.Next(0, 4) == 0)
				Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("SeaStar"), 1, false, 0, false, false);

			for (int i = 0; i < 8; ++i)
			{
				Color rgb = new Color(83, 66, 180);
				int index3 = Dust.NewDust(new Vector2((float) (projectile.position.X + 4.0), (float) (projectile.position.Y + 4.0)), projectile.width - 8, projectile.height - 8, 76, 0.0f, 0.0f, 0, rgb, 1.5f);
			}
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
		}

	}
}

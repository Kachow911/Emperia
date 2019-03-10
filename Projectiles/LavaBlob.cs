using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	public class LavaBlob : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magma Blob");

		}
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.PainterPaintball);
			projectile.friendly = true;
			projectile.hostile = false;
            projectile.magic = true;
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
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("FireBall"), projectile.damage, 0f, Main.player[projectile.owner].whoAmI);
           

			for (int i = 0; i < 8; ++i)
			{
				int index3 = Dust.NewDust(new Vector2((float) (projectile.position.X + 4.0), (float) (projectile.position.Y + 4.0)), projectile.width - 8, projectile.height - 8, 258, 0.0f, 0.0f, 0, Color.White, 1.5f);
			}
		}

	}
}

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
			// DisplayName.SetDefault("Magma Blob");

		}
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.PainterPaintball);
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
			Projectile.timeLeft = 200;
			Projectile.height = 24;
			Projectile.width = 24;
			Projectile.penetrate = 4;
			Projectile.extraUpdates = 1;
			Projectile.alpha = 0;
		}

		public override void AI()
		{

			Projectile.rotation += 0.2f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
				Projectile.Kill();
			else
			{
				Projectile.ai[0] += 0.1f;
				if (Projectile.velocity.X != oldVelocity.X)
					Projectile.velocity.X = -oldVelocity.X;

				if (Projectile.velocity.Y != oldVelocity.Y)
					Projectile.velocity.Y = -oldVelocity.Y;

				Projectile.velocity *= 0.5f;
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position.X, Projectile.position.Y, 0, 0, ModContent.ProjectileType<FireBall>(), Projectile.damage, 0f, Main.player[Projectile.owner].whoAmI);
           

			for (int i = 0; i < 8; ++i)
			{
				int index3 = Dust.NewDust(new Vector2((float) (Projectile.position.X + 4.0), (float) (Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, 258, 0.0f, 0.0f, 0, Color.White, 1.5f);
			}
		}

	}
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	public class ScorchBlast : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scorch Blast");
		}

		public override void SetDefaults()
		{
			Projectile.penetrate = 1;
			Projectile.hostile = false;
			Projectile.friendly = true;
			Projectile.aiStyle = 1;
			Projectile.timeLeft = 240;
			Projectile.alpha = 255;
			AIType = ProjectileID.Bullet;
			Projectile.tileCollide = true;
			Projectile.DamageType = DamageClass.Magic;
		}

		public override void AI()
		{
			for (int i = 0; i < 10; i++)
			{
				int num = Dust.NewDust(Projectile.Center, 26, 26, DustID.LavaMoss, 0f, 0f, 0, default(Color), 1f);
				Main.dust[num].alpha = 0;
				Main.dust[num].position.X = Projectile.Center.X - Projectile.velocity.X / 10f * (float)i;
				Main.dust[num].position.Y = Projectile.Center.Y - Projectile.velocity.Y / 10f * (float)i;
				Main.dust[num].velocity *= 0f;
				Main.dust[num].noGravity = true;
			}
		}

		public override void OnKill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.LavaMoss);
				Vector2 vel = new Vector2(0, -1).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
			}
		}

	}
}
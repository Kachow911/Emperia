using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	public class ShadowBallProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Spikes");

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
			Main.projFrames[projectile.type] = 7;
		}

		public override void AI()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				if (projectile.frame < 4)
					projectile.frame = (projectile.frame + 1);
				else
					projectile.frame = 0;
			} 
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
				Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("ShadowBall"), 1, false, 0, false, false);

			for (int i = 0; i < 8; ++i)
			{
				Color rgb = new Color(83, 66, 180);
				int index3 = Dust.NewDust(new Vector2((float) (projectile.position.X + 4.0), (float) (projectile.position.Y + 4.0)), projectile.width - 8, projectile.height - 8, DustID.Shadowflame, 0.0f, 0.0f, 0, Color.White, 1.5f);
			}
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 27);
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (Main.rand.Next(3) == 0)
			 target.AddBuff(BuffID.ShadowFlame, 120);

		}

	}
}

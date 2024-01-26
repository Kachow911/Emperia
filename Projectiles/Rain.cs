using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles
{
	public class Rain : ModProjectile
	{
		private bool init = false;
		Color rgb = new Color(0, 0, 0);
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.PainterPaintball);

			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.DamageType = DamageClass.Magic;

		}
		
	

		public override void AI()
		{
			Projectile.ai[1] ++;
			int x1 = Main.rand.Next(7);
			if (!init)
			{
				rgb = new Color(83, 66, 180);

                for (int index1 = 0; index1 < 4; ++index1)
                {
                    int index3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Snow, (float)Projectile.velocity.X, (float)Projectile.velocity.Y, 0, rgb, 1.1f);
                    Main.dust[index3].noGravity = true;
                    Main.dust[index3].velocity = Projectile.Center - Main.dust[index3].position;
                    ((Vector2)@Main.dust[index3].velocity).Normalize();
                    Dust dust1 = Main.dust[index3];
                    Vector2 vector2_1 = dust1.velocity * -3f;
                    dust1.velocity = vector2_1;
                    Dust dust2 = Main.dust[index3];
                    Vector2 vector2_2 = dust2.velocity + (Projectile.velocity / 2f);
                    dust2.velocity = vector2_2;
                }
                init = true;
			}

            int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, DustID.Snow, (float)(Projectile.velocity.X * 0.200000002980232), (float)(Projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.9f);
            Main.dust[index2].position = Projectile.Center;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = Projectile.velocity * 0.5f;
        }
		public override void OnKill(int timeLeft)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int index1 = 0; index1 < 10; ++index1)
			{
			int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Snow, 0.0f, 0.0f, 0, rgb, 1f);
			Main.dust[index2].noGravity = true;
			Dust dust1 = Main.dust[index2];
			dust1.velocity = dust1.velocity * 1.2f;
			Main.dust[index2].scale = 1.2f;
			Dust dust2 = Main.dust[index2];
			dust2.velocity = dust2.velocity - (Projectile.oldVelocity * 0.3f);
			int index3 = Dust.NewDust(new Vector2((float) (Projectile.position.X + 4.0), (float) (Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, DustID.Snow, 0.0f, 0.0f, 0, rgb, 1.5f);
			Main.dust[index3].noGravity = true;
			Dust dust3 = Main.dust[index3];
			dust3.velocity = dust3.velocity* 2f;
			}
		}
		
	}
	
}
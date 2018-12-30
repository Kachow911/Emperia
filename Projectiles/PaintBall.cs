using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles
{
	public class PaintBall : ModProjectile
	{
		private bool init = false;
		Color rgb = new Color(0, 0, 0);
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.PainterPaintball);

			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.ranged = true;

		}
		
	

		public override void AI()
		{
			projectile.ai[1] ++;
			int x1 = Main.rand.Next(7);
			if (!init)
			{
				
				if (x1 == 0)
				{
					rgb = new Color(230, 0, 0);
				}			
				else if (x1 == 1)
				{
					rgb = new Color(255, 255, 0);			
				}
				else if (x1 == 2)
				{
					rgb = new Color(0, 0, 240);
				}
				else if(x1 == 3)
				{
					rgb = new Color(0, 255, 0);
				}
				else if(x1 == 4)
				{
					rgb = new Color(255, 105, 180);
				}
				else if (x1 == 5)
				{
					rgb = new Color(132, 112, 255);
				}
				else if (x1 == 6)
				{
				rgb = new Color(255, 165, 0);
				}

                for (int index1 = 0; index1 < 4; ++index1)
                {
                    int index3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 76, (float)projectile.velocity.X, (float)projectile.velocity.Y, 0, rgb, 1.1f);
                    Main.dust[index3].noGravity = true;
                    Main.dust[index3].velocity = projectile.Center - Main.dust[index3].position;
                    ((Vector2)@Main.dust[index3].velocity).Normalize();
                    Dust dust1 = Main.dust[index3];
                    Vector2 vector2_1 = dust1.velocity * -3f;
                    dust1.velocity = vector2_1;
                    Dust dust2 = Main.dust[index3];
                    Vector2 vector2_2 = dust2.velocity + (projectile.velocity / 2f);
                    dust2.velocity = vector2_2;
                }
                init = true;
			}

            int index2 = Dust.NewDust(new Vector2((float)(projectile.position.X + 4.0), (float)(projectile.position.Y + 4.0)), projectile.width - 8, projectile.height - 8, 76, (float)(projectile.velocity.X * 0.200000002980232), (float)(projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.9f);
            Main.dust[index2].position = projectile.Center;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = projectile.velocity * 0.5f;
        }
		public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0.0f);
            for (int index1 = 0; index1 < 10; ++index1)
			{
			int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 76, 0.0f, 0.0f, 0, rgb, 1f);
			Main.dust[index2].noGravity = true;
			Dust dust1 = Main.dust[index2];
			dust1.velocity = dust1.velocity * 1.2f;
			Main.dust[index2].scale = 1.2f;
			Dust dust2 = Main.dust[index2];
			dust2.velocity = dust2.velocity - (projectile.oldVelocity * 0.3f);
			int index3 = Dust.NewDust(new Vector2((float) (projectile.position.X + 4.0), (float) (projectile.position.Y + 4.0)), projectile.width - 8, projectile.height - 8, 76, 0.0f, 0.0f, 0, rgb, 1.5f);
			Main.dust[index3].noGravity = true;
			Dust dust3 = Main.dust[index3];
			dust3.velocity = dust3.velocity* 2f;
			}
		}
		
	}
	
}
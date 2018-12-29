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
				init = true;
			}
			//Color rgb = new Color(Main.rand.Next(50, 256), Main.rand.Next(50, 256), Main.rand.Next(50, 256));
			 Main.PlaySound(SoundID.Item5, projectile.position);
            for (int index1 = 0; index1 < 4; ++index1)
            {
              int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 76, (float) projectile.velocity.X, (float) projectile.velocity.Y, 0, rgb, 0.9f);
              Main.dust[index2].noGravity = true;
              // ISSUE: explicit reference operation
              ((Vector2) @Main.dust[index2].velocity).Normalize();
              Dust dust1 = Main.dust[index2];
              Vector2 vector2_1 = new Vector2(dust1.velocity.X * -3f, dust1.velocity.Y * -3f);
              dust1.velocity = vector2_1;
              Dust dust2 = Main.dust[index2];
              Vector2 vector2_2 = new Vector2(dust2.velocity.X + projectile.velocity.X / 2, dust2.velocity.Y + projectile.velocity.Y / 2);
              dust2.velocity = vector2_2;
            }
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			projectile.velocity.Y += 0.2f;
		}
		public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Dig, projectile.Center, 1);
			int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 76, (float) 0, (float) 0, 0, rgb, 0.9f);
		}
		
	}
	
}
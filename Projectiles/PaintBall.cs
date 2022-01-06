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
        int timer = 0;
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.PainterPaintball);

			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.DamageType = DamageClass.Ranged;

		}
		
	

		public override void AI()
		{
            timer++;
			Projectile.ai[1] ++;
			int x1 = Main.rand.Next(7);
			if (!init)
			{
				if (x1 == 0)
				{
					rgb = new Color(230, 0, 0); //red
				}			
				else if (x1 == 1)
				{
					rgb = new Color(255, 255, 0); //yellow		
				}
				else if (x1 == 2)
				{
					rgb = new Color(0, 0, 240); //blue
				}
				else if(x1 == 3)
				{
					rgb = new Color(0, 255, 0); //lime
				}
				else if(x1 == 4)
				{
					//rgb = new Color(255, 105, 180); pink
					rgb = new Color(255, 70, 240); //fuschia
				}
				else if (x1 == 5)
				{
					//rgb = new Color(132, 112, 255); purple
					rgb = new Color(30, 255, 255); //teal
				}
				else if (x1 == 6)
				{
					//rgb = new Color(255, 165, 0);
					rgb = new Color(255, 120, 35); //orange
				}
                init = true;
			}
			Vector2 direction = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(25)) * 0.6f;
			Vector2 direction2 = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(-25)) * 0.6f;
			if (timer == 4)
			{
				for (int i = 0; i < 2; ++i)
			    {
			        if (i < 1)
			        {
					    int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, 76, direction.X, direction.Y, 0, rgb, 1.3f);
              			Main.dust[index2].position = Projectile.Center;
               			Main.dust[index2].noGravity = true;
			        }
					else 
					{
					    int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, 76, direction2.X, direction2.Y, 0, rgb, 1.3f);
              			Main.dust[index2].position = Projectile.Center;
               			Main.dust[index2].noGravity = true;
					}
			    }
			}
            if (timer > 2)
            {
                int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, 76, (float)(Projectile.velocity.X * 0.200000002980232), (float)(Projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.9f);
                Main.dust[index2].position = Projectile.Center;
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity = Projectile.velocity * 0.5f;
            }
        }
		public override void Kill(int timeLeft)
        {
            Terraria.Audio.SoundEngine.PlaySound(0, (int)Projectile.position.X, (int)Projectile.position.Y, 1, 1f, 0.0f);
            for (int index1 = 0; index1 < 8; ++index1)
			{
			int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 76, 0.0f, 0.0f, 0, rgb, 1f);
			Main.dust[index2].noGravity = true;
			Dust dust1 = Main.dust[index2];
			dust1.velocity = dust1.velocity * 1.2f;
			Dust dust2 = Main.dust[index2];
			dust2.velocity = dust2.velocity - (Projectile.oldVelocity * 0.3f);
			int index3 = Dust.NewDust(new Vector2((float) (Projectile.position.X + 4.0), (float) (Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, 76, 0.0f, 0.0f, 0, rgb, 1f);
			Main.dust[index3].noGravity = true;
			Dust dust3 = Main.dust[index3];
			dust3.velocity = dust3.velocity* 2f;
			}
		}
		
	}
	
}
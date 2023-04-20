using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles
{
	public class VineLeaf2 : ModProjectile
	{
        private bool init = false;
        Color rgb;
        public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Bullet);
            
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 225;
            Projectile.alpha = 0;

		}
		
	

		public override void AI()
		{
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);// |
            if (Projectile.velocity.X > 0)
            {
                Projectile.spriteDirection = 1;
            }
            else if (Projectile.velocity.X < 0)
            {
                Projectile.spriteDirection = -1;
            }

            if (!init)
            {
                rgb = new Color(50, 205, 50);

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

            int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, DustID.Snow, (float)(Projectile.velocity.X * 0.200000002980232), (float)(Projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.7f);
            Main.dust[index2].position = Projectile.Center;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = Projectile.velocity * 0.5f;
        }
			
		
		
		
	}
}
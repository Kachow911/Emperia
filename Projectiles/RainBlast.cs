using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class RainBlast : ModProjectile
    {
		private bool init = false;
		Color rgb = new Color(0, 0, 0);
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Blast");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 20;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {
            Projectile.ai[1] ++;
			int x1 = Main.rand.Next(7);
			if (!init)
			{
				rgb = new Color(83, 66, 180);

                for (int index1 = 0; index1 < 4; ++index1)
                {
                    int index3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 76, (float)Projectile.velocity.X, (float)Projectile.velocity.Y, 0, rgb, 1.1f);
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

            int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, 76, (float)(Projectile.velocity.X * 0.200000002980232), (float)(Projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.9f);
            Main.dust[index2].position = Projectile.Center;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = Projectile.velocity * 0.5f;
        }
        
    }
}
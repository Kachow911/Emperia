using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class VineLeaf : ModProjectile
    {
		private bool init = false;
		Color rgb;
		private int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vine Leaf");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 24;       //Projectile width
            Projectile.height = 14;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Melee;         // 
			Projectile.aiStyle = 0;
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 3;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the Projectile will face the corect way
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
				rgb = new Color(50,205,50);

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

            int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, 76, (float)(Projectile.velocity.X * 0.200000002980232), (float)(Projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.7f);
            Main.dust[index2].position = Projectile.Center;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = Projectile.velocity * 0.5f;
        }
		
		public override void Kill(int timeLeft)
        {

        	 for (int i = 0; i < 360; i += 36)
				{
				Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 46, 0f, 0f, 158, new Color(53f, 67f, 253f), 1f);
				Main.dust[num622].velocity += (vec *2f);
				Main.dust[num622].noGravity = true;
				}

		}
		
    }
}
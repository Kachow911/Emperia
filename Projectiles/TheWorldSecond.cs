using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class TheWorldSecond : ModProjectile
    {
		private int explodeRadius = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Phantasmal Yoyo");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 8;       //Projectile width
            Projectile.height = 8;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 100;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 0;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {           
			float maxHome = 200f;		// |
           explodeRadius++;
			if (explodeRadius % 2 == 0)
			{
				Projectile.alpha += 5;
			}
			
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 107, 0f, 0f, 91, new Color(255, 255, 255), 1.5f);
            Main.dust[dust].velocity *= 0.1f;
            if (Projectile.velocity == Vector2.Zero)
            {
                Main.dust[dust].velocity.Y -= 1f;
                Main.dust[dust].scale = 1.2f;
            }
			 else
            {
                Main.dust[dust].velocity += Projectile.velocity * 0.2f;
            }
			 Main.dust[dust].position.X = Projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].position.Y = Projectile.Center.Y + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].noGravity = true;
			float targetX = 0;
			float targetY = 0;
			bool targetNPC = false;
			for (int npcFinder = 0; npcFinder < 200; ++npcFinder)
                {
                    if (Main.npc[npcFinder].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[npcFinder].Center, 1, 1))
                    {
                        float num1 = Main.npc[npcFinder].position.X + (float)(Main.npc[npcFinder].width / 2);
                        float num2 = Main.npc[npcFinder].position.Y + (float)(Main.npc[npcFinder].height / 2);
                        float num3 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num1) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num2);
                        if (num3 < maxHome)
                        {
							maxHome = num3;
							targetX = num1;
							targetY = num2;
							targetNPC = true;
                           
                        }
						
                    }
                }
			if (targetNPC)
			{
					float num4 = Main.rand.Next(30, 43);
                    Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                    float num5 = targetX - vector35.X;
                    float num6 = targetY - vector35.Y;
                    float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
                    num7 = num4 / num7;
                    num5 *= num7;
                    num6 *= num7;
                    Projectile.velocity.X = (Projectile.velocity.X * 20f + num5) / 35f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num6) / 35f;
		
			}
				Projectile.velocity.X *= .97f;
				Projectile.velocity.Y *= .97f;	
           
		}
		public override void Kill(int timeLeft)
        {
			//
		}
        
    }
}
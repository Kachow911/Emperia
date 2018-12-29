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
			DisplayName.SetDefault("Phantasmal Yoyo");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 8;       //projectile width
            projectile.height = 8;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 100;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 0;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {           
			float maxHome = 200f;		// |
           explodeRadius++;
			if (explodeRadius % 2 == 0)
			{
				projectile.alpha += 5;
			}
			
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 107, 0f, 0f, 91, new Color(255, 255, 255), 1.5f);
            Main.dust[dust].velocity *= 0.1f;
            if (projectile.velocity == Vector2.Zero)
            {
                Main.dust[dust].velocity.Y -= 1f;
                Main.dust[dust].scale = 1.2f;
            }
			 else
            {
                Main.dust[dust].velocity += projectile.velocity * 0.2f;
            }
			 Main.dust[dust].position.X = projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].position.Y = projectile.Center.Y + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].noGravity = true;
			float targetX = 0;
			float targetY = 0;
			bool targetNPC = false;
			for (int npcFinder = 0; npcFinder < 200; ++npcFinder)
                {
                    if (Main.npc[npcFinder].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[npcFinder].Center, 1, 1))
                    {
                        float num1 = Main.npc[npcFinder].position.X + (float)(Main.npc[npcFinder].width / 2);
                        float num2 = Main.npc[npcFinder].position.Y + (float)(Main.npc[npcFinder].height / 2);
                        float num3 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num1) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num2);
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
                    Vector2 vector35 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                    float num5 = targetX - vector35.X;
                    float num6 = targetY - vector35.Y;
                    float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
                    num7 = num4 / num7;
                    num5 *= num7;
                    num6 *= num7;
                    projectile.velocity.X = (projectile.velocity.X * 20f + num5) / 35f;
                    projectile.velocity.Y = (projectile.velocity.Y * 20f + num6) / 35f;
		
			}
				projectile.velocity.X *= .97f;
				projectile.velocity.Y *= .97f;	
           
		}
		public override void Kill(int timeLeft)
        {
			//
		}
        
    }
}
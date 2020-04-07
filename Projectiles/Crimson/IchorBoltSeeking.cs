using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Crimson
{

    public class IchorBoltSeeking : ModProjectile
    {
		private int explodeRadius = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ichor Bolt");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 8;       //projectile width
            projectile.height = 8;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            //projectile.thrown = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 100;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
             target.AddBuff(BuffID.Ichor, 600);
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {           
			float maxHome = 400f;       // |
            for (int i = 0; i < 10; i++)
            {
                int num = Dust.NewDust(projectile.Center, 26, 26, 64, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num].alpha = 0;
                Main.dust[num].position.X = projectile.Center.X - projectile.velocity.X / 10f * (float)i;
                Main.dust[num].position.Y = projectile.Center.Y - projectile.velocity.Y / 10f * (float)i;
                Main.dust[num].noGravity = true;
            }
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
            for (int i = 0; i < 20; ++i)
            {
                int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 64, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 3.25f;
            }
            Main.PlaySound(SoundID.Dig, projectile.Center);
        }
        
    }
}
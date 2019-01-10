using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.TrueDaysVerge
{
	
    public class DVP2 : ModProjectile
    {
		private bool init = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pine Needle");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 28;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many projectile will penetrate
            projectile.timeLeft = 240;   //how many time projectile projectile has before disepire
            projectile.light = 0f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {          
float maxHome = 200f;		
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			if(Main.rand.Next(2) == 0)
			{
				int type = 0;
				if (Main.rand.Next(2) == 0)
					type = 58;
				else
					type = 176;
				int num250 = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X, projectile.position.Y - projectile.velocity.Y), projectile.width, projectile.height, type, (float)(projectile.direction * 2), 0f, 150, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num250].noGravity = true;
				Main.dust[num250].velocity *= 0f;
				Main.dust[num250].alpha = 0;
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
			}
        }
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Stratos
{
    public class StratosMeteorite : ModProjectile
    {
        bool init = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stratos Rock");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 14;       //projectile width
            projectile.height = 14;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            projectile.light = 0.1f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {
            float maxHome = 200f;// |
                                 //projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(5) == 2)
			{
				int num622 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, 180, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num622].velocity += projectile.velocity * 0.2f;
				Main.dust[num622].noGravity = true;
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
                projectile.velocity.X = (projectile.velocity.X * 20f + num5) / 25f;
                projectile.velocity.Y = (projectile.velocity.Y * 20f + num6) / 25f;

            }
            if (!init)
            {
                init = true;
                for (int i = 0; i < 360; i++)
                {
                    Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
                    if (i % 8 == 0)
                    {
                        int b = Dust.NewDust(projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 180);
                        Main.dust[b].noGravity = true;
                        Main.dust[b].velocity = vec;
                    }
                }
            }
        }
		
		public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Dig, projectile.Center);
        	for (int i = 0; i < 108; i += 36)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 180, 0f, 0f, 0, new Color(53f, 67f, 253f), 1f);
				Main.dust[num622].velocity += (vec *2f);
				Main.dust[num622].noGravity = true;
			}
		}
		
    }
}
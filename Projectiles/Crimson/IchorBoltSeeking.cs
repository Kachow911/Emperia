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
        {  //Projectile name
            Projectile.width = 8;       //Projectile width
            Projectile.height = 8;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Ranged;
//was thrown pre 1.4         // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 100;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
             target.AddBuff(BuffID.Ichor, 600);
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {           
			float maxHome = 400f;       // |
            for (int i = 0; i < 10; i++)
            {
                int num = Dust.NewDust(Projectile.Center, 26, 26, 64, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num].alpha = 0;
                Main.dust[num].position.X = Projectile.Center.X - Projectile.velocity.X / 10f * (float)i;
                Main.dust[num].position.Y = Projectile.Center.Y - Projectile.velocity.Y / 10f * (float)i;
                Main.dust[num].noGravity = true;
            }
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
            for (int i = 0; i < 20; ++i)
            {
                int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 64, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 3.25f;
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
        }
        
    }
}
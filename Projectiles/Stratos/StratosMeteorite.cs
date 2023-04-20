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
			// DisplayName.SetDefault("Stratos Rock");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 14;       //Projectile width
            Projectile.height = 14;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this Projectile has before disepire
            Projectile.light = 0.1f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the Projectile will face the corect way
        {
            float maxHome = 200f;// |
                                 //Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(5) == 2)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, DustID.DungeonSpirit, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num622].velocity += Projectile.velocity * 0.2f;
				Main.dust[num622].noGravity = true;
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
                Projectile.velocity.X = (Projectile.velocity.X * 20f + num5) / 25f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num6) / 25f;

            }
            if (!init)
            {
                init = true;
                for (int i = 0; i < 360; i++)
                {
                    Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
                    if (i % 8 == 0)
                    {
                        int b = Dust.NewDust(Projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), DustID.DungeonSpirit);
                        Main.dust[b].noGravity = true;
                        Main.dust[b].velocity = vec;
                    }
                }
            }
        }
		
		public override void Kill(int timeLeft)
        {
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
        	for (int i = 0; i < 108; i += 36)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0, new Color(53f, 67f, 253f), 1f);
				Main.dust[num622].velocity += (vec *2f);
				Main.dust[num622].noGravity = true;
			}
		}
		
    }
}
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
			// DisplayName.SetDefault("Pine Needle");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 28;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many Projectile will penetrate
            Projectile.timeLeft = 240;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {          
float maxHome = 200f;		
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			if(Main.rand.Next(2) == 0)
			{
				int type = 0;
				if (Main.rand.Next(2) == 0)
					type = 58;
				else
					type = 176;
				int num250 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X, Projectile.position.Y - Projectile.velocity.Y), Projectile.width, Projectile.height, type, (float)(Projectile.direction * 2), 0f, 150, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num250].noGravity = true;
				Main.dust[num250].velocity *= 0f;
				Main.dust[num250].alpha = 0;
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
			}
        }
    }
}
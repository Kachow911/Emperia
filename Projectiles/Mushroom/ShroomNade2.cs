using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{
	
    public class ShroomNade2 : ModProjectile
    {
    	private const float explodeRadius = 64;
		private const float pullRadius = 256;
		private int thing = 5;
		private bool doPull = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shroomnade");
		}
        public override void SetDefaults()
        {
            Projectile.width = 25;
            Projectile.height = 25;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 2;
            Projectile.timeLeft = 180;
            AIType = 48;
        }
        
        public override void AI()
        {
        	
            	Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 41, Projectile.velocity.X * 0.15f, Projectile.velocity.Y * 0.15f);
                if (doPull) 
				{
					Projectile.velocity.X = 0;
					Projectile.velocity.Y = 0;
					for (int i = 0; i < Main.npc.Length; i++)
                    {
			            if (Projectile.Distance(Main.npc[i].Center) < pullRadius)
						{
							Vector2 pullVectorThingy = Projectile.Center - Main.npc[i].Center;
							if (Main.npc[i].boss = false)
								Main.npc[i].velocity = pullVectorThingy * 0.05f;
			            }
					}
				}
        }
		 public override bool OnTileCollide(Vector2 oldVelocity)
        {
			Projectile.velocity.X = 0;
			Projectile.velocity.Y = 0;
			
			doPull = true;
            return false;
        }
        public override void Kill(int timeLeft)
        {
			 for (int i = 0; i < Main.npc.Length; i++)
            {
				if (Projectile.Distance(Main.npc[i].Center) < explodeRadius)
                    Main.npc[i].StrikeNPC(Projectile.damage, 0f, 0, false, false, false);
			}
        	 for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-explodeRadius, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                if (i % 8 == 0)
                {   //odd
                    Dust.NewDust(Projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(Projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20, vec.X * 2, vec.Y * 2);
                }
            }

            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item, Projectile.Center, 14);    //bomb explosion sound
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item, Projectile.Center, 21);    //swishy sound
		}
    }
}
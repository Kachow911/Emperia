using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Yeti
{

    public class YetiSnowball : ModProjectile
    {
		private int explodeRadius = 32;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yeti Snowball");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 56;       //Projectile width
            Projectile.height = 56;  //Projectile height
            Projectile.friendly = false;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 200;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.hostile = true;
			Projectile.alpha = 0;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {             
		   Projectile.rotation += .02f;		// |
           Projectile.velocity.Y += 0.02f;
		   if (Projectile.velocity.Y > 4)
			   Projectile.velocity.Y = 4;
		   if (Main.rand.NextBool(20))
			    Dust.NewDust(Projectile.Center + Projectile.velocity, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 51, 0, 0);
		}
		 public override void Kill(int timeLeft)
        {
            Projectile.velocity.Y += .03f;
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item, Projectile.Center, 14);
			for (int i = 0; i < Main.player.Length; i++)
            {
                if (Projectile.Distance(Main.player[i].Center) < explodeRadius)
                    Main.player[i].Hurt(Terraria.DataStructures.PlayerDeathReason.ByProjectile(Main.player[i].whoAmI, Projectile.whoAmI), 35, 0);
            }
			 for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-explodeRadius, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                if (i % 8 == 0)
                {   //odd
                    Dust.NewDust(Projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 51);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(Projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 192, vec.X * 2, vec.Y * 2);
                }
            }
		}
        
    }
}
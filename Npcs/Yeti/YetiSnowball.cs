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
        {  //projectile name
            projectile.width = 32;       //projectile width
            projectile.height = 32;  //projectile height
            projectile.friendly = false;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 200;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.hostile = true;
			projectile.alpha = 0;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {             
		   projectile.rotation += .02f;		// |
           projectile.velocity.Y += 0.1f;
		   if (projectile.velocity.Y > 4)
			   projectile.velocity.Y = 4;
		   if (Main.rand.NextBool(20))
			    Dust.NewDust(projectile.Center + projectile.velocity, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 51, 0, 0);
		}
		 public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Item, projectile.Center, 14);
			for (int i = 0; i < Main.player.Length; i++)
            {
                if (projectile.Distance(Main.player[i].Center) < explodeRadius)
                    Main.player[i].Hurt(Terraria.DataStructures.PlayerDeathReason.ByProjectile(Main.player[i].whoAmI, projectile.whoAmI), 35, 0);
            }
			 for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-explodeRadius, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                if (i % 8 == 0)
                {   //odd
                    Dust.NewDust(projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 51);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 192, vec.X * 2, vec.Y * 2);
                }
            }
		}
        
    }
}
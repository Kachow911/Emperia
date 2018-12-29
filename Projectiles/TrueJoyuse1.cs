using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class TrueJoyuse1 : ModProjectile
    {
		private int explodeRadius = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Holy Yoyo");
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
        {                                                           // |
           explodeRadius++;
			if (explodeRadius % 2 == 0)
			{
				projectile.alpha += 5;
			}
			
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 72, 0f, 0f, 91, new Color(255, 255, 255), .8f);
            Main.dust[dust].velocity *= 0.1f;
            if (projectile.velocity == Vector2.Zero)
            {
                Main.dust[dust].velocity.Y -= 1f;
                Main.dust[dust].scale = .6f;
            }
            else
            {
                Main.dust[dust].velocity += projectile.velocity * 0.2f;
            }
            Main.dust[dust].position.X = projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].position.Y = projectile.Center.Y + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].noGravity = true;
			projectile.velocity = projectile.velocity.RotatedByRandom(1);
			projectile.velocity *= .98f;
		}
		public override void Kill(int timeLeft)
        {
			//
		}
        
    }
}
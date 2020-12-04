using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class Splinter : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Splinter");
		}
        public override void SetDefaults()
        { 
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.melee = true;
			projectile.aiStyle = 0;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 80;
            projectile.extraUpdates = 0;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);// |
            if (projectile.velocity.X > 0)
			{
				projectile.spriteDirection = 1;	
			}
			else if (projectile.velocity.X < 0)
			{
				projectile.spriteDirection = -1;
			}
        }
		
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            crit = false;
        }

		public override void Kill(int timeLeft)
        {
		   Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 7);
		}
    }
}
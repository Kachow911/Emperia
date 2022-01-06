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
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
			Projectile.aiStyle = 0;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 80;
            Projectile.extraUpdates = 0;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);// |
            if (Projectile.velocity.X > 0)
			{
				Projectile.spriteDirection = 1;	
			}
			else if (Projectile.velocity.X < 0)
			{
				Projectile.spriteDirection = -1;
			}
        }
		
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            crit = false;
        }

		public override void Kill(int timeLeft)
        {
		   Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 7);
		}
    }
}
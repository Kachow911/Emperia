using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	
    public class Needle : ModProjectile
    {
		private bool init = false;
		private const float speedMax = 8;
        private const float speed = 2;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pine Needle");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 28;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many projectile will penetrate
            projectile.timeLeft = 540;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 0;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {             
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			projectile.velocity += Vector2.Normalize((Main.MouseWorld - projectile.Center) * speed);
            projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -speedMax, speedMax);
            projectile.velocity.Y = MathHelper.Clamp(projectile.velocity.Y, -speedMax, speedMax);
        }
    }
}
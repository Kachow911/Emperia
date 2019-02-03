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
            projectile.timeLeft = 510;   //how many time projectile projectile has before disepire
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
		public override void Kill(int timeLeft)
        {
            for (int index1 = 4; index1 < 8; ++index1)
            {
              float num1 = (float) (projectile.oldVelocity.X * (30.0 / (double) index1));
              float num2 = (float) (projectile.oldVelocity.Y * (30.0 / (double) index1));
              int index2 = Dust.NewDust(new Vector2((float) projectile.oldPosition.X - num1, (float) projectile.oldPosition.Y - num2), 8, 8, 3, (float) projectile.oldVelocity.X, (float) projectile.oldVelocity.Y, 100, Color.LightBlue, 1.8f);
              Main.dust[index2].noGravity = true;
              Dust dust1 = Main.dust[index2];
              dust1.velocity = dust1.velocity * 0.5f;
              int index3 = Dust.NewDust(new Vector2((float) projectile.oldPosition.X - num1, (float) projectile.oldPosition.Y - num2), 8, 8, 3, (float) projectile.oldVelocity.X, (float) projectile.oldVelocity.Y, 100, Color.LightBlue, 1.4f);
              Dust dust2 = Main.dust[index3];
              dust2.velocity = dust2.velocity * 0.5f;
            }
		
        }
    }
}
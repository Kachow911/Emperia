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
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 270;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 0;
            Main.projFrames[projectile.type] = 3;
        }
        public override void AI()
        {             
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			projectile.velocity += Vector2.Normalize((Main.MouseWorld - projectile.Center) * speed);
            projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -speedMax, speedMax);
            projectile.velocity.Y = MathHelper.Clamp(projectile.velocity.Y, -speedMax, speedMax);
            if (!init)
            {
                projectile.frame = Main.rand.Next(0, 2);
                init = true;
            }
        }
		public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 4; ++i)
            {
              int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), 8, 8, 93, 0f, 0f, 0, Color.LightBlue, 1f);
              Main.dust[index2].noGravity = true;
            }
        }
    }
}
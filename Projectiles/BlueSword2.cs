using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles
{
	public class BlueSword2 : ModProjectile
	{
        private bool init = false;
        Color rgb;
        public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Bullet);
            
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.timeLeft = 225;
            projectile.alpha = 0;

		}
		
	

		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 0.785f;
			projectile.alpha = 100 + (int)(Math.Cos(projectile.timeLeft) * 100);
			if (Main.rand.Next(2) == 0)
			{
				int num250 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 187, (float)(projectile.direction * 2), 0f, 150, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num250].noGravity = true;
				Main.dust[num250].velocity *= 0f;
			}
			Player player = Main.player[projectile.owner];
			if (projectile.Center.Y > player.Center.Y - player.height * 4)
			{
				projectile.tileCollide = true;
			}
		}
			
		
		
		
	}
}
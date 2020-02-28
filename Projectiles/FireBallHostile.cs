using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles
{
	public class FireBallHostile : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.PainterPaintball);

			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.timeLeft = 225;

		}
		
	

		public override void AI()
		{
			projectile.ai[1] ++;
			int x1 = Main.rand.Next(7);
			if (Main.rand.Next(2) == 0)
            {
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 258);
			}
		}
			public override bool OnTileCollide(Vector2 oldVelocity)
			{
				projectile.velocity = Vector2.Zero;
				return false;
			}
		
		
		
	}
}
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
			Projectile.CloneDefaults(ProjectileID.PainterPaintball);

			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 225;

		}
		
	

		public override void AI()
		{
			Projectile.ai[1] ++;
			int x1 = Main.rand.Next(7);
			if (Main.rand.Next(2) == 0)
            {
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.LavaMoss);
			}
		}
			public override bool OnTileCollide(Vector2 oldVelocity)
			{
				Projectile.velocity = Vector2.Zero;
				return false;
			}
		
		
		
	}
}
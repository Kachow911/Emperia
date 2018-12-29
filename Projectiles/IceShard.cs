using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles
{
	
	public class IceShard : ModProjectile
	{
		private bool hitGround;
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.PainterPaintball);
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.timeLeft = 225;
			projectile.alpha = 0;
		}
		public override void AI()
		{
			projectile.alpha = 0;
			if (Main.rand.NextBool(20))
			{
				Color rgb = new Color(135,206,250);
				int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 76, (float) projectile.velocity.X, (float) projectile.velocity.Y, 0, rgb, 0.9f);
			}
			projectile.velocity.Y += .3f;
			if (!hitGround)
				projectile.rotation += Main.rand.Next(10) * .01f;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Color rgb = new Color(135,206,250);
			int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 76, (float) projectile.velocity.X, (float) projectile.velocity.Y, 0, rgb, 0.9f);
			target.AddBuff(BuffID.Frostburn, 120);
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			hitGround = true;
			projectile.velocity = Vector2.Zero;
			return false;
		}
	}
}
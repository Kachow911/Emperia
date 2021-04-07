using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Yeti
{
	
	public class IceShard : ModProjectile
	{
		private bool hitGround;
		private int hitTimer;
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.PainterPaintball);
			projectile.friendly = true;
			projectile.penetrate = 1 + Main.rand.Next(3);
			projectile.magic = true;
			projectile.timeLeft = 900;
			projectile.alpha = 0;
			projectile.height = 2;
			projectile.width = 2;
			drawOriginOffsetY = -6;
		}
		public override void AI()
		{
			projectile.alpha = 0;
			if (Main.rand.NextBool(30))
			{
				Color rgb = new Color(135,206,250);
				int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, (float) projectile.velocity.X, (float) projectile.velocity.Y, 0, rgb, 0.9f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 0.5f;
			}
			projectile.velocity.Y += .3f;
			hitTimer--;
			if (!hitGround)
				projectile.rotation += Main.rand.Next(10) * .01f;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(BuffID.Frostburn, 120); //make frostburn 1/3 chance, if activates penetration goes down?
			hitTimer = 20;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (!hitGround) projectile.damage /= 2;
			hitGround = true;
			projectile.velocity = Vector2.Zero;
			return false;
		}
		public override bool? CanHitNPC(NPC target)
		{
            if (hitTimer > 0)
            {
                return false;
            }
            else return true;
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item, projectile.Center, 27);  
			Color rgb = new Color(135,206,250);
			int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, (float) projectile.velocity.X, (float) projectile.velocity.Y, 0, rgb, 0.9f);
			Main.dust[index2].noGravity = true;
		}
	}
}
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
			Projectile.CloneDefaults(ProjectileID.PainterPaintball);
			Projectile.friendly = true;
			Projectile.penetrate = 1 + Main.rand.Next(3);
			Projectile.DamageType = DamageClass.Magic;
			Projectile.timeLeft = 900;
			Projectile.alpha = 0;
			Projectile.height = 2;
			Projectile.width = 2;
			DrawOriginOffsetY = -6;
		}
		public override void AI()
		{
			Projectile.alpha = 0;
			if (Main.rand.NextBool(30))
			{
				Color rgb = new Color(135,206,250);
				int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 68, (float) Projectile.velocity.X, (float) Projectile.velocity.Y, 0, rgb, 0.9f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 0.5f;
			}
			Projectile.velocity.Y += .3f;
			hitTimer--;
			if (!hitGround)
				Projectile.rotation += Main.rand.Next(10) * .01f;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.Frostburn, 120); //make frostburn 1/3 chance, if activates penetration goes down?
			hitTimer = 20;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (!hitGround) Projectile.damage /= 2;
			hitGround = true;
			Projectile.velocity = Vector2.Zero;
			return false;
		}
		public override bool? CanHitNPC(NPC target)
		{
            if (hitTimer > 0)
            {
                return false;
            }
            else return null;
		}
		public override void Kill(int timeLeft)
		{
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);  
			Color rgb = new Color(135,206,250);
			int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 68, (float) Projectile.velocity.X, (float) Projectile.velocity.Y, 0, rgb, 0.9f);
			Main.dust[index2].noGravity = true;
		}
	}
}
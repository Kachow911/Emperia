using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles.Granite
{
	
	public class GraniteCanisterProj : ModProjectile
	{
		private bool hitGround;
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.PainterPaintball);
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 225;
			Projectile.alpha = 0;
			Projectile.damage = 0;
		}
		public override void AI()
		{
			Projectile.alpha = 0;
			if (Main.rand.NextBool(20))
			{
				Color rgb = new Color(135,206,250);
				int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Granite, (float) Projectile.velocity.X, (float) Projectile.velocity.Y, 0, rgb, 0.9f);
			}
			Projectile.velocity.Y += .3f;
			if (!hitGround)
				Projectile.rotation += Main.rand.Next(10) * .01f;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.alpha = 255;
			if (!hitGround)
			{
				Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(Main.rand.Next(-2, 2), -5), ModContent.Find<ModGore>("Gores/GraniteCanister").Type, 1f);
			}	
			hitGround = true;
			Projectile.velocity = Vector2.Zero;
			
			return false;
			
		}
        public override bool PreDrawExtras()
		{
			return true;
		}
	}
}
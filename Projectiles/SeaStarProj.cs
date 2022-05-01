using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Items.Sets.PreHardmode.Aquatic;

namespace Emperia.Projectiles
{
	public class SeaStarProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sea Star");

		}
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.PainterPaintball);
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.timeLeft = 200;
			Projectile.height = 24;
			Projectile.width = 24;
			Projectile.penetrate = 4;
			Projectile.extraUpdates = 1;
			Projectile.alpha = 0;
		}

		public override void AI()
		{

			Projectile.rotation += 0.2f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
				Projectile.Kill();
			else
			{
				Projectile.ai[0] += 0.1f;
				if (Projectile.velocity.X != oldVelocity.X)
					Projectile.velocity.X = -oldVelocity.X;

				if (Projectile.velocity.Y != oldVelocity.Y)
					Projectile.velocity.Y = -oldVelocity.Y;

				Projectile.velocity *= 0.5f;
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			if (Main.rand.Next(0, 4) == 0)
				Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<Items.Sets.PreHardmode.Aquatic.SeaStar>(), 1, false, 0, false, false);

			for (int i = 0; i < 8; ++i)
			{
				Color rgb = new Color(83, 66, 180);
				int index3 = Dust.NewDust(new Vector2((float) (Projectile.position.X + 4.0), (float) (Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, 76, 0.0f, 0.0f, 0, rgb, 1.5f);
			}
			Terraria.Audio.SoundEngine.PlaySound(2, (int)Projectile.position.X, (int)Projectile.position.Y, 27);
		}

	}
}

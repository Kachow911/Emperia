using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles.Desert
{
	public class PoundProj : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 100;
			projectile.height = 40;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 2;
			projectile.light = 0.75f;
			//projectile.extraUpdates = 1;
			projectile.ignoreWater = true;
			projectile.hide = true;

		}

			public override bool OnTileCollide(Vector2 oldVelocity)
			{
				projectile.velocity = Vector2.Zero;
				return false;
			}
		
		
		
	}
}
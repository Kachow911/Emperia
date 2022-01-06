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
			Projectile.width = 100;
			Projectile.height = 40;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 2;
			Projectile.light = 0.75f;
			//Projectile.extraUpdates = 1;
			Projectile.ignoreWater = true;
			Projectile.hide = true;

		}

			public override bool OnTileCollide(Vector2 oldVelocity)
			{
				Projectile.velocity = Vector2.Zero;
				return false;
			}
		
		
		
	}
}
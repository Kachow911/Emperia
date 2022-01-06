using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	public class FemurProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Oversized Femur");
		}
		int timer = 0;
		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = 3;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 700;
			Projectile.extraUpdates = 0;
			Projectile.tileCollide = true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			timer++;
			if (timer > 20)
            {
				Projectile.width = 46;
				Projectile.height = 66;
			}
		}

		public override void AI()
		{
			Projectile.scale = 0.9f;
		}

	}
}

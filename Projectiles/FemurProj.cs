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
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 3;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.magic = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 700;
			projectile.extraUpdates = 0;
			projectile.tileCollide = true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			timer++;
			if (timer > 20)
            {
				projectile.width = 46;
				projectile.height = 66;
			}
		}

		public override void AI()
		{
			projectile.scale = 0.9f;
		}

	}
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Emperia.Projectiles.Ethereal
{
	public class EtherealArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 1000;
			Projectile.tileCollide = false;
			Projectile.light = 1f; 
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ethereal Arrow");
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.GetGlobalNPC<MyNPC>().etherealDamages.Add(damageDone/2);
            target.GetGlobalNPC<MyNPC>().etherealCounts.Add(2);
			target.GetGlobalNPC<MyNPC>().etherealSource = Projectile;
		}

		public override void AI()
		{
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Vortex, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 0.8f);
			}
		}

	}
}
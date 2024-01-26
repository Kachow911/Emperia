using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Emperia.Projectiles.Ice
{
	public class ChillSword: ModProjectile
	{
		bool init = false;
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = -1;
			Projectile.friendly = false;
			Projectile.hostile = true;
			//Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 60;
			Projectile.tileCollide = true;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chillsteel Blade");
		}
		
		public override void OnKill(int timeLeft)
		{
			for (int i = 0; i < 30; ++i)
			{
			  int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.DungeonSpirit, 0.0f, 0.0f, 15, Color.White, 2f);
			  Main.dust[index2].noGravity = true;
			  Main.dust[index2].velocity *= 2.7f;
			}
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{	
			target.immune[Projectile.owner] = 2;
		}
		
		public override void AI()
		{
			if (!init)
            {
				init = true;
				for (int i = 0; i < 360; i++)
				{
					Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
					if (i % 8 == 0)
					{
						int b = Dust.NewDust(Projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), DustID.DungeonSpirit);
						Main.dust[b].noGravity = true;
						Main.dust[b].velocity = vec;
					}
				}
			}
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height,DustID.DungeonSpirit, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 0.8f);
			}
		}

	}
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Emperia.Projectiles.Twilight
{
	public class LeechArrow : ModProjectile
	{
		bool init = false;
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 1000;
			projectile.tileCollide = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leech Arrow");
		}
		
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item10, projectile.position);
			for (int index1 = 4; index1 < 31; ++index1)
			{
				float num1 = (float)(projectile.oldVelocity.X * (30.0 / (double)index1));
				float num2 = (float)(projectile.oldVelocity.Y * (30.0 / (double)index1));
				int index2 = Dust.NewDust(new Vector2((float)projectile.oldPosition.X - num1, (float)projectile.oldPosition.Y - num2), 8, 8, DustID.GoldCoin, (float)projectile.oldVelocity.X, (float)projectile.oldVelocity.Y, 100, default(Color), 1.6f);
				Main.dust[index2].noGravity = true;
				Dust dust1 = Main.dust[index2];
				dust1.velocity = dust1.velocity * 0.5f;
				int index3 = Dust.NewDust(new Vector2((float)projectile.oldPosition.X - num1, (float)projectile.oldPosition.Y - num2), 8, 8, DustID.GoldCoin, (float)projectile.oldVelocity.X, (float)projectile.oldVelocity.Y, 100, default(Color), 1.3f);
				Main.dust[index3].noGravity = true;
				Dust dust2 = Main.dust[index3];
				dust2.velocity = dust2.velocity * 0.5f;
			}

		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{		

			projectile.damage *= 2;
		}
		
		public override void AI()
		{
			if(!init)
            {
				init = true;
				if(Main.rand.NextBool(4))
                {
					projectile.penetrate = 2;
                }
            }
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.GoldCoin, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 0.8f);
			}
		}
	}
}
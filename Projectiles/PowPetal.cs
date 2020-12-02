using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	public class PowPetal : ModProjectile
	{
		bool latched;
		bool explode;
		bool init = false;
		int returntimer = 34;

		NPC npc;
		Vector2 offset;
		float rot;
		public override void SetDefaults()
		{
			projectile.width = 24;
			projectile.height = 10;
			projectile.alpha = 0;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 240;
			projectile.ignoreWater = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pow Petal");
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (!latched)
			{
				npc = target;
				offset = projectile.position - npc.position;
				latched = true;
				rot = projectile.rotation;
				returntimer = 36;
			}
		}

		public override void AI()
		{
			if (!init)
			{
				init = true;
				if (Main.player[projectile.owner].yoyoString)
					returntimer = 42;
			}
			Player player = Main.player[projectile.owner];
			Vector2 playerCenter = player.MountedCenter;
			if ((double)projectile.velocity.X < 0.0)
			{
				projectile.spriteDirection = -1;
				projectile.rotation = (float)Math.Atan2(-(double)projectile.velocity.Y, -(double)projectile.velocity.X);
			}
			else
			{
				projectile.spriteDirection = 1;
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
			}

			
			if (returntimer <= 0)
			{
				projectile.timeLeft = 0;
				explode = true;
			}
			if (latched && returntimer > 0)
			{
				projectile.rotation = rot;
				projectile.damage = 0;
				returntimer--;
				if (!npc.active)
				{
					explode = true;
					returntimer = 0;
				}
				//projectile.rotation = (float)Math.Atan2(-(double)offset.Y, -(double)offset.X);
				projectile.frameCounter++;
				projectile.velocity = Vector2.Zero;
				projectile.position = npc.position + offset;
				if (projectile.frameCounter >= 3)
				{
					projectile.frameCounter = 0;
					projectile.frame = (projectile.frame + 1) % 3;
				}

				if ((double)offset.X > 0.0)
				{
					projectile.spriteDirection = -1;
					//projectile.rotation = (float)Math.Atan2((double)offset.Y, (double)offset.X);
				}
				else
				{
					projectile.spriteDirection = 1;
					//projectile.rotation = (float)Math.Atan2(-(double)offset.Y, -(double)offset.X);
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 0);
				Main.dust[dust].scale = 1.5f;
				Main.dust[dust].noGravity = true;
			}
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
			return true;
		}
		public override void Kill(int timeLeft)
		{
			if (explode)
			{
				for (int i = 0; i < Main.npc.Length; i++)
				{
					if (projectile.Distance(Main.npc[i].Center) < 25 && !Main.npc[i].townNPC)
						Main.npc[i].StrikeNPCNoInteraction(14, 0f, 0, false, false, false);
				}
				for (int i = 0; i < 20; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 249, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 2.25f;
				}
			}
		}
	}
}

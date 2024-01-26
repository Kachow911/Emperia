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

		NPC NPC;
		Vector2 offset;
		float rot;
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.alpha = 0;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 240;
			Projectile.ignoreWater = true;
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pow Petal");
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (!latched)
			{
				NPC = target;
				offset = Projectile.position - NPC.position;
				latched = true;
				rot = Projectile.rotation;
				returntimer = 36;
			}
		}

		public override void AI()
		{
			if (!init)
			{
				init = true;
				Projectile.frame = Main.rand.Next(2);
			}
			Player player = Main.player[Projectile.owner];
			Vector2 playerCenter = player.MountedCenter;
			if ((double)Projectile.velocity.X < 0.0)
			{
				Projectile.spriteDirection = -1;
				Projectile.rotation = (float)Math.Atan2(-(double)Projectile.velocity.Y, -(double)Projectile.velocity.X);
			}
			else
			{
				Projectile.spriteDirection = 1;
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
			}

			
			if (returntimer <= 0)
			{
				Projectile.timeLeft = 0;
				explode = true;
			}
			if (latched && returntimer > 0)
			{
				Projectile.rotation = rot;
				Projectile.damage = 0;
				returntimer--;
				if (!NPC.active)
				{
					explode = true;
					returntimer = 0;
				}
				//Projectile.rotation = (float)Math.Atan2(-(double)offset.Y, -(double)offset.X);
				Projectile.velocity = Vector2.Zero;
				Projectile.position = NPC.position + offset;
				if (Projectile.frameCounter >= 3)
				{
					Projectile.frameCounter = 0;
					Projectile.frame = (Projectile.frame + 1) % 3;
				}

				if ((double)offset.X > 0.0)
				{
					Projectile.spriteDirection = -1;
					//Projectile.rotation = (float)Math.Atan2((double)offset.Y, (double)offset.X);
				}
				else
				{
					Projectile.spriteDirection = 1;
					//Projectile.rotation = (float)Math.Atan2(-(double)offset.Y, -(double)offset.X);
				}
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			for (int i = 0; i < 3; i++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TsunamiInABottle);
				Main.dust[dust].scale = 0.5f;
			}
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
			return true;
		}
		public override void OnKill(int timeLeft)
		{
			if (explode)
			{
				for (int i = 0; i < Main.npc.Length; i++)
				{
					if (Projectile.Distance(Main.npc[i].Center) < 25 && !Main.npc[i].townNPC)
						Main.npc[i].SimpleStrikeNPC(14, 0);
				}
				for (int i = 0; i < 8; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TsunamiInABottle, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 1.5f;
				}
				Terraria.Audio.SoundEngine.PlaySound(SoundID.Grass, Projectile.position);
			}
		}
	}
}

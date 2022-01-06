using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;
using Emperia.Projectiles;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Projectiles
{
	public class CoralBurstMain : ModProjectile
	{
		Vector2 burstVel = new Vector2(0,0);
		bool init = false;
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 15;
			Projectile.tileCollide = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coral Burst");
		}
		public override void AI()
		{
			if (!init)
			{
				burstVel = Projectile.velocity;
				Projectile.velocity = Vector2.Zero;
				init = true;
			}
			Player player = Main.player[Projectile.owner];
			if (Projectile.timeLeft % 5 == 0)
			{
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), player.Center.X, player.Center.Y, burstVel.X, burstVel.Y, ModContent.ProjectileType<CoralBurst>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0, 0);
			}
		}
		public override bool? CanHitNPC(NPC target)
		{
			return true;
		}
	}
	public class CoralBurst : ModProjectile
	{
		public override void SetDefaults()
		{
			//Projectile.damage = 10;
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 20;
			Projectile.tileCollide = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coral Burst");
		}
		
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 2; ++i)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 46, Projectile.velocity.X / 2, Projectile.velocity.Y / 2, 0, default(Color), 0.65f);
                Main.dust[dust].noGravity = false;
			}
		}
		public override void AI()
		{
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{	
			PlaySound(SoundID.Dig, Projectile.position);
			for (int i = 0; i < 3; i++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 46, (float) Projectile.velocity.X / 5, (float) Projectile.velocity.Y / 5, 0, default(Color), 0.8f);
            }
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			PlaySound(SoundID.Dig, Projectile.position);
			for (int i = 0; i < 3; i++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 46, (float) Projectile.velocity.X / 5, (float) Projectile.velocity.Y / 5, 0, default(Color), 0.8f);
            }
			return true;		
		}
	}
}
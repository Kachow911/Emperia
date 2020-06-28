using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Emperia.Projectiles
{
	public class CoralBurstMain : ModProjectile
	{
		Vector2 burstVel = new Vector2(0,0);
		bool init = false;
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 15;
			projectile.tileCollide = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coral Burst");
		}
		public override void AI()
		{
			if (!init)
			{
				burstVel = projectile.velocity;
				projectile.velocity = Vector2.Zero;
				init = true;
			}
			Player player = Main.player[projectile.owner];
			if (projectile.timeLeft % 5 == 0)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, burstVel.X, burstVel.Y, mod.ProjectileType("CoralBurst"), projectile.damage, projectile.knockBack, Main.myPlayer, 0, 0);
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
			projectile.damage = 10;
			projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 20;
			projectile.tileCollide = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coral Burst");
		}
		
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 2; ++i)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 46, (float) 0, 0, 0, default(Color), 0.5f);
                Main.dust[dust].noGravity = false;
			}
		}
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{	
			Main.PlaySound(SoundID.Dig, projectile.position);
			for (int i = 0; i < 3; i++)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 46, (float) projectile.velocity.X / 5, (float) projectile.velocity.Y / 5, 0, default(Color), 0.8f);
            }
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.Dig, projectile.position);
			for (int i = 0; i < 3; i++)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 46, (float) projectile.velocity.X / 5, (float) projectile.velocity.Y / 5, 0, default(Color), 0.8f);
            }
			return true;		
		}
	}
}
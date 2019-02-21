using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Summon
{
	public class EmberTyrant : ModProjectile
	{
        int timer = 0;
		int move = 0;
		bool slam = true;
		NPC targetNPC;
		int timeFromLastD = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ember Tyrant");
			Main.projFrames[base.projectile.type] = 1;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}
		int counter = 0;
		public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.Spazmamini);
            projectile.width = 46;
            projectile.height = 42;
            projectile.minion = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.netImportant = true;
            aiType = -1;
            projectile.alpha = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.minionSlots = 1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			
            return false;
        }

        public override void AI()
		{
			Player player = Main.player[projectile.owner];
            timer++;
			if (timeFromLastD < 60 && timeFromLastD > 0)
			{
				move = 0;
			}
			for (int i = 0; i < 200; i++)
			{
				if (player.Distance(Main.npc[i].Center) < 400f && move != 2 && !Main.npc[i].townNPC && Main.npc[i].life >= 1)
				{
					if (timeFromLastD > 60)
					{
						move = 1;
						targetNPC = Main.npc[i];
					}
				}
			}
			if (move == 0)
			{
				Vector2 targetPos = player.Center + new Vector2(0, -100);
				if (player.velocity.X == 0)
				{
					targetPos += new Vector2(16f * (float)Math.Cos(MathHelper.ToRadians(timer * 3)), 0);
				}
					SmoothMoveToPosition(targetPos, .1f, 6, 32);
			}
			if (move == 1)
			{
				if (Math.Abs(projectile.Center.X - targetNPC.Center.X) > 20f && projectile.Center.Y < targetNPC.Center.Y)
				{
					SmoothMoveToPosition(targetNPC.Center + new Vector2(0, -100), .1f, 6, 32);
				}
				else
				{
					if (timeFromLastD > 60)
					{
						projectile.velocity = Vector2.Zero;
						move = 2;
					}
				}
			}
			if (move == 2)
			{
				projectile.velocity.Y = 12f;
				counter ++;
				targetNPC = null;
				timeFromLastD = 0;
			}
			else if (move != 1)
			{
				timeFromLastD++;
			}
			if (counter >= 120)
			{
				move = 0;
				projectile.Center = player.Center + new Vector2(0, -100);
				for (int i = 0; i < 50; ++i) //Create dust b4 teleport
				{
					int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 258);
					int dust1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 258);
					Main.dust[dust1].scale = 1.5f;
					Main.dust[dust1].velocity *= 1.5f;
					int dust2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 258);
					Main.dust[dust2].scale = 1.5f;
				}
				counter = 0;
			}
			 bool flag64 = projectile.type == mod.ProjectileType("EmberTyrant");
			
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			if (flag64)
			{
				if (player.dead)
					modPlayer.EmberTyrant = false;

				if (modPlayer.EmberTyrant)
					projectile.timeLeft =2;

			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (move != 0)
			{
				for (int i = 0; i < 50; ++i) //Create dust b4 teleport
				{
					int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 258);
					int dust1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 258);
					Main.dust[dust1].scale = 1.5f;
					Main.dust[dust1].velocity *= 1.5f;
					int dust2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 258);
					Main.dust[dust2].scale = 1.5f;
				}
				projectile.velocity.Y *= -1;
				move = 0;
			}
		}
		private void SmoothMoveToPosition(Vector2 toPosition, float addSpeed, float maxSpeed, float slowRange = 64, float slowBy = .95f)
        {
            if (Math.Abs((toPosition - projectile.Center).Length()) >= slowRange)
            {
                projectile.velocity += Vector2.Normalize((toPosition - projectile.Center) * addSpeed);
                projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -maxSpeed, maxSpeed);
                projectile.velocity.Y = MathHelper.Clamp(projectile.velocity.Y, -maxSpeed, maxSpeed);
            }
            else
            {
                projectile.velocity *= slowBy;
            }
        }
	}
}
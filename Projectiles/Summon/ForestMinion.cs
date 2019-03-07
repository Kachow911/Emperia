using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Summon
{
	public class ForestMinion : ModProjectile
	{
        int timer = 0;
		int move = 0;
		NPC targetNPC;
		int timeFromLastShot = 0;
		int shootTimer = 0;
		private float rotate { get { return projectile.ai[1]; } set { projectile.ai[1] = value; } }
		private float rotate2 = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primordial Spirit");
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
			timeFromLastShot++;
			timer++;
			projectile.Center = new Vector2(player.Center.X, player.Center.Y - 100 + 0.5f * (float)Math.Cos(MathHelper.ToRadians(timer * 2)));
			rotate2 += 2f;
			for (int i = 0; i < 200; i++)
			{
				if (player.Distance(Main.npc[i].Center) < 400f && !Main.npc[i].townNPC && Main.npc[i].life >= 1 && Main.npc[i].type != NPCID.TargetDummy)
				{
					if (timeFromLastShot > 60)
					{
						shootTimer = 15;
						targetNPC = Main.npc[i];
					}
				}
			}
			if (shootTimer > 0)
			{
				timeFromLastShot = 0;
				if (shootTimer % 5 == 0)
				{
					Vector2 placePosition = projectile.Center;
					Vector2 direction = (targetNPC.Center - placePosition);
					direction.Normalize();
					Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * 7f, direction.Y * 7f, mod.ProjectileType("VineLeaf"), projectile.damage, 1, Main.myPlayer, 0, 0);	
				}
				shootTimer--;
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
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
		private float rotate { get { return Projectile.ai[1]; } set { Projectile.ai[1] = value; } }
		private float rotate2 = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primordial Spirit");
			Main.projFrames[base.Projectile.type] = 1;
			ProjectileID.Sets.MinionSacrificable[base.Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[base.Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
		int counter = 0;
		public override void SetDefaults()
		{
            Projectile.CloneDefaults(ProjectileID.Spazmamini);
            Projectile.width = 46;
            Projectile.height = 42;
            Projectile.minion = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.netImportant = true;
            AIType = -1;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.minionSlots = 1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			
            return false;
        }

        public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			timeFromLastShot++;
			timer++;
			Projectile.Center = new Vector2(player.Center.X, player.Center.Y - 100 + 0.5f * (float)Math.Cos(MathHelper.ToRadians(timer * 2)));
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
					Vector2 placePosition = Projectile.Center;
					Vector2 direction = (targetNPC.Center - placePosition);
					direction.Normalize();
					Projectile.NewProjectile(Projectile.InheritSource(Projectile), placePosition.X, placePosition.Y, direction.X * 7f, direction.Y * 7f, ModContent.ProjectileType<VineLeaf>(), Projectile.damage, 1, Main.myPlayer, 0, 0);	
				}
				shootTimer--;
			}
		}
		
		private void SmoothMoveToPosition(Vector2 toPosition, float addSpeed, float maxSpeed, float slowRange = 64, float slowBy = .95f)
        {
            if (Math.Abs((toPosition - Projectile.Center).Length()) >= slowRange)
            {
                Projectile.velocity += Vector2.Normalize((toPosition - Projectile.Center) * addSpeed);
                Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -maxSpeed, maxSpeed);
                Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y, -maxSpeed, maxSpeed);
            }
            else
            {
                Projectile.velocity *= slowBy;
            }
        }
	}
}
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Summon
{
	public class SharkMinion : ModProjectile
	{
        int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Depth Scrounger");
			Main.projFrames[base.projectile.type] = 8;
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}

		public override void SetDefaults()
		{
            projectile.CloneDefaults(ProjectileID.Spazmamini);
            projectile.width = 30;
            projectile.height = 34;
            projectile.minion = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.netImportant = true;
            aiType = ProjectileID.Spazmamini;
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
            /* timer+=5;
             Vector2 targetPos = player.Center + new Vector2((float) (50 * Math.Cos(MathHelper.ToRadians(timer))), -50f);
             projectile.velocity.X += Vector2.Normalize((targetPos - projectile.Center) * .05f).X;
             projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -5f, 5f);
             projectile.velocity.Y = Vector2.Normalize((targetPos - projectile.Center) * .05f).Y;
             projectile.velocity.Y = MathHelper.Clamp(projectile.velocity.Y, -5f, 5f);*/
            if (projectile.velocity.X > 0)
            {
                projectile.spriteDirection = -1;
                projectile.rotation = MathHelper.ToRadians(180 + MathHelper.ToDegrees(projectile.rotation));
            }
            else projectile.spriteDirection = 1;
            bool flag64 = projectile.type == mod.ProjectileType("SharkMinion");
			
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
			if (flag64)
			{
				if (player.dead)
					modPlayer.sharkMinion = false;

				if (modPlayer.sharkMinion)
					projectile.timeLeft =2;

			}
		}

	}
}
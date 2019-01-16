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
			if (projectile.velocity.Length() > 7f)
			{
				Color rgb = new Color(83, 66, 180);
				int index2 = Dust.NewDust(new Vector2((float)(projectile.position.X + 4.0), (float)(projectile.position.Y + 4.0)), projectile.width - 8, projectile.height - 8, 76, (float)(projectile.velocity.X * 0.200000002980232), (float)(projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.9f);
				Main.dust[index2].position = projectile.Center;
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity = projectile.velocity * 0.5f;
			}
           
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
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			
				Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
				for(int k = 0; k < projectile.oldPos.Length; k++)
				{
					Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
					Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
					spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
				}
			
			return true;
        }

	}
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class GauntletSkull : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gauntlet Skull");
		}
        public override void SetDefaults()
        { 
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.damage = 0;
            projectile.timeLeft = 1200;
            projectile.tileCollide = false;
            Main.projFrames[projectile.type] = 3;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
			Vector2 direction = player.Center - projectile.Center;
            if (!player.HasBuff(mod.BuffType("SkullBuff")))
            {
                projectile.timeLeft = 0;
            }
            projectile.frameCounter++;
            {
                projectile.velocity.X = direction.X * 0.05f;
                projectile.velocity.Y = direction.Y * 0.05f;
            }
            if (projectile.velocity.X < 0)
            {
                projectile.spriteDirection = 1;
            }
            else projectile.spriteDirection = -1;
			if (projectile.frameCounter >= 3)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 3;
			}
            if (projectile.timeLeft % 4 == 0) {
                int flame = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
                Main.dust[flame].velocity *= 0f;
                Main.dust[flame].noGravity = true;
                Main.dust[flame].scale *= 1.5f;
            } 
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 15; ++i)
				{
					int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 3.25f;
                    Main.dust[index2].scale *= 2f;
				}
        }
        public override bool? CanCutTiles()
        {
			return false;
		}
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Projectiles
{
    public class GauntletSkull : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gauntlet Skull");
		}
        public override void SetDefaults()
        { 
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.damage = 0;
            Projectile.timeLeft = 1200;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 3;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
			Vector2 direction = player.Center - Projectile.Center;
            if (!player.HasBuff(ModContent.BuffType<SkullBuff>()))
            {
                Projectile.timeLeft = 0;
            }
            Projectile.frameCounter++;
            {
                Projectile.velocity.X = direction.X * 0.05f;
                Projectile.velocity.Y = direction.Y * 0.05f;
            }
            if (Projectile.velocity.X < 0)
            {
                Projectile.spriteDirection = 1;
            }
            else Projectile.spriteDirection = -1;
			if (Projectile.frameCounter >= 3)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 3;
			}
            if (Projectile.timeLeft % 4 == 0) {
                int flame = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6);
                Main.dust[flame].velocity *= 0f;
                Main.dust[flame].noGravity = true;
                Main.dust[flame].scale *= 1.5f;
            } 
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 15; ++i)
				{
					int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6);
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
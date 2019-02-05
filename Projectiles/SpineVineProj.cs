using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class SpineVineProj : ModProjectile
    {
		bool init = false;
		Color rgb;
		int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spine Vine");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 16;       //projectile width
            projectile.height = 16;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.melee = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            timer++;
			if (timer % 5 ==0 )
			{
				projectile.velocity.Y += 0.2f;
			}
			if (projectile.velocity.X > 0)
			{
				projectile.spriteDirection = 1;
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + (float) (3.14 / 4);
			}
			else if (projectile.velocity.X < 0)
			{
				projectile.spriteDirection = -1;
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + (float)((3.14) - (3.14 / 4));
			}
			
			if (!init)
			{
				rgb = new Color(50,205,50);

                for (int index1 = 0; index1 < 4; ++index1)
                {
                    int index3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 76, (float)projectile.velocity.X, (float)projectile.velocity.Y, 0, rgb, 1.1f);
                    Main.dust[index3].noGravity = true;
                    Main.dust[index3].velocity = projectile.Center - Main.dust[index3].position;
                    ((Vector2)@Main.dust[index3].velocity).Normalize();
                    Dust dust1 = Main.dust[index3];
                    Vector2 vector2_1 = dust1.velocity * -3f;
                    dust1.velocity = vector2_1;
                    Dust dust2 = Main.dust[index3];
                    Vector2 vector2_2 = dust2.velocity + (projectile.velocity / 2f);
                    dust2.velocity = vector2_2;
                }
                init = true;
			}

            int index2 = Dust.NewDust(new Vector2((float)(projectile.position.X + 4.0), (float)(projectile.position.Y + 4.0)), projectile.width - 8, projectile.height - 8, 76, (float)(projectile.velocity.X * 0.200000002980232), (float)(projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.7f);
            Main.dust[index2].position += projectile.velocity.RotatedBy(1.570796, new Vector2());
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = projectile.velocity.RotatedBy(1.570796, new Vector2()) * 0.33f + projectile.velocity / 4f;
			int index5 = Dust.NewDust(new Vector2((float)(projectile.position.X + 4.0), (float)(projectile.position.Y + 4.0)), projectile.width - 8, projectile.height - 8, 76, (float)(projectile.velocity.X * 0.200000002980232), (float)(projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.7f);
            Main.dust[index5].position += projectile.velocity.RotatedBy(1.570796, new Vector2());
            Main.dust[index5].noGravity = true;
            Main.dust[index5].velocity = projectile.velocity.RotatedBy(-1.570796, new Vector2()) * 0.33f + projectile.velocity / 4f;
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(BuffID.Poisoned, 240);
		}
    }
}
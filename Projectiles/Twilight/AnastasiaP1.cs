using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Emperia;

namespace Emperia.Projectiles.Twilight
{
    public class AnastasiaP1 : ModProjectile
    {
		int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twilight Spine");
		}
        public override void SetDefaults()
        {
			projectile.width = 30;
			projectile.height = 32;
			projectile.aiStyle = 4;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.alpha = 255;
			projectile.ignoreWater = true;
			projectile.magic = true;
		}

        public override void AI()
		{
			timer++;
			int num52 = projectile.type;
			
			if (projectile.ai[1] >= 6f)
			{
				num52 = mod.ProjectileType("AnastasiaP2");
			}
			if (projectile.ai[1] != -1 && timer > 2)
			{
				int num53 = Projectile.NewProjectile(projectile.position.X + projectile.velocity.X * .9f + (float)(projectile.width / 2), projectile.position.Y + projectile.velocity.Y * .9f + (float)(projectile.height / 2), projectile.velocity.X, projectile.velocity.Y, num52, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
				Main.projectile[num53].damage = projectile.damage;
				Main.projectile[num53].ai[1] = projectile.ai[1] + 1f;
				projectile.ai[1] = -1;
				NetMessage.SendData(27, -1, -1, null, num53, 0f, 0f, 0f, 0, 0, 0);
			}

		}
		public override void Kill(int timeLeft)
        {
			/*for (int i = 0; i < 360; i += 10)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-16, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, 0f, 0f, 91, new Color(255, 255, 255), 1.5f);
                Main.dust[num622].velocity += (vec * 2f);
                Main.dust[num622].noGravity = true;
            }*/
	     }
       /* public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }*/
	
    }
}

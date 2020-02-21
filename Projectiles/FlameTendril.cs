using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class FlameTendril : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame Tendril");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 40;       //projectile width
            projectile.height = 40;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 3;      //how many npc will penetrate
            projectile.timeLeft = 100;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {
			Vector2 center10 = projectile.Center;
			projectile.scale = 1f - projectile.localAI[0];
			projectile.width = (int)(20f * projectile.scale);
			projectile.height = projectile.width;
			projectile.position.X = center10.X - (float)(projectile.width / 2);
			projectile.position.Y = center10.Y - (float)(projectile.height / 2);
			if ((double)projectile.localAI[0] < 0.1)
			{
				projectile.localAI[0] += 0.01f;
			}
			else
			{
				projectile.localAI[0] += 0.025f;
			}
			if (projectile.localAI[0] >= 0.95f)
			{
				projectile.Kill();
			}
			projectile.velocity.X = projectile.velocity.X + projectile.ai[0] * 1.5f;
			projectile.velocity.Y = projectile.velocity.Y + projectile.ai[1] * 1.5f;
			if (projectile.velocity.Length() > 16f)
			{
				projectile.velocity.Normalize();
				projectile.velocity *= 16f;
			}
			projectile.ai[0] *= 1.05f;
			projectile.ai[1] *= 1.05f;
			if (projectile.scale < 1f)
			{
				int num890 = 0;
				while ((float)num890 < projectile.scale * 10f)
				{
					int num891 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.1f);
					Main.dust[num891].position = (Main.dust[num891].position + projectile.Center) / 2f;
					Main.dust[num891].noGravity = true;
					Dust dust3 = Main.dust[num891];
					dust3.velocity *= 0.1f;
					dust3 = Main.dust[num891];
					dust3.velocity -= projectile.velocity * (1.3f - projectile.scale);
					Main.dust[num891].fadeIn = (float)(100 + projectile.owner);
					dust3 = Main.dust[num891];
					dust3.scale += projectile.scale * 0.75f;
					int num3 = num890;
					num890 = num3 + 1;
				}
				return;
			}

		}
	
        
    }
}
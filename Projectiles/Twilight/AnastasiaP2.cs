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
    public class AnastasiaP2 : ModProjectile
    {
       
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

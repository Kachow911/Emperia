using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class ConiferociousProj : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coniferocious");
		}
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 3600;
            projectile.ignoreWater = false;
            projectile.aiStyle = 3;
        }
        public override void AI()
        {
			if (Main.rand.Next(4) == 0)
			{
				int num622 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, 93, 0f, 0f, 0, default, 1.3f);
				Main.dust[num622].velocity += projectile.velocity * 0.2f;
				Main.dust[num622].noGravity = true;
			}
        }
    }
}

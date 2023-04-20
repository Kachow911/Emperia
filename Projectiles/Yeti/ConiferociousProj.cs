using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Yeti
{

    public class ConiferociousProj : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Coniferocious");
		}
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
            Projectile.ignoreWater = false;
            Projectile.aiStyle = 3;
        }
        public override void AI()
        {
			if (Main.rand.Next(4) == 0)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, DustID.GreenMoss, 0f, 0f, 0, default, 1.3f);
				Main.dust[num622].velocity += Projectile.velocity * 0.2f;
				Main.dust[num622].noGravity = true;
			}
        }
    }
}

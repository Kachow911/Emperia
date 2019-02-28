using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class InquisitionBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 44;
            //projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.melee = true;
            projectile.timeLeft = 360;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inquisitor's Blade");
        }

        public override void Kill(int timeLeft)
        {
            int num622 = Dust.NewDust(new Vector2(projectile.position.X, (float)((double)projectile.position.Y + (double)projectile.height - 16.0)), projectile.width, 16, DustID.GoldCoin, 0.0f, 0.0f, 0, new Color(), 1f);
        }

        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
    }
}
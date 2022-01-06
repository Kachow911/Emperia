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
            Projectile.width = 22;
            Projectile.height = 44;
            //Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 360;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inquisitor's Blade");
        }

        public override void Kill(int timeLeft)
        {
            int num622 = Dust.NewDust(new Vector2(Projectile.position.X, (float)((double)Projectile.position.Y + (double)Projectile.height - 16.0)), Projectile.width, 16, DustID.GoldCoin, 0.0f, 0.0f, 0, new Color(), 1f);
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
    }
}
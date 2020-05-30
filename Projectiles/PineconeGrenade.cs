using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class PineconeGrenade : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("PineconeGrenade");
		}
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 40;
            projectile.ignoreWater = false;
            projectile.aiStyle = 2;
        }
		public override void Kill(int timeLeft)
		{
            for (int i = 0; i < 7; i++)
            {
			    int smokeDust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0.0f, 0.0f, 60, new Color(53f, 67f, 253f), 1.5f);
				Main.dust[smokeDust].velocity *= 3.5f;
				Main.dust[smokeDust].noGravity = true;
			    int sparkDust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0.0f, 0.0f, 0, default(Color), 1.5f);
				Main.dust[sparkDust].velocity *= 2f;
                int barkDust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 155, 0.0f, 0.0f, 0, default(Color), 1.3f);
				Main.dust[barkDust].velocity *= 4f;
                Main.dust[barkDust].noGravity = true;
            }
            for (int i = 0; i < 4; i++)
            {
                Gore.NewGore(projectile.position, new Vector2(0.25f, 0.25f).RotatedByRandom(MathHelper.ToRadians(360)), Main.rand.Next(61, 63), 1f);
            }
            Main.PlaySound(SoundID.Item14, projectile.Center);
		}
    }
}

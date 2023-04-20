using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Projectiles.Yeti
{
	
    public class Needle : ModProjectile
    {
		private bool init = false;
		private const float speedMax = 8;
        private const float speed = 2;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pine Needle");
		}
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 270;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 0;
            Main.projFrames[Projectile.type] = 3;
        }
        public override void AI()
        {             
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			Projectile.velocity += Vector2.Normalize((Main.MouseWorld - Projectile.Center) * speed);
            Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -speedMax, speedMax);
            Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y, -speedMax, speedMax);
            if (!init)
            {
                Projectile.frame = Main.rand.Next(0, 2);
                init = true;
            }
            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = 40;
                PlaySound(SoundID.Item7 with { Pitch = 0.2f, Volume = 0.8f, MaxInstances = 4}, Projectile.position);
            }
        }
		public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 4; ++i)
            {
              int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 8, 8, DustID.GreenMoss, 0f, 0f, 0, Color.LightBlue, 1f);
              Main.dust[index2].noGravity = true;
            }
        }
    }
}
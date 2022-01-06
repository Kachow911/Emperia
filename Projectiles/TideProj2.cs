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

namespace Emperia.Projectiles
{
    public class TideProj2 : ModProjectile
    {
        private const float explodeRadius = 32;
        private float rotate { get { return Projectile.ai[1]; } set { Projectile.ai[1] = value; } }
		private float rotate2 = 0;
        Color rgb = new Color(83, 66, 180);
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Water Bolt");
		}
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            //Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.light = 0.75f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 75;
            Projectile.aiStyle = -1;
        }

        public override void AI()
		{
			Player player = Main.player[Projectile.owner];
            Vector2 rotatePosition = Vector2.Transform(new Vector2(128, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(rotate * 60 + rotate2))) + player.Center;
            Projectile.Center = rotatePosition;

            rotate2 += 1f;
            int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, 76, (float)(Projectile.velocity.X * 0.200000002980232), (float)(Projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.9f);
            Main.dust[index2].position = Projectile.Center;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = Projectile.velocity * 0.5f;

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.Kill();
        }
		
    }
}

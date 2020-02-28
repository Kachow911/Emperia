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
        private float rotate { get { return projectile.ai[1]; } set { projectile.ai[1] = value; } }
		private float rotate2 = 0;
        Color rgb = new Color(83, 66, 180);
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Water Bolt");
		}
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            //projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.light = 0.75f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 75;
            projectile.aiStyle = -1;
        }

        public override void AI()
		{
			Player player = Main.player[projectile.owner];
            Vector2 rotatePosition = Vector2.Transform(new Vector2(128, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(rotate * 60 + rotate2))) + player.Center;
            projectile.Center = rotatePosition;

            rotate2 += 1f;
            int index2 = Dust.NewDust(new Vector2((float)(projectile.position.X + 4.0), (float)(projectile.position.Y + 4.0)), projectile.width - 8, projectile.height - 8, 76, (float)(projectile.velocity.X * 0.200000002980232), (float)(projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.9f);
            Main.dust[index2].position = projectile.Center;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = projectile.velocity * 0.5f;

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }
		
    }
}

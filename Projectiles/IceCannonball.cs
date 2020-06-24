using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	
    public class IceCannonball : ModProjectile
    {
		private bool init = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Icicle");
		}
        public override void SetDefaults()
        {
            projectile.width = 6;       //projectile width
            projectile.height = 6;  //projectile height
            projectile.hostile = false;      //make that the projectile will not damage you
			projectile.friendly = true;
            projectile.ranged = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many projectile will penetrate
            projectile.timeLeft = 300;   //how many time projectile projectile has before disepire
            projectile.light = 0f;    // projectile light
            projectile.ignoreWater = false;
			projectile.alpha = 0;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (Main.rand.NextBool(4))
			{
				int index2 = Dust.NewDust(projectile.oldPosition, projectile.width, projectile.height, 68, (float) projectile.velocity.X, (float) projectile.velocity.Y, 0, default(Color), 0.9f);
				Main.dust[index2].noGravity = true;
			}
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) > 0)
            {
			    target.AddBuff(BuffID.Frostburn, 120);
            }
            Vector2 perturbedSpeed = new Vector2(projectile.velocity.X * 0.6f, projectile.velocity.Y * 0.6f).RotatedByRandom(MathHelper.ToRadians(10));
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("IceShardTiny"), projectile.damage / 3 , 0, Main.myPlayer, 0, 0);

		}
        public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 3; i++)
			{
				int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, (float) projectile.velocity.X / 10, (float) projectile.velocity.Y / 10, 0, default(Color), 0.9f);

            }
            Main.PlaySound(SoundID.Item27, projectile.Center);
		}
    }
}
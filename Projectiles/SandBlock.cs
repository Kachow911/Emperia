using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	
    public class SandBlock : ModProjectile
    {
		private bool init = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stray Block");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 28;  //projectile height
            projectile.friendly = false;      //make that the projectile will not damage you
			projectile.hostile = true;
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many projectile will penetrate
            projectile.timeLeft = 240;   //how many time projectile projectile has before disepire
            projectile.light = 0f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 0;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {             
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			for (int i = 0; i < 2; i++)
			{
				int dust = Dust.NewDust(new Vector2(projectile.position.X, (float) ((double) projectile.position.Y + (double) projectile.height - 16.0)), projectile.width, 16, 85, 0.0f, 0.0f, 0, new Color(), 1f);
				Main.dust[dust].velocity *= 0.1f;
				if (projectile.velocity == Vector2.Zero)
				{
					Main.dust[dust].velocity.Y -= 1f;
					Main.dust[dust].scale = 1.2f;
				}
				else
				{
					Main.dust[dust].velocity += projectile.velocity * 0.2f;
				}
				Main.dust[dust].position.X = projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
				Main.dust[dust].position.Y = projectile.Center.Y + (float)Main.rand.Next(-2, 3);
				Main.dust[dust].noGravity = true;
            }
			projectile.velocity.Y += 0.15f;
        }
    }
}
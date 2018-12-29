using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class FateFlameX : ModProjectile
    {
		private int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fate's Flames");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 64;       //projectile width
            projectile.height = 64;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 200;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                                                           // |
            /*projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 191, 0f, 0f, 91, new Color(89, 249, 116), 1.5f);
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
            Main.dust[dust].noGravity = true;*/
			projectile.velocity *= .95f;
			timer++;
			if (timer % 5 == 0)
			{
			for (int i = 0; i < 20; i+=2)
			{
				
			int dust = Dust.NewDust(projectile.Center + new Vector2(i, i), projectile.width, projectile.height, 191, 0f, 0f, 91, new Color(89, 249, 116), 1f);
			int dust1 = Dust.NewDust(projectile.Center + new Vector2(-i, i), projectile.width, projectile.height, 191, 0f, 0f, 91, new Color(89, 249, 116), 1f);
			int dust2 = Dust.NewDust(projectile.Center + new Vector2(i, -i), projectile.width, projectile.height, 191, 0f, 0f, 91, new Color(89, 249, 116), 1f);
			int dust3 = Dust.NewDust(projectile.Center + new Vector2(-i, -i), projectile.width, projectile.height, 191, 0f, 0f, 91, new Color(89, 249, 116), 1f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust1].noGravity = true;
			Main.dust[dust2].noGravity = true;
			Main.dust[dust3].noGravity = true;
			}
			}
		}
		public override void Kill(int timeLeft)
        {
			//
		}
        
    }
}
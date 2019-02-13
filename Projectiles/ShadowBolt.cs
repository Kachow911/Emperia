using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class ShadowBolt : ModProjectile
    {
		private int explodeRadius = 32;
		int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Bolt");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 8;       //projectile width
            projectile.height = 8;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 100;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {       
			timer++;		
			int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Shadowflame, 0f, 0f);
			Main.dust[dust].scale = 1.5f;
			Main.dust[dust].velocity *= 0f;
			if (timer % 15 == 0)
			{
					float count = 25.0f;
                    for (int k = 0; (double)k < (double)count; k++)
                    {
                        Vector2 vector2 = (Vector2.UnitX * 0.0f + -Vector2.UnitY.RotatedBy((double)k * (6.22 / (double)count), new Vector2()) * new Vector2(2.0f, 8.0f)).RotatedBy((double)projectile.velocity.ToRotation(), new Vector2());
                        int dust1 = Dust.NewDust(projectile.Center - new Vector2(0.0f, 4.0f), 0, 0, DustID.Shadowflame, 0.0f, 0.0f, 0, new Color(), 1.0f);
                        Main.dust[dust].scale = 1.25f;
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].position = projectile.Center + vector2;
                        Main.dust[dust].velocity = projectile.velocity * 0.0f + vector2.SafeNormalize(Vector2.UnitY) * 1.0f;
                    }
			}
        }
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < Main.npc.Length; i++)
            {
				if (projectile.Distance(Main.npc[i].Center) < explodeRadius)
                    Main.npc[i].AddBuff(BuffID.ShadowFlame, 120);
			}
			for (int i = 0; i < 360; i += 10)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-explodeRadius, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Shadowflame, 0f, 0f, 91, new Color(255, 255, 255), 1.5f);
                Main.dust[num622].velocity += (vec * 2f);
                Main.dust[num622].velocity += projectile.velocity;
                Main.dust[num622].noGravity = true;
            }
	     }
        
    }
}
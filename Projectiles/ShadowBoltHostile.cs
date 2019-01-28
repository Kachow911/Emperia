using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class ShadowBoltHostile : ModProjectile
    {
		private int explodeRadius = 32;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Bolt");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 8;       //projectile width
            projectile.height = 8;  //projectile height
            projectile.friendly = false;      //make that the projectile will not damage you
            Projectiles.ShadowBoltHostile = true;
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
			int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Shadowflame, 0f, 0f);
			Main.dust[dust].scale = 1.5f;
			Main.dust[dust].velocity *= 0f;

        }
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < Main.player.Length; i++)
            {
				if (projectile.Distance(Main.player[i].Center) < explodeRadius)
                    Main.player[i].AddBuff(BuffID.ShadowFlame, 120);
			}
			for (int i = 0; i < 360; i += 10)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-explodeRadius, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Shadowflame, 0f, 0f, 91, new Color(255, 255, 255), 1.5f);
                Main.dust[num622].velocity += (vec * 2f);
                Main.dust[num622].noGravity = true;
            }
	     }
        
    }
}
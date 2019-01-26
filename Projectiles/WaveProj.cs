using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class WaveProj : ModProjectile
    {
		private int explodeRadius = 100;
        int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wave");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 32;       //projectile width
            projectile.height = 32;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 3;      //how many npc will penetrate
            projectile.timeLeft = 100;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.ignoreWater = true;
			projectile.alpha = 255;
			projectile.scale = 1f;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {
            timer += 8;
			int dust = Dust.NewDust(projectile.position + new Vector2(10f * (float)Math.Cos(MathHelper.ToRadians(timer)), 0f).RotatedBy(projectile.rotation + 1.57), 8, 8, 76, 0f, 0f, 91, new Color(83, 66, 180), 1f);
            int dust1 = Dust.NewDust(projectile.position + new Vector2(10f * (float)Math.Cos(MathHelper.ToRadians(timer)), 0f).RotatedBy(projectile.rotation - 1.57), 8, 8, 76, 0f, 0f, 91, new Color(83, 66, 180), 1f);
            Main.dust[dust].velocity = Vector2.Zero;
            Main.dust[dust1].velocity = Vector2.Zero;
            /*int dust2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 66, 0f, 0f, 91, new Color(83, 66, 180), 1.5f);
            Main.dust[dust2].velocity = projectile.velocity;
            Main.dust[dust2].noGravity = true;*/

        }
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 360; i += 10)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-10, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(projectile.position, 8, 8, 76, 0f, 0f, 91, new Color(83, 66, 180), 1.5f);
                Main.dust[num622].velocity += (vec * 2f);
                Main.dust[num622].velocity += projectile.velocity;
                Main.dust[num622].noGravity = true;
            }
	     }
        
    }
}
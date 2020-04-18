using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Stratos
{

    public class StratosSpark : ModProjectile
    {
		private int explodeRadius = 100;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stratos Spark");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 8;       //projectile width
            projectile.height = 8;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 3;      //how many npc will penetrate
            projectile.timeLeft = 100;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                                                           
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 180, 0f, 0f, 91, new Color(65, 250, 247), 1f);
            Main.dust[dust].position += projectile.velocity.RotatedBy(1.57);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity = projectile.velocity.RotatedBy(0.5);
            int dust1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 180, 0f, 0f, 91, new Color(65, 250, 247), 1f);
            Main.dust[dust].position += projectile.velocity.RotatedBy(-1.57);
            Main.dust[dust1].noGravity = true;
            Main.dust[dust1].velocity = projectile.velocity.RotatedBy(-0.5);
            int dust2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 180, 0f, 0f, 91, new Color(65, 250, 247), 1.5f);
            Main.dust[dust2].velocity = projectile.velocity;
            Main.dust[dust2].noGravity = true;

        }
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 360; i += 10)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-10, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 180, 0f, 0f, 91, new Color(65, 250, 247), 1.5f);
                Main.dust[num622].velocity += (vec * 2f);
                Main.dust[num622].velocity += projectile.velocity;
                Main.dust[num622].noGravity = true;
            }
	     }
        
    }
}
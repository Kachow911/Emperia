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
        {  //Projectile name
            Projectile.width = 32;       //Projectile width
            Projectile.height = 32;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 3;      //how many NPC will penetrate
            Projectile.timeLeft = 100;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
			Projectile.scale = 1f;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {
            timer += 8;
			int dust = Dust.NewDust(Projectile.position + new Vector2(10f * (float)Math.Cos(MathHelper.ToRadians(timer)), 0f).RotatedBy(Projectile.rotation + 1.57), 8, 8, 76, 0f, 0f, 91, new Color(83, 66, 180), 1f);
            int dust1 = Dust.NewDust(Projectile.position + new Vector2(10f * (float)Math.Cos(MathHelper.ToRadians(timer)), 0f).RotatedBy(Projectile.rotation - 1.57), 8, 8, 76, 0f, 0f, 91, new Color(83, 66, 180), 1f);
            Main.dust[dust].velocity = Vector2.Zero;
            Main.dust[dust1].velocity = Vector2.Zero;
            /*int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 66, 0f, 0f, 91, new Color(83, 66, 180), 1.5f);
            Main.dust[dust2].velocity = Projectile.velocity;
            Main.dust[dust2].noGravity = true;*/

        }
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 360; i += 10)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-10, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(Projectile.position, 8, 8, 76, 0f, 0f, 91, new Color(83, 66, 180), 1.5f);
                Main.dust[num622].velocity += (vec * 2f);
                Main.dust[num622].velocity += Projectile.velocity;
                Main.dust[num622].noGravity = true;
            }
	     }
        
    }
}
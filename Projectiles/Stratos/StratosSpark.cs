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
			// DisplayName.SetDefault("Stratos Spark");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 8;       //Projectile width
            Projectile.height = 8;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 3;      //how many NPC will penetrate
            Projectile.timeLeft = 100;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           
			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 91, new Color(65, 250, 247), 1f);
            Main.dust[dust].position += Projectile.velocity.RotatedBy(1.57);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity = Projectile.velocity.RotatedBy(0.5);
            int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 91, new Color(65, 250, 247), 1f);
            Main.dust[dust].position += Projectile.velocity.RotatedBy(-1.57);
            Main.dust[dust1].noGravity = true;
            Main.dust[dust1].velocity = Projectile.velocity.RotatedBy(-0.5);
            int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 91, new Color(65, 250, 247), 1.5f);
            Main.dust[dust2].velocity = Projectile.velocity;
            Main.dust[dust2].noGravity = true;

        }
		public override void OnKill(int timeLeft)
        {
			for (int i = 0; i < 360; i += 10)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-10, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 91, new Color(65, 250, 247), 1.5f);
                Main.dust[num622].velocity += (vec * 2f);
                Main.dust[num622].velocity += Projectile.velocity;
                Main.dust[num622].noGravity = true;
            }
	     }
        
    }
}
using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class HrP2 : ModProjectile
    {
		private int explodeRadius = 32;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ghastly Bolt");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 8;       //Projectile width
            Projectile.height = 8;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Ranged;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 80;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.tileCollide = false;
			if (Projectile.timeLeft > 30)
				Projectile.timeLeft = 30;
			Projectile.velocity = oldVelocity;
			return false;
			
		}
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           
			int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 15, 0f, 0f);
			Main.dust[dust].scale = 1.5f;
			Main.dust[dust].velocity *= 0f;

        }
		
        
    }
}
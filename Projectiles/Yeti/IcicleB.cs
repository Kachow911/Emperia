using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Yeti
{
	
    public class IcicleB : ModProjectile
    {
		private bool init = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pine Needle");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 28;  //Projectile height
            Projectile.friendly = false;      //make that the Projectile will not damage you
			Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many Projectile will penetrate
            Projectile.timeLeft = 140;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 0;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {             
			if (Projectile.timeLeft > 120)
			{
				
				Projectile.velocity.Y = 0;
			}
        }
    }
}
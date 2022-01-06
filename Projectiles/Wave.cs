using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	
    public class Wave : ModProjectile
    {
		private bool init = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wave");
			Main.projFrames[Projectile.type] = 5;
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 5;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
			Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many Projectile will penetrate
            Projectile.timeLeft = 50;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 0;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {
			if (!init)
			{
				
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				if (Projectile.frame < 4)
					Projectile.frame = (Projectile.frame + 1);
				
			} 
			Projectile.velocity.Y = 0;
		}
    }
}
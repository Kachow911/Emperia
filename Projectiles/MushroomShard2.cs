using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class MushroomShard2 : ModProjectile
    {
		private int explodeRadius = 30;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushard");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 16;       //projectile width
            projectile.height = 16;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you       // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 45;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                       
			if (projectile.timeLeft > 20)
			{
				projectile.velocity.Y *= 1.02f;
				projectile.velocity.X *= 1.02f;
				
			}
			else
			{
				projectile.velocity.Y *= .9f;
				projectile.velocity.X *= .9f;
			}	// |
            //projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			projectile.rotation++;
		}
		
        
    }
}
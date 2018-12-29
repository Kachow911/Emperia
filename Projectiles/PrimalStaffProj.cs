using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class PrimalStaffProj : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Purple Sphere");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 20;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 200;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                                                           // |
    
			projectile.velocity.X *= 0.99f;
			projectile.velocity.Y *= 0.99f;
		}
		public override void Kill(int timeLeft)
        {
			//
		}
        
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Desert
{
	
    public class DesertSpike : ModProjectile
    {
		private bool init = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spike");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 16;       //projectile width
            projectile.height = 16;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
			projectile.hostile = false;
            projectile.melee = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many projectile will penetrate
            projectile.timeLeft = 160;   //how many time projectile projectile has before disepire
            projectile.light = 0f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 0;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {
            projectile.scale = 1.1f;
            projectile.velocity *= .90f;
        }
    }
}
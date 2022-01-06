using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{

    public class MushroomShard2 : ModProjectile
    {
		private int explodeRadius = 30;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushard");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 16;       //Projectile width
            Projectile.height = 16;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you       // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 45;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.DamageType = DamageClass.Ranged;
//was thrown pre 1.4
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                       
			if (Projectile.timeLeft > 20)
			{
				Projectile.velocity.Y *= 1.02f;
				Projectile.velocity.X *= 1.02f;
				
			}
			else
			{
				Projectile.velocity.Y *= .9f;
				Projectile.velocity.X *= .9f;
			}	// |
            //Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			Projectile.rotation++;
		}
		
        
    }
}
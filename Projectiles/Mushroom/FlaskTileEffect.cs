using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{

    public class FlaskTileEffect : ModProjectile
    {
		private int explodeRadius = 100;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Enchanted Mushroom");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 8;      
            Projectile.height = 8;  
            Projectile.friendly = true;      
            Projectile.DamageType = DamageClass.Magic;         
            Projectile.tileCollide = false;   
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 900;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.5f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           // |
            if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 20, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
			if (Projectile.timeLeft % 100 == 0)
			{
				if (Main.rand.NextBool(4))
					Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X + Main.rand.Next(-20, 20), Projectile.Center.Y + 10, 0, -1, ModContent.ProjectileType<BigShroom2>(), 48, 2f, Projectile.owner, 0f, 0f);
				else
					Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X + Main.rand.Next(-20, 20), Projectile.Center.Y + 10, 0, -1, ModContent.ProjectileType<EnchantedMushroom>(), 24, 1.5f, Projectile.owner, 0f, 0f);
			}
		}
		public override void Kill(int timeLeft)
        {
			//
		}
        
		
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class FlaskTileEffect : ModProjectile
    {
		private int explodeRadius = 100;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Mushroom");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 8;      
            projectile.height = 8;  
            projectile.friendly = true;      
            projectile.magic = true;         
            projectile.tileCollide = false;   
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 900;   //how many time projectile projectile has before disepire
            projectile.light = 0.5f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                                                           // |
            if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 20, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
			if (projectile.timeLeft % 100 == 0)
			{
				if (Main.rand.NextBool(4))
					Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-20, 20), projectile.Center.Y + 10, 0, -1, mod.ProjectileType("BigShroom2"), 48, 2f, projectile.owner, 0f, 0f);
				else
					Projectile.NewProjectile(projectile.Center.X + Main.rand.Next(-20, 20), projectile.Center.Y + 10, 0, -1, mod.ProjectileType("EnchantedMushroom"), 24, 1.5f, projectile.owner, 0f, 0f);
			}
		}
		public override void Kill(int timeLeft)
        {
			//
		}
        
		
    }
}
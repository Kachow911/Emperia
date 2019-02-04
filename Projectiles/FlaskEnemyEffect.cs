using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class FlaskEnemyEffect : ModProjectile
    {
		private int explodeRadius = 70;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushroom Gas");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 8;      
            projectile.height = 8;  
            projectile.friendly = true;      
            projectile.magic = true;         
            projectile.tileCollide = false;   
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 300;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {   		// |
			for (int i = 0; i < 5; i++)
			{
				if (Main.rand.Next(3) == 0)
				{
					Dust.NewDust(projectile.position + new Vector2(Main.rand.Next(-explodeRadius / 2, explodeRadius / 2), Main.rand.Next(-explodeRadius / 2, explodeRadius / 2)), projectile.width, projectile.height, 20, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
				}
			}
			for (int i = 0; i < Main.npc.Length; i++)
            {
				if (projectile.Distance(Main.npc[i].Center) < explodeRadius && projectile.timeLeft % 30 == 0 && !Main.npc[i].townNPC)
                    Main.npc[i].StrikeNPC(32, 0f, 0, false, false, false);
			}
			
		}

		public override void Kill(int timeLeft)
        {
			//
		}
        
		
    }
}
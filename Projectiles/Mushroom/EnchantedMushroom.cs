using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{
	
    public class EnchantedMushroom : ModProjectile
    {
		private bool init = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Mushroom");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 28;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 180;   //how many time projectile projectile has before disepire
            projectile.light = 0.2f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 0;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {             
			if (!init)
			{
				if (Main.rand.NextBool(2))
					projectile.spriteDirection = -1;
				init = true;
			}
            if (Main.rand.Next(20) == 0)
            {
            	int dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width / 8, projectile.height / 8, 20, 0f, 0f, 0, new Color(39, 90, 219), 1.5f);
            }
			projectile.alpha++;
        }
        
    }
}
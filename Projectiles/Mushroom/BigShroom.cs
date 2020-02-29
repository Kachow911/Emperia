using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{

    public class BigShroom : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Mushroom");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 28;  //projectile height
            projectile.friendly = false;      //make that the projectile will not damage you
			projectile.hostile = true;
            projectile.magic = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 240;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
			
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 75;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            int dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width / 8, projectile.height / 8, 20, 0f, 0f, 0, new Color(39, 90, 219), 1.5f);// |
            if (Main.rand.Next(5) == 0)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 41, projectile.velocity.X * 0.15f, projectile.velocity.Y * 0.15f);
               
            }
			
        }
        
    }
}
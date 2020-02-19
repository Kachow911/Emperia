using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{

    public class ExplodeMushroom : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Explosive Mushroom");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 32;       //projectile width
            projectile.height = 32;  //projectile height
            projectile.friendly = false;      //make that the projectile will not damage you   // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 100;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 0;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {     
			/*if (projectile.timeLeft % 10 == 0)
			{
				for (int i = 0; i < 360; i+= 10)
				{
					int dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width / 8, projectile.height / 8, 20, 0f, 0f, 0, new Color(39, 90, 219), 1.5f);
					Main.dust[dust].velocity = new Vector2(0, -3).RotatedBy(MathHelper.ToRadians(10 * i));
				}	
			}*/
			
        }
        public override void Kill(int timeLeft)
        {
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("ExplodeMushroomEffect"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
		}
    }
}
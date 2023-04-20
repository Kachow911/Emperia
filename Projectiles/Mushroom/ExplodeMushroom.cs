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
			// DisplayName.SetDefault("Explosive Mushroom");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 32;       //Projectile width
            Projectile.height = 32;  //Projectile height
            Projectile.friendly = false;      //make that the Projectile will not damage you   // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 100;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 0;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {     
			/*if (Projectile.timeLeft % 10 == 0)
			{
				for (int i = 0; i < 360; i+= 10)
				{
					int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width / 8, Projectile.height / 8, 20, 0f, 0f, 0, new Color(39, 90, 219), 1.5f);
					Main.dust[dust].velocity = new Vector2(0, -3).RotatedBy(MathHelper.ToRadians(10 * i));
				}	
			}*/
			
        }
        public override void Kill(int timeLeft)
        {
			Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<ExplodeMushroomEffect>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
		}
    }
}
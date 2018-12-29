using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class ExplodeMushroomEffect : ModProjectile
    {
		private int explodeRadius = 100;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushroom Gas");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 8;      
            projectile.height = 8;  
            projectile.friendly = false;
			projectile.hostile = true;			
            projectile.magic = true;         
            projectile.tileCollide = false;   
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 180;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {   		// |
			if (projectile.timeLeft % 15 == 0)
			{
				for (int i = 0; i < 360; i+= 5)
				{
					if (Main.rand.Next(5) == 0)
					{
						int dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width / 8, projectile.height / 8, 20, 0f, 0f, 0, new Color(39, 90, 219), 1.5f);
						Main.dust[dust].velocity = new Vector2(0, -3).RotatedBy(MathHelper.ToRadians(5 * i));
					}
				}	
			}
			for (int i = 0; i < Main.player.Length; i++)
            {
				if (projectile.Distance(Main.player[i].Center) < explodeRadius && projectile.timeLeft % 10 == 0)
                     Main.player[i].Hurt(Terraria.DataStructures.PlayerDeathReason.ByProjectile(Main.player[i].whoAmI, projectile.whoAmI), projectile.damage, 0);
			}
			
		}
		public override void Kill(int timeLeft)
        {
			
		}
        
		
    }
}
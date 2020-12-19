using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class EnchantedBlade : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Blade");
		}
        public override void SetDefaults()
        { 
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.melee = true;
			projectile.aiStyle = 0;
            projectile.tileCollide = false;
            projectile.penetrate = 2;
            projectile.timeLeft = 18;
            projectile.alpha = 240;
            drawOffsetX = -3;
        }
        bool init = false;
        int direction = 1;
        public override void AI()
        {
            if (!init)
            {
                init = true;
                if (projectile.velocity.X < 0) {
                    direction = -1;
                }
            }
            if (projectile.alpha > 0) {projectile.alpha -= 40;}

            projectile.velocity.X = (9 - (18 - projectile.timeLeft)) / 1.05f * direction; //swings the sword in an arc
            if (projectile.timeLeft >= 9f)
            {
                projectile.velocity.Y = (18 - projectile.timeLeft) / 1.05f;
            }
            else
            {
               projectile.velocity.Y = projectile.timeLeft / 1.05f;
            }

            projectile.rotation = (MathHelper.ToRadians(180 - projectile.timeLeft * 10 * direction)); //rotates the sword in an arc

            if (projectile.timeLeft % 6 == 0) {
                Vector2 dustPosition = new Vector2(projectile.position.X, projectile.position.Y);                
    	    	int blue = Dust.NewDust(dustPosition, projectile.width, projectile.height, 15, projectile.velocity.X, projectile.velocity.Y);
                int yellow = Dust.NewDust(dustPosition, projectile.width, projectile.height, 57, projectile.velocity.X, projectile.velocity.Y);
                int pink = Dust.NewDust(dustPosition, projectile.width, projectile.height, 58, projectile.velocity.X, projectile.velocity.Y);
	            Main.dust[blue].velocity *= -0.1f;
                Main.dust[yellow].velocity *= -0.1f;
                Main.dust[pink].velocity *= -0.1f;
            } 
            //projectile.rotation = (MathHelper.ToRadians(-90 + (180 - projectile.timeLeft)));
        }
        public override bool? CanHitNPC(NPC target)
		{
            if (projectile.penetrate == 2)
            {
                return true;
            }
            else return false;
		}

		public override void Kill(int timeLeft)
        {
            for (int i = 1; i < 5; ++i){
                Vector2 dustPosition = new Vector2(projectile.position.X, projectile.position.Y);                
	    	    int blue = Dust.NewDust(dustPosition, projectile.width, projectile.height, 15, 0f, 0f, 0, default(Color), 1.25f);
    		    int yellow = Dust.NewDust(dustPosition, projectile.width, projectile.height, 57, 0f, 0f, 0, default(Color), 1.25f);
                int pink = Dust.NewDust(dustPosition, projectile.width, projectile.height, 58, 0f, 0f, 0, default(Color), 1.25f);
                Main.dust[blue].velocity *= 0.3f;
                Main.dust[yellow].velocity *= 0.3f;
                Main.dust[pink].velocity *= 0.3f;
		    }
        }
    }
}
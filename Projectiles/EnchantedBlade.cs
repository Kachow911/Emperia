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
			// DisplayName.SetDefault("Enchanted Blade");
		}
        public override void SetDefaults()
        { 
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
			Projectile.aiStyle = 0;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 18;
            Projectile.alpha = 240;
            DrawOffsetX = -3;
        }
        bool init = false;
        int direction = 1;
        public override void AI()
        {
            if (!init)
            {
                init = true;
                if (Projectile.velocity.X < 0) {
                    direction = -1;
                }
            }
            if (Projectile.alpha > 0) {Projectile.alpha -= 40;}

            Projectile.velocity.X = (9 - (18 - Projectile.timeLeft)) / 1.05f * direction; //swings the sword in an arc
            if (Projectile.timeLeft >= 9f)
            {
                Projectile.velocity.Y = (18 - Projectile.timeLeft) / 1.05f;
            }
            else
            {
               Projectile.velocity.Y = Projectile.timeLeft / 1.05f;
            }

            Projectile.rotation = (MathHelper.ToRadians(180 - Projectile.timeLeft * 10 * direction)); //rotates the sword in an arc

            if (Projectile.timeLeft % 6 == 0) {
                Vector2 dustPosition = new Vector2(Projectile.position.X, Projectile.position.Y);                
    	    	int blue = Dust.NewDust(dustPosition, Projectile.width, Projectile.height, DustID.MagicMirror, Projectile.velocity.X, Projectile.velocity.Y);
                int yellow = Dust.NewDust(dustPosition, Projectile.width, Projectile.height, DustID.Enchanted_Gold, Projectile.velocity.X, Projectile.velocity.Y);
                int pink = Dust.NewDust(dustPosition, Projectile.width, Projectile.height, DustID.Enchanted_Pink, Projectile.velocity.X, Projectile.velocity.Y);
	            Main.dust[blue].velocity *= -0.1f;
                Main.dust[yellow].velocity *= -0.1f;
                Main.dust[pink].velocity *= -0.1f;
            } 
            //Projectile.rotation = (MathHelper.ToRadians(-90 + (180 - Projectile.timeLeft)));
        }
        public override bool? CanHitNPC(NPC target)
		{
            if (Projectile.penetrate == 2)
            {
                return null;
            }
            else return false;
		}

		public override void Kill(int timeLeft)
        {
            for (int i = 1; i < 5; ++i){
                Vector2 dustPosition = new Vector2(Projectile.position.X, Projectile.position.Y);                
	    	    int blue = Dust.NewDust(dustPosition, Projectile.width, Projectile.height, DustID.MagicMirror, 0f, 0f, 0, default(Color), 1.25f);
    		    int yellow = Dust.NewDust(dustPosition, Projectile.width, Projectile.height, DustID.Enchanted_Gold, 0f, 0f, 0, default(Color), 1.25f);
                int pink = Dust.NewDust(dustPosition, Projectile.width, Projectile.height, DustID.Enchanted_Pink, 0f, 0f, 0, default(Color), 1.25f);
                Main.dust[blue].velocity *= 0.3f;
                Main.dust[yellow].velocity *= 0.3f;
                Main.dust[pink].velocity *= 0.3f;
		    }
        }
    }
}
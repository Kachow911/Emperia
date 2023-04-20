using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class TrueJoyuse2 : ModProjectile
    {
		private int explodeRadius = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Holy Yoyo");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 8;       //Projectile width
            Projectile.height = 8;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 100;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 0;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           // |
           explodeRadius++;
			if (explodeRadius % 2 == 0)
			{
				Projectile.alpha += 5;
			}
			
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch, 0f, 0f, 91, new Color(255, 255, 255), .8f);
            Main.dust[dust].velocity *= 0.1f;
            if (Projectile.velocity == Vector2.Zero)
            {
                Main.dust[dust].velocity.Y -= 1f;
                Main.dust[dust].scale = .6f;
            }
            else
            {
                Main.dust[dust].velocity += Projectile.velocity * 0.2f;
            }
            Main.dust[dust].position.X = Projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].position.Y = Projectile.Center.Y + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].noGravity = true;
			Projectile.velocity = Projectile.velocity.RotatedByRandom(1);
			Projectile.velocity *= .98f;
		}
		public override void Kill(int timeLeft)
        {
			//
		}
        
    }
}
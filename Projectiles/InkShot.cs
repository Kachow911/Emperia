using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class InkShot : ModProjectile
    {
		private int explodeRadius = 100;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ink Blast");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 8;       //Projectile width
            Projectile.height = 8;  //Projectile height
            Projectile.friendly = false;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 200;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.hostile = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           // |
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 54, 0f, 0f, 91, new Color(89, 249, 116), 1.5f);
            Main.dust[dust].velocity *= 0.1f;
            if (Projectile.velocity == Vector2.Zero)
            {
                Main.dust[dust].velocity.Y -= 1f;
                Main.dust[dust].scale = 1.2f;
            }
            else
            {
                Main.dust[dust].velocity += Projectile.velocity * 0.2f;
            }
            Main.dust[dust].position.X = Projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].position.Y = Projectile.Center.Y + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].noGravity = true;
		}
		public override void Kill(int timeLeft)
        {
			//
		}
        
    }
}
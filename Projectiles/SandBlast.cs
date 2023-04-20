using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class SandBlast : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sandstorm Blast");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 20;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 2;      //how many NPC will penetrate
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {
            Projectile.velocity *= .98f;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			for (int i = 0; i < 2; i++)
			{
				int dust = Dust.NewDust(new Vector2(Projectile.position.X, (float) ((double) Projectile.position.Y + (double) Projectile.height - 16.0)), Projectile.width, 16, 85, 0.0f, 0.0f, 0, new Color(), 1.5f);
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
        }
        
    }
}
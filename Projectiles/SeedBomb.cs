using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class SeedBomb : ModProjectile
    {
		private int explodeRadius = 30;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("SeedBomb");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 16;       //Projectile width
            Projectile.height = 16;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 200;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           // |
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 89, 0f, 0f, 91, new Color(89, 249, 116), 1.5f);
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
			for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Projectile.Distance(Main.npc[i].Center) < explodeRadius)
                     Main.npc[i].SimpleStrikeNPC(Projectile.damage, 0);
            }
			for (int i = 0; i < 360; i += 10)
				{
					Vector2 vec = Vector2.Transform(new Vector2(-10, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
					vec.Normalize();
					int num622 = Dust.NewDust(new Vector2(Projectile.Center.X, (float) ((double) Projectile.Center.Y + (double) Projectile.height - 16.0)), Projectile.width, 16, 89, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[num622].velocity += (vec *0.2f);
					Main.dust[num622].noGravity = true;
				}
		}
        
    }
}
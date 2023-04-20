using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class MagmaBomb : ModProjectile
    {
		private int explodeRadius = 100;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Magma Bomb");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 8;       //Projectile width
            Projectile.height = 8;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 240;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {       
			if (Projectile.timeLeft > 10)
			{
                for (int i = 0; i < 360; i += 6)
                {
                    Vector2 vec = Vector2.Transform(new Vector2(-5, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
                    vec.Normalize();
                    int num622 = Dust.NewDust(new Vector2(Projectile.Center.X, (float)((double)Projectile.Center.Y)), Projectile.width, 16, DustID.Torch, 0.0f, 0.0f, 0, new Color(), 1f);
                    Main.dust[num622].position += (vec);
                    Main.dust[num622].noGravity = true;
                }
            }
		}
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Projectile.Distance(Main.npc[i].Center) < explodeRadius && !Main.npc[i].townNPC)
					
                     Main.npc[i].SimpleStrikeNPC(Projectile.damage, 0);
            }
			for (int i = 0; i < 360; i += 10)
				{
					Vector2 vec = Vector2.Transform(new Vector2(-10, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
					vec.Normalize();
					int num622 = Dust.NewDust(new Vector2(Projectile.Center.X, (float) ((double) Projectile.Center.Y)), Projectile.width, 16, DustID.Torch, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[num622].velocity += (vec *2f);
					Main.dust[num622].noGravity = true;
				}
		}
        
    }
}
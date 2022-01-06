using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class ShadowBoltHostile : ModProjectile
    {
		private int explodeRadius = 32;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Bolt");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 8;       //Projectile width
            Projectile.height = 8;  //Projectile height
            Projectile.friendly = false;      //make that the Projectile will not damage you
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 100;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           
			int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f);
			Main.dust[dust].scale = 1.5f;
			Main.dust[dust].velocity *= 0f;

        }
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < Main.player.Length; i++)
            {
				if (Projectile.Distance(Main.player[i].Center) < explodeRadius)
                    Main.player[i].AddBuff(BuffID.ShadowFlame, 120);
			}
			for (int i = 0; i < 360; i += 10)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-explodeRadius, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 91, new Color(255, 255, 255), 1.5f);
                Main.dust[num622].velocity += (vec * 2f);
                Main.dust[num622].noGravity = true;
            }
	     }
        
    }
}
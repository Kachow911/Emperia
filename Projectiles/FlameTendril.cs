using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class FlameTendril : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame Tendril");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 40;       //Projectile width
            Projectile.height = 40;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 3;      //how many NPC will penetrate
            Projectile.timeLeft = 100;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {
			Vector2 center10 = Projectile.Center;
			Projectile.scale = 1f - Projectile.localAI[0];
			Projectile.width = (int)(20f * Projectile.scale);
			Projectile.height = Projectile.width;
			Projectile.position.X = center10.X - (float)(Projectile.width / 2);
			Projectile.position.Y = center10.Y - (float)(Projectile.height / 2);
			if ((double)Projectile.localAI[0] < 0.1)
			{
				Projectile.localAI[0] += 0.01f;
			}
			else
			{
				Projectile.localAI[0] += 0.025f;
			}
			if (Projectile.localAI[0] >= 0.95f)
			{
				Projectile.Kill();
			}
			Projectile.velocity.X = Projectile.velocity.X + Projectile.ai[0] * 1.5f;
			Projectile.velocity.Y = Projectile.velocity.Y + Projectile.ai[1] * 1.5f;
			if (Projectile.velocity.Length() > 16f)
			{
				Projectile.velocity.Normalize();
				Projectile.velocity *= 16f;
			}
			Projectile.ai[0] *= 1.05f;
			Projectile.ai[1] *= 1.05f;
			if (Projectile.scale < 1f)
			{
				int num890 = 0;
				while ((float)num890 < Projectile.scale * 10f)
				{
					int num891 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1.1f);
					Main.dust[num891].position = (Main.dust[num891].position + Projectile.Center) / 2f;
					Main.dust[num891].noGravity = true;
					Dust dust3 = Main.dust[num891];
					dust3.velocity *= 0.1f;
					dust3 = Main.dust[num891];
					dust3.velocity -= Projectile.velocity * (1.3f - Projectile.scale);
					Main.dust[num891].fadeIn = (float)(100 + Projectile.owner);
					dust3 = Main.dust[num891];
					dust3.scale += Projectile.scale * 0.75f;
					int num3 = num890;
					num890 = num3 + 1;
				}
				return;
			}

		}
	
        
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Granite
{
    public class GraniteRock1 : ModProjectile
    {

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Rock");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 14;       //Projectile width
            Projectile.height = 14;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this Projectile has before disepire
            Projectile.light = 0.1f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the Projectile will face the corect way
        {                                                           // |
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			if (Main.rand.Next(5) == 0)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, 240, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num622].velocity += Projectile.velocity * 0.2f;
				Main.dust[num622].noGravity = true;
			}
        }
		
		public override void Kill(int timeLeft)
        {
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
        	for (int i = 0; i < 108; i += 36)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 240, 0f, 0f, 0, new Color(53f, 67f, 253f), 1f);
				Main.dust[num622].velocity += (vec *2f);
				Main.dust[num622].noGravity = true;
			}
		}
		
    }
}
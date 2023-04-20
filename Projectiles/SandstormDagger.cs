using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	public class SandstormDagger : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 15;
			Projectile.height = 23;
			//Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Ranged;
//was thrown pre 1.4
			Projectile.timeLeft = 360;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("SandstormDagger");
		}
		
		public override void Kill(int timeLeft)
		{
			//if (Main.rand.Next(4) == 0 && Projectile.noDropItem == false)
        	//{
        	//	Item.NewItem((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<SandstormDagger>());
        	//}
			 for (int i = 0; i < 360; i += 10)
				{
					Vector2 vec = Vector2.Transform(new Vector2(-10, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
					vec.Normalize();
					int num622 = Dust.NewDust(new Vector2(Projectile.position.X, (float) ((double) Projectile.position.Y + (double) Projectile.height - 16.0)), Projectile.width, 16, 85, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[num622].velocity += (vec *0.2f);
				}
		}

		public override void AI()
		{
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			Projectile.velocity.Y += 0.2f;
		}
		
		
	}
}
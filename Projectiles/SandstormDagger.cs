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
			projectile.width = 15;
			projectile.height = 23;
			//projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.thrown = true;
			projectile.timeLeft = 360;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SandstormDagger");
		}
		
		public override void Kill(int timeLeft)
		{
			if (Main.rand.Next(4) == 0 && projectile.noDropItem == false)
        	{
        		Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("SandstormDagger"));
        	}
			 for (int i = 0; i < 360; i += 10)
				{
					Vector2 vec = Vector2.Transform(new Vector2(-10, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
					vec.Normalize();
					int num622 = Dust.NewDust(new Vector2(projectile.position.X, (float) ((double) projectile.position.Y + (double) projectile.height - 16.0)), projectile.width, 16, 85, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[num622].velocity += (vec *0.2f);
				}
		}

		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			projectile.velocity.Y += 0.2f;
		}
		
		
	}
}
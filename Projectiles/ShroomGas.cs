using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Emperia.Projectiles {
public class ShroomGas : ModProjectile
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shroom Gas");
		}
	public override void SetDefaults()
	{
		projectile.CloneDefaults(ProjectileID.ToxicCloud);
		
	}
	 public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			if (Main.rand.Next(3) == 0)
            {
            	Dust.NewDust(projectile.position + new Vector2(Main.rand.Next(-50, 50), Main.rand.Next(-50, 50)), projectile.width, projectile.height, 20, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
		}
	
}
}	
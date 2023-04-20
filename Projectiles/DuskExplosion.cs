using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	public class DuskExplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Indigo Explosion");
		}
		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 15;
			Projectile.light = 0.5f;
			Projectile.tileCollide = false;
			Main.projFrames[Projectile.type] = 7;
			Projectile.scale = 1.25f;
		}
		
		public override void AI()
		{
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 4;
			} 
		}
	}
}
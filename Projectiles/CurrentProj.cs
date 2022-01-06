using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Projectiles
{
	public class CurrentProj : ModProjectile
	{
		int timer = 0;
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 7f;
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 350f;
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 10f;
		}

		public override void SetDefaults()
		{
			Projectile.extraUpdates = 0;
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = 99;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.scale = 1f;
		}
		
		public override void AI()
		{
			timer ++;
		
			if (timer % 60 == 0)
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position.X, Projectile.position.Y, 0, 8f, ModContent.ProjectileType<Projectiles.Rain>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0, 0);
		}
	
	}
}

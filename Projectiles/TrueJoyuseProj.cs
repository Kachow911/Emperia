using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	public class TrueJoyuseProj : ModProjectile
	{
		int timer = 0;
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 8f;
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 350f;
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 10f;
		}

		public override void SetDefaults()
		{
			projectile.extraUpdates = 0;
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 99;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.scale = 1f;
		}
		
		public override void AI()
		{
			timer ++;
			Vector2 perturbedSpeed = new Vector2(0, 3);
			Vector2 perturbedSpeed2 = new Vector2(0, -3);
			if (timer % 20 == 0)
			{
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("TrueJoyuse1"), projectile.damage, projectile.knockBack, projectile.owner, 0, 0);
				Projectile.NewProjectile(projectile.position.X, projectile.position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, mod.ProjectileType("TrueJoyuse2"), projectile.damage, projectile.knockBack, projectile.owner, 0, 0);
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			
		}
	}
}

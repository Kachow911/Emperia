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
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 8f;
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
			Vector2 perturbedSpeed = new Vector2(0, 3);
			Vector2 perturbedSpeed2 = new Vector2(0, -3);
			if (timer % 20 == 0)
			{
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position.X, Projectile.position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<TrueJoyuse1>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0);
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position.X, Projectile.position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, ModContent.ProjectileType<TrueJoyuse2>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0, 0);
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			
		}
	}
}

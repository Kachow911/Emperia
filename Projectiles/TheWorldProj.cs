using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Projectiles
{
	public class TheWorldProj : ModProjectile
	{
		int timer = 0;
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 100f;
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 400f;
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 13f;
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
			
			if (timer % 20 == 0)
			{
				for (int i = 0; i < 360; i += 30)
				{
					Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(i));
					Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position.X, Projectile.position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<TheWorldSecond>(), Projectile.damage / 5, Projectile.knockBack, Projectile.owner, 0, 0);
				}
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<BurningNight>(), 320);
		}
	}
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Projectiles
{
	public class JoyuseProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 20f;
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 320f;
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 8f;
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
			
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			//target.AddBuff(ModContent.BuffType<BurningNight>(), 240);
		}
	}
}

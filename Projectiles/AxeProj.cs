using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Projectiles
{
	public class AxeProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloody Axe");

		}
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.PainterPaintball);
			Projectile.DamageType = DamageClass.Ranged;
			//was thrown pre 1.4
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.timeLeft = 200;
			Projectile.height = 24;
			Projectile.width = 24;
			Projectile.penetrate = 1;
			Projectile.extraUpdates = 1;
			Projectile.alpha = 0;
		}

		public override void AI()
		{
            if (Projectile.velocity.X > 0)
            {
                Projectile.spriteDirection = -1;
            }
            else if (Projectile.velocity.X < 0)
            {
                Projectile.spriteDirection = 1;
            }
			Projectile.rotation += 0.5f;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 8; ++i)
            {
                Color rgb = new Color(166, 16, 30);
                int index3 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, 76, 0.0f, 0.0f, 0, rgb, 0.8f);
            }
            
            target.AddBuff(ModContent.BuffType<MoreDamage>(), 240);
        }

        public override void Kill(int timeLeft)
		{
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, (int)Projectile.position.X, (int)Projectile.position.Y, 27);
            if (Main.rand.Next(0, 4) == 0)
				Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<Items.Weapons.BarbarianWaraxe>(), 1, false, 0, false, false);
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 7);
				Vector2 vel = new Vector2(0, -1).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
			}

		}

	}
}

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
			projectile.CloneDefaults(ProjectileID.PainterPaintball);
			projectile.ranged = false;
			projectile.thrown = true;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.timeLeft = 200;
			projectile.height = 24;
			projectile.width = 24;
			projectile.penetrate = 1;
			projectile.extraUpdates = 1;
			projectile.alpha = 0;
		}

		public override void AI()
		{
            if (projectile.velocity.X > 0)
            {
                projectile.spriteDirection = -1;
            }
            else if (projectile.velocity.X < 0)
            {
                projectile.spriteDirection = 1;
            }
			projectile.rotation += 0.2f;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 8; ++i)
            {
                Color rgb = new Color(166, 16, 30);
                int index3 = Dust.NewDust(new Vector2((float)(projectile.position.X + 4.0), (float)(projectile.position.Y + 4.0)), projectile.width - 8, projectile.height - 8, 76, 0.0f, 0.0f, 0, rgb, 0.8f);
            }
            
            target.AddBuff(mod.BuffType("MoreDamage"), 240);
        }

        public override void Kill(int timeLeft)
		{
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 27);
            if (Main.rand.Next(0, 4) == 0)
				Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("BarbarianWaraxe"), 1, false, 0, false, false);
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 7);
				Vector2 vel = new Vector2(0, -1).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
			}

		}

	}
}

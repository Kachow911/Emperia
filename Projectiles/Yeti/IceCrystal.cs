using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Yeti
{

    public class IceCrystal : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Crystal");
		}
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.light = 0.75f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()
        {                                                           // |
			if (Main.rand.NextBool(2))
			{
				int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 68, (float) Projectile.velocity.X, (float) Projectile.velocity.Y, 0, default(Color), 0.9f);
				Main.dust[index2].noGravity = true;
			}
			Projectile.velocity.X *= 0.99f;
			Projectile.velocity.Y *= 0.99f;
			Projectile.rotation += .05f;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Color rgb = new Color(135,206,250);
			int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 68, (float) Projectile.velocity.X, (float) Projectile.velocity.Y, 0, rgb, 0.9f);
			Main.dust[index2].noGravity = true;
		}
		public override void Kill(int timeLeft)
        {
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);  
			for (int i = 0; i < 6; i++)
			{
				Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 60 * i));
				int p = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<IceShard>(), Projectile.damage / 3, 0, Main.myPlayer, 0, 0);
				Main.projectile[p].rotation = MathHelper.ToRadians(Main.rand.Next(360));
			}
		}
        
    }
}
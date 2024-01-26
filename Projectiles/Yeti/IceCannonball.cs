using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Yeti
{
	
    public class IceCannonball : ModProjectile
    {
		private bool init = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Icicle");
		}
        public override void SetDefaults()
        {
            Projectile.width = 6;       //Projectile width
            Projectile.height = 6;  //Projectile height
            Projectile.hostile = false;      //make that the Projectile will not damage you
			Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many Projectile will penetrate
            Projectile.timeLeft = 300;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0f;    // Projectile light
            Projectile.ignoreWater = false;
			Projectile.alpha = 0;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Main.rand.NextBool(4))
			{
				int index2 = Dust.NewDust(Projectile.oldPosition, Projectile.width, Projectile.height, DustID.BlueCrystalShard, (float) Projectile.velocity.X, (float) Projectile.velocity.Y, 0, default(Color), 0.9f);
				Main.dust[index2].noGravity = true;
			}
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(3) > 0)
            {
			    target.AddBuff(BuffID.Frostburn, 120);
            }
            Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X * 0.6f, Projectile.velocity.Y * 0.6f).RotatedByRandom(MathHelper.ToRadians(10));
            Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<IceShardTiny>(), Projectile.damage / 3 , 0, Main.myPlayer, 0, 0);

		}
        public override void OnKill(int timeLeft)
		{
			for (int i = 0; i < 3; i++)
			{
				int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueCrystalShard, (float) Projectile.velocity.X / 10, (float) Projectile.velocity.Y / 10, 0, default(Color), 0.9f);

            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
		}
    }
}
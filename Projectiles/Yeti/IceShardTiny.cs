using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Yeti
{
	
    public class IceShardTiny : ModProjectile
    {
		private bool init = false;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ice Shard");
		}
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.hostile = false;
			Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 10;
            Projectile.light = 0f;
            Projectile.ignoreWater = false;
			Projectile.alpha = 0;
            Projectile.scale = 0.9f;
        }
        public override void AI()
        {
            Projectile.scale -= 0.03f;
            Projectile.rotation = Main.rand.Next(7);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(2) == 0)
            {
			    target.AddBuff(BuffID.Frostburn, 90);
            }
		}
        public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 1; i++)
			{
				int index2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueCrystalShard, (float) Projectile.velocity.X / 10, (float) Projectile.velocity.Y / 10, 0, default(Color), 0.9f);
                Main.dust[index2].noGravity = true;
            }
		}
        public override bool? CanHitNPC(NPC target)
		{
            if (Projectile.timeLeft > 9)
            {
                return false;
            }
            else return null;
		}
    }
}
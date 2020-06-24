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
			DisplayName.SetDefault("Ice Shard");
		}
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.hostile = false;
			projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 10;
            projectile.light = 0f;
            projectile.ignoreWater = false;
			projectile.alpha = 0;
            projectile.scale = 0.9f;
        }
        public override void AI()
        {
            projectile.scale -= 0.03f;
            projectile.rotation = Main.rand.Next(7);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
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
				int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, (float) projectile.velocity.X / 10, (float) projectile.velocity.Y / 10, 0, default(Color), 0.9f);
                Main.dust[index2].noGravity = true;
            }
		}
        public override bool? CanHitNPC(NPC target)
		{
            if (projectile.timeLeft > 9)
            {
                return false;
            }
            else return true;
		}
    }
}
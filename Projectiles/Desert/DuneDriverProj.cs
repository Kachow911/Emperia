using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Desert
{
    public class DuneDriverProj : ModProjectile
    {
		bool hitGround = false;
        public override void SetDefaults()
        {
			projectile.damage = 14;
            projectile.width = 40;
            projectile.height = 2;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 120;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
            projectile.light = 0.25f;
        }
        public override void AI()
        {
			projectile.velocity.Y = 10;
            if (projectile.timeLeft % 20 == 0 && projectile.timeLeft <= 60 && hitGround)
			{
                Main.PlaySound(SoundID.Roar, projectile.Center);
                Player player = Main.player[projectile.owner];
                Vector2 direction = Main.MouseWorld - projectile.Center;
                direction.Normalize();
                Projectile.NewProjectile(projectile.Center.X, projectile.position.Y, direction.X * 7f, direction.Y * 7f, mod.ProjectileType("DuneWorm"), projectile.damage, 0, Main.myPlayer, 0, 0);
                //Projectile.NewProjectile(projectile.position.X + Main.rand.Next(40), projectile.position.Y, direction.X * 7f, direction.Y * 7f, mod.ProjectileType("DuneWorm"), projectile.damage, 0, Main.myPlayer, 0, 0);
            }
            if (Main.rand.Next(3) == 0)
			{
				int wormDust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 246, -1, -1);
                Main.dust[wormDust].noGravity = true;
			}
		}
		public override bool? CanHitNPC(NPC target)
		{
            return false;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			hitGround = true;
			projectile.velocity = Vector2.Zero;
			return false;
            projectile.timeLeft = 10;
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = false;
            return true;
        }
    }
}
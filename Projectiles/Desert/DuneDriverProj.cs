using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Desert;

namespace Emperia.Projectiles.Desert
{
    public class DuneDriverProj : ModProjectile
    {
		bool hitGround = false;
        public override void SetDefaults()
        {
			Projectile.damage = 14;
            Projectile.width = 40;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.light = 0.25f;
        }
        public override void AI()
        {
			Projectile.velocity.Y = 10;
            if (Projectile.timeLeft % 20 == 0 && Projectile.timeLeft <= 60 && hitGround)
			{
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Roar, Projectile.Center);
                Player player = Main.player[Projectile.owner];
                Vector2 direction = Main.MouseWorld - Projectile.Center;
                direction.Normalize();
                Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.position.Y, direction.X * 7f, direction.Y * 7f, ModContent.ProjectileType<DuneWorm>(), Projectile.damage, 0, Main.myPlayer, 0, 0);
                //Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.InheritSource(Projectile), Projectile.position.X + Main.rand.Next(40), Projectile.position.Y, direction.X * 7f, direction.Y * 7f, ModContent.ProjectileType<DuneWorm>(), Projectile.damage, 0, Main.myPlayer, 0, 0);
            }
            if (Main.rand.Next(3) == 0)
			{
				int wormDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 246, -1, -1);
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
			Projectile.velocity = Vector2.Zero;
			return false;
            Projectile.timeLeft = 10;
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }
    }
}
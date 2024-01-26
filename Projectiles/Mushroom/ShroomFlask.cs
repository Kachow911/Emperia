using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{
    public class ShroomFlask : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shroomy Flask");
		}
        public override void SetDefaults()
        {
            Projectile.width = 25;
            Projectile.height = 25;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 2;
            Projectile.timeLeft = 180;
            AIType = 48;
        }
        
        public override void AI()
        {
        	if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.GlowingMushroom, Projectile.velocity.X * 0.15f, Projectile.velocity.Y * 0.15f);
            }
        }
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Player player = Main.player[Projectile.owner];
			player.statMana+=5;
			player.ManaEffect(5);
			Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<FlaskEnemyEffect>(), 0, Projectile.knockBack, Projectile.owner, 0f, 0f);
			Projectile.Kill();
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<FlaskTileEffect>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			Projectile.Kill();
			return false;
		}
        public override void OnKill(int timeLeft)
        {
        	Terraria.Audio.SoundEngine.PlaySound(SoundID.Item107, Projectile.Center);  
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = false;
            return true;
        }
 
    }
}

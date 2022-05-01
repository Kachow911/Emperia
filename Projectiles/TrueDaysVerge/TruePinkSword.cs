using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.TrueDaysVerge
{

    public class TruePinkSword : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pink Day's Blade");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 28;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Melee;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 2;      //how many NPC will penetrate
            Projectile.timeLeft = 400;   //how many time this Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the Projectile will face the corect way
        {                                                           // |
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
			Projectile.alpha = 100 + (int) (Math.Cos(Projectile.timeLeft) * 100);
			if(Main.rand.Next(2) == 0)
			{
				int num250 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X, Projectile.position.Y - Projectile.velocity.Y), Projectile.width, Projectile.height, 66, (float)(Projectile.direction * 2), 0f, 150, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num250].noGravity = true;
				Main.dust[num250].velocity *= 0f;
			}
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Vector2 placePosition = target.Center + new Vector2(Main.rand.Next(-100, 100), -400);
			Vector2 direction = target.Center - placePosition;
			direction.Normalize();
			Projectile.NewProjectile(Projectile.InheritSource(Projectile), placePosition.X, placePosition.Y, direction.X * 13f, direction.Y * 13f, ModContent.ProjectileType<DVP2>(), Projectile.damage, 1, Main.myPlayer, 0, 0);
		}
    }
}
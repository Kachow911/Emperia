using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class ScoriaKnife : ModProjectile
    {
		bool init = false;
		Color rgb;
		int timer = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scoria Knife");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 30;       //Projectile width
            Projectile.height = 30;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Melee;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this Projectile has before disepire
            Projectile.light = 0f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the Projectile will face the corect way
        {                                                           // |
            timer++;
			int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width / 8, Projectile.height / 8, DustID.LavaMoss, 0f, 0f);
			Main.dust[dust].velocity = -Projectile.velocity;
			if (Projectile.velocity.X > 0)
			{
				Projectile.spriteDirection = 1;
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + (float) (3.14 / 4);
			}
			else if (Projectile.velocity.X < 0)
			{
				Projectile.spriteDirection = -1;
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + (float)((3.14) - (3.14 / 4));
			}
			
        }
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (Main.rand.Next(3) == 0)
			{
				target.AddBuff(BuffID.OnFire, 240);
				Projectile.Kill();
			}
		}
		public override void Kill(int timeLeft)
        {

        	 

		}
		
    }
}
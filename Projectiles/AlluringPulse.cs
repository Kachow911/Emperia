using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class AlluringPulse : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("AlluringPulse");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 8;       //Projectile width
            Projectile.height = 8;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 120;   
            Projectile.light = 0.75f;    // Projectile light
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           
			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 248, 0f, 0f, 91, new Color(255, 255, 255), 1f); //58 dust type
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity = Projectile.velocity;
            

        }
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (!target.boss)
			{
				Player player = Main.player[Projectile.owner];
				double direction = Math.Atan((target.position.Y - player.position.Y) / (target.position.X - player.position.X));
				if (player.position.X > target.position.X)
				{
					target.velocity.X = (float) (5 * Math.Cos(direction));
					target.velocity.Y = (float) (5 * Math.Sin(direction));
				}
				if (player.position.X < target.position.X)
				{
					target.velocity.X = -(float) (5 * Math.Cos(direction));
					target.velocity.Y = -(float) (5 * Math.Sin(direction));
				}
			}
			
		}
		
        
    }
}
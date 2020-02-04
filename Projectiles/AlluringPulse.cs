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
			DisplayName.SetDefault("AlluringPulse");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 8;       //projectile width
            projectile.height = 8;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 120;   
            projectile.light = 0.75f;    // projectile light
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                                                           
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 248, 0f, 0f, 91, new Color(255, 255, 255), 1f); //58 dust type
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity = projectile.velocity;
            

        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (!target.boss)
			{
				Player player = Main.player[projectile.owner];
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
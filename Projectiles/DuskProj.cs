using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class DuskProj: ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dusk Blade");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 28;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.melee = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 400;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {       
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;	
           if (projectile.velocity.X > 0)
			{
				projectile.spriteDirection = 1;
				
			}
			else
			{
				projectile.spriteDirection = -1;
			}
			
        	if(projectile.spriteDirection == 1)
        	{
        		projectile.rotation -= 1.57f;
        	}
			if(Main.rand.Next(2) == 0)
			{
            int num250 = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X, projectile.position.Y - projectile.velocity.Y), projectile.width, projectile.height, 21, (float)(projectile.direction * 2), 0f, 150, new Color(53f, 67f, 253f), 1.3f);
					Main.dust[num250].noGravity = true;
					Main.dust[num250].velocity *= 0f;
			}
        }
		public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 21, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("DuskExplosion"), projectile.damage / 2, 5f, projectile.owner);
            }
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			 target.AddBuff(mod.BuffType("IndigoInfirmary"), 240);

		}
    }
}
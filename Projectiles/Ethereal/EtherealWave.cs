using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Ethereal
{

    public class EtherealWave : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("EtherealWave");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 68;       //projectile width
            projectile.height = 36;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 80;   //how many time projectile projectile has before disepire
            projectile.light = 1f;    // projectile light
            projectile.ignoreWater = true;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {
            //projectile.alpha += 4;
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			if (Main.rand.NextBool(2))
			{
				int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229);
				Main.dust[dust].noGravity = true;
			}
			if (projectile.timeLeft < 20)
				projectile.alpha += 10;
			
        }
		 public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.GetGlobalNPC<MyNPC>().etherealDamages.Add(damage/2);
			target.GetGlobalNPC<MyNPC>().etherealCounts.Add(2);
			
		}
		public override void Kill(int timeLeft)
        {
			//
		}
        
    }
}
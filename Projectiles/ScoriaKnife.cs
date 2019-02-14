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
			DisplayName.SetDefault("Scoria Knife");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 30;       //projectile width
            projectile.height = 30;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.melee = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            timer++;
			int dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width / 8, projectile.height / 8, 258, 0f, 0f);
			Main.dust[dust].velocity = -projectile.velocity;
			if (projectile.velocity.X > 0)
			{
				projectile.spriteDirection = 1;
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + (float) (3.14 / 4);
			}
			else if (projectile.velocity.X < 0)
			{
				projectile.spriteDirection = -1;
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + (float)((3.14) - (3.14 / 4));
			}
			
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (Main.rand.Next(3) == 0)
			{
				target.AddBuff(BuffID.OnFire, 240);
				projectile.Kill();
			}
		}
		public override void Kill(int timeLeft)
        {

        	 

		}
		
    }
}
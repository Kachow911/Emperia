using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class FateFlame : ModProjectile
    {
		private int explodeRadius = 20;
		private bool init = false;
		private int damage1 = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fate's Flames");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 8;       //projectile width
            projectile.height = 8;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
           // projectile.magic = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 600;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {              
			if (!init)
			{
				init = true;
				damage1 = projectile.damage;
				projectile.damage = 0;
			}
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			if (projectile.timeLeft % 5 == 0)
			{
				for (int i = 1; i < 5; i++)
				{
					int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 191, 0f, 0f, 91, new Color(89, 249, 116), 1.5f);
					Main.dust[dust].velocity = new Vector2(0, 5).RotatedBy(45 + 90 * i);
					Main.dust[dust].noGravity = true;
				}
			}
			for (int i = 0; i < Main.npc.Length; i++)
            {
				if (projectile.Distance(Main.npc[i].Center) < explodeRadius && projectile.timeLeft % 10 == 0)
				{
					Main.npc[i].AddBuff(mod.BuffType("FatesDemise"), 720);
                    Main.npc[i].StrikeNPC(damage1, 0f, 0, false, false, false);
				}
				
					
			}
            /*Main.dust[dust].velocity *= 0.1f;
            if (projectile.velocity == Vector2.Zero)
            {
                Main.dust[dust].velocity.Y -= 1f;
                Main.dust[dust].scale = 1.2f;
            }
            else
            {
                Main.dust[dust].velocity += projectile.velocity * 0.2f;
            }
            Main.dust[dust].position.X = projectile.Center.X + 4f + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].position.Y = projectile.Center.Y + (float)Main.rand.Next(-2, 3);
            Main.dust[dust].noGravity = true;
			projectile.velocity *= .95f;*/
		}
		public override void Kill(int timeLeft)
        {
			//
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			 target.AddBuff(mod.BuffType("FatesDemise"), 720);

		}
    }
}
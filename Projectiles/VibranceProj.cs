using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class VibranceProj : ModProjectile
    {
		int NpcToHit;
		bool hitAgain;
		int hitTimer = 10;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vermillion Blade");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 28;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.melee = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 60;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
			if(Main.rand.Next(2) == 0)
			{
            int num250 = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X, projectile.position.Y - projectile.velocity.Y), projectile.width, projectile.height, 158, (float)(projectile.direction * 2), 0f, 158, new Color(53f, 67f, 253f), 1.3f);
					Main.dust[num250].noGravity = true;
					Main.dust[num250].velocity *= 0f;
			}
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
			if (hitAgain)
			{
				hitTimer--;
				if (hitTimer <= 0)
				{
					if (Main.rand.Next(5) == 0)
					{
						Main.npc[NpcToHit].StrikeNPC(projectile.damage + Main.rand.Next(-5, 5), 0f, 0, true, false, false);
					}
					else
					{
						Main.npc[NpcToHit].StrikeNPC(projectile.damage + Main.rand.Next(-5, 5), 0f, 0, false, false, false);
					}
				hitAgain = false;
				hitTimer = 10;
				}
			}
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			 target.AddBuff(mod.BuffType("VermillionVenom"), 600);
			 hitAgain = true;
			  for (int npcFinder = 0; npcFinder < 200; ++npcFinder)
              {
				  if (target == Main.npc[npcFinder])
				  {
					  NpcToHit = npcFinder;
				  }
			  }
			 hitTimer = 10;
			 //target.StrikeNPC(projectile.damage, 0f, 0, false, false, false);

		}
		public override void Kill(int timeLeft)
        {

        	 for (int i = 0; i < 360; i += 5)
				{
				Vector2 vec = Vector2.Transform(new Vector2(-4, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 158, 0f, 0f, 158, new Color(53f, 67f, 253f), 2f);
				Main.dust[num622].velocity += (vec *2f);
				Main.dust[num622].noGravity = true;
				}

		}
		
    }
}
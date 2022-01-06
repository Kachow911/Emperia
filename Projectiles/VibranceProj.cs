using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

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
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 28;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Melee;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 60;   //how many time this Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the Projectile will face the corect way
        {                                                           // |
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
			if(Main.rand.Next(2) == 0)
			{
            int num250 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X, Projectile.position.Y - Projectile.velocity.Y), Projectile.width, Projectile.height, 158, (float)(Projectile.direction * 2), 0f, 158, new Color(53f, 67f, 253f), 1.3f);
					Main.dust[num250].noGravity = true;
					Main.dust[num250].velocity *= 0f;
			}
			if (Projectile.velocity.X > 0)
			{
				Projectile.spriteDirection = 1;
				
			}
			else
			{
				Projectile.spriteDirection = -1;
			}
			
        	if(Projectile.spriteDirection == 1)
        	{
        		Projectile.rotation -= 1.57f;
        	}
			if (hitAgain)
			{
				hitTimer--;
				if (hitTimer <= 0)
				{
					if (Main.rand.Next(5) == 0)
					{
						Main.npc[NpcToHit].StrikeNPC(Projectile.damage + Main.rand.Next(-5, 5), 0f, 0, true, false, false);
					}
					else
					{
						Main.npc[NpcToHit].StrikeNPC(Projectile.damage + Main.rand.Next(-5, 5), 0f, 0, false, false, false);
					}
				hitAgain = false;
				hitTimer = 10;
				}
			}
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			 target.AddBuff(ModContent.BuffType<VermillionVenom>(), 600);
			 hitAgain = true;
			  for (int npcFinder = 0; npcFinder < 200; ++npcFinder)
              {
				  if (target == Main.npc[npcFinder])
				  {
					  NpcToHit = npcFinder;
				  }
			  }
			 hitTimer = 10;
			 //target.StrikeNPC(Projectile.damage, 0f, 0, false, false, false);

		}
		public override void Kill(int timeLeft)
        {

        	 for (int i = 0; i < 360; i += 5)
				{
				Vector2 vec = Vector2.Transform(new Vector2(-4, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 158, 0f, 0f, 158, new Color(53f, 67f, 253f), 2f);
				Main.dust[num622].velocity += (vec *2f);
				Main.dust[num622].noGravity = true;
				}

		}
		
    }
}
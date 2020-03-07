using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Emperia.Projectiles.Lightning
{
	public class LightningArrow : ModProjectile
	{
		//NPC hitNPC;
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			//projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1000;
			projectile.tileCollide = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lightning Arrow");
		}
		
		
		public override void Kill(int timeLeft)
		{
			/*Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
			Player player = Main.player[projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			projectile.localAI[1] = -1f;
			projectile.maxPenetrate = 0;
			projectile.Damage();
			if (modPlayer.graniteSet && modPlayer.graniteTime >= 1800)
			{
				for (int i = 0; i < Main.npc.Length; i++)
            	{
                	if (projectile.Distance(Main.npc[i].Center) < 90 && Main.npc[i] != hitNPC)
                    	Main.npc[i].StrikeNPC(projectile.damage + projectile.damage / 2, 0f, 0, false, false, false);
            	}
				for (int i = 0; i < 45; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 3.25f;
				}
				modPlayer.graniteTime = 0;
			}
			else
			{
				for (int i = 0; i < Main.npc.Length; i++)
            	{
                	if (projectile.Distance(Main.npc[i].Center) < 60 && Main.npc[i] != hitNPC)
                    	Main.npc[i].StrikeNPC(projectile.damage, 0f, 0, false, false, false);
            	}
				for (int i = 0; i < 30; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 2f;
				}
			}*/
			
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(mod.BuffType("ElecHostile"), 120);
		}
		
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, 0.0f, 0.0f, 15, Color.LightBlue, 0.8f);
			}
		}
	
	}
}
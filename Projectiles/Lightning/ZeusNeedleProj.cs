using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Lightning
{
	public class ZeusNeedleProj : ModProjectile
	{
		private bool init = false;
		Vector2 distFromEnemy;
		NPC target1;
		float rotation;
		int timer = 0;
	    public override void SetDefaults()
		{
			projectile.width = 90;
			projectile.height = 60;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = 5;
			projectile.thrown = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.timeLeft = 260;
			//aiType = ProjectileID.JavelinFriendly;
		}

	    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zeus' Needle");
           
		}
		public override void AI()
        {
		
			//int num250 = Dust.NewDust(new Vector2(projectile.position.X - projectile.velocity.X, projectile.position.Y - projectile.velocity.Y), projectile.width, projectile.height, 226, (float)(projectile.direction * 2), 0f, 226, new Color(53f, 67f, 253f), 1.3f);
			timer++;
			if (timer < 5)
			{
				projectile.alpha = 255;
			}
			else projectile.alpha = 0;
			Player player = Main.player[projectile.owner];
			if (projectile.position.X >= player.position.X)
			{
				projectile.spriteDirection = 1;
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 0.7f;
			}
			else
            {
				projectile.spriteDirection = -1;
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.5f;
			}

		}
		
		 public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			int count = 0;
			for (int npcFinder = 0; npcFinder < 200; ++npcFinder)
			{
				if (Main.npc[npcFinder] != target && Main.npc[npcFinder].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[npcFinder].Center, 1, 1))
				{
					Vector2 num1 = Main.npc[npcFinder].Center;
					float num2 = Math.Abs(projectile.Center.X - num1.X) + Math.Abs(projectile.Center.Y - num1.Y);
					if (num2 < 900f)
					{
						Vector2 vector2 = new Vector2(projectile.Center.X, projectile.Center.Y);
						float num11 = Main.npc[npcFinder].Center.X - vector2.X;
						float num22 = Main.npc[npcFinder].Center.Y - vector2.Y;
						float rotation = (float)Math.Atan2((double)num22, (double)num11);
						if (count < 5)
						{
							Main.npc[npcFinder].StrikeNPC(projectile.damage / 2, 0f, 0, false, false, false);
							count++;
							bool flag = true;
							while (flag)
							{
								float f = (float)Math.Sqrt((double)num11 * (double)num11 + (double)num22 * (double)num22);
								if ((double)f < 25.0)
									flag = false;
								else if (float.IsNaN(f))
								{
									flag = false;
								}
								else
								{
									float num3 = 5f / f;
									float num4 = num11 * num3;
									float num5 = num22 * num3;
									vector2.X += num4;
									vector2.Y += num5;
									num11 = Main.npc[npcFinder].Center.X - vector2.X;
									num22 = Main.npc[npcFinder].Center.Y - vector2.Y;
									int num250 = Dust.NewDust(new Vector2(vector2.X, vector2.Y), 16, 16, 226, (float)(projectile.direction * 2), 0f, 226, new Color(53f, 67f, 253f), 0.5f);
									Main.dust[num250].noGravity = true;
									
								}
								//Main.spriteBatch.Draw(mod.GetTexture("Projectiles/Tether"), new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 12, 6)), color, rotation, new Vector2((float)12 * 0.5f, (float)6 * 0.5f), 1f, SpriteEffects.None, 0.0f);
							}
						}

					}
				}
			}
			/*target.AddBuff(BuffID.Frostburn, 100);
			for (int i = 0; i < 360; i++)
		   {
			   Vector2 vec = Vector2.Transform(new Vector2(-32, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

			   if (i % 8 == 0)
			   {   //odd
				   Dust.NewDust(projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 51);
			   }

			   if (i % 9 == 0)
			   {   //even
				   vec.Normalize();
				   Dust.NewDust(projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7),51, vec.X * 2, vec.Y * 2);
			   }
		   }*/
		}
	   
	}
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System;

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
			Projectile.width = 90;
			Projectile.height = 60;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = 5;
			Projectile.DamageType = DamageClass.Ranged;
//was thrown pre 1.4
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 260;
			//AIType = ProjectileID.JavelinFriendly;
		}

	    public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Zeus' Needle");
           
		}
		public override void AI()
        {
		
			//int num250 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X, Projectile.position.Y - Projectile.velocity.Y), Projectile.width, Projectile.height, 226, (float)(Projectile.direction * 2), 0f, 226, new Color(53f, 67f, 253f), 1.3f);
			timer++;
			if (timer < 5)
			{
				Projectile.alpha = 255;
			}
			else Projectile.alpha = 0;
			Player player = Main.player[Projectile.owner];
			if (Projectile.position.X >= player.position.X)
			{
				Projectile.spriteDirection = 1;
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.7f;
			}
			else
            {
				Projectile.spriteDirection = -1;
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.5f;
			}

		}
		
		 public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Player player = Main.player[Projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.lightningSet)
				modPlayer.lightningDamage += damageDone;
			int count = 0;
			for (int npcFinder = 0; npcFinder < 200; ++npcFinder)
			{
				if (Main.npc[npcFinder] != target && Main.npc[npcFinder].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[npcFinder].Center, 1, 1))
				{
					Vector2 num1 = Main.npc[npcFinder].Center;
					float num2 = Math.Abs(Projectile.Center.X - num1.X) + Math.Abs(Projectile.Center.Y - num1.Y);
					if (num2 < 900f)
					{
						Vector2 vector2 = new Vector2(Projectile.Center.X, Projectile.Center.Y);
						float num11 = Main.npc[npcFinder].Center.X - vector2.X;
						float num22 = Main.npc[npcFinder].Center.Y - vector2.Y;
						float rotation = (float)Math.Atan2((double)num22, (double)num11);
						if (count < 5)
						{
							Main.npc[npcFinder].SimpleStrikeNPC(hit.SourceDamage / 2, 0);
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
									int num250 = Dust.NewDust(new Vector2(vector2.X, vector2.Y), 16, 16, 226, (float)(Projectile.direction * 2), 0f, 226, new Color(53f, 67f, 253f), 0.5f);
									Main.dust[num250].noGravity = true;
									
								}
								//Main.EntitySpriteDraw(Mod.Assets.Request<Texture2D>("Projectiles/Tether").Value, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 12, 6)), color, rotation, new Vector2((float)12 * 0.5f, (float)6 * 0.5f), 1f, SpriteEffects.None, 0);
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
				   Dust.NewDust(Projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 51);
			   }

			   if (i % 9 == 0)
			   {   //even
				   vec.Normalize();
				   Dust.NewDust(Projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7),51, vec.X * 2, vec.Y * 2);
			   }
		   }*/
		}
	   
	}
}

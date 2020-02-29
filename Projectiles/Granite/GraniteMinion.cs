using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles.Granite
{
	public class GraniteMinion : ModProjectile
	{
		private int npc;
		bool targetNPC = false;
		private int totalHits = 0;
		private int curHits = 0;
		private bool init = false;
		private int projDamage = 0;
		int hitTimer = 0;
		int retargetTimer = 60;
		bool initRetargDone = false;
		public override void SetDefaults()
		{
			//projectile.CloneDefaults(ProjectileID.Spazmamini);
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.netImportant = true;
			projectile.width = 26;
			projectile.height = 26;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1000;
			Main.projFrames[projectile.type] = 12;
			//aiType = -1;
			//

		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{

			return false;
		}
		public override void AI()
		{
			float projVelAbs = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
			// animation
			projectile.frameCounter++;
			if (projectile.frameCounter >= 6)
			{
				projectile.frameCounter = 0;
				if (projVelAbs < 1)
					projectile.frame = (projectile.frame + 1) % 12;
				else
					projectile.frame = (int) ((projectile.frame + projVelAbs) % 12);
			}
			//
			retargetTimer--;
			hitTimer++;
			if (!init)
            {
				projDamage = projectile.damage;
				projectile.damage = 0;
				init = true;
            }
			bool flag64 = projectile.type == mod.ProjectileType("GraniteMinion");

			MyPlayer modPlayer1 = Main.player[projectile.owner].GetModPlayer<MyPlayer>();
			if (flag64)
			{
				if (Main.player[projectile.owner].dead)
					modPlayer1.graniteMinion = false;

				if (modPlayer1.graniteMinion)
					projectile.timeLeft = 2;

			}
			targetNPC = false;
			for (int npcFinder = 0; npcFinder < 200; ++npcFinder)
			{
				if (Main.npc[npcFinder].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[npcFinder].Center, 1, 1))
				{
					Vector2 num1 = Main.npc[npcFinder].Center;
					float num2 = Math.Abs(projectile.Center.X - num1.X) + Math.Abs(projectile.Center.Y - num1.Y);
					if (num2 < 250f)
					{
						targetNPC = true;
						npc = npcFinder;

					}
					if (num2 < 50f && !initRetargDone)
                    {
						targetNPC = true;
						npc = npcFinder;
						initRetargDone = true;
						retargetTimer = 0;
					}

				}
			}
			if (retargetTimer < 0)
			{
				initRetargDone = true;
			}
			if (targetNPC && retargetTimer < 0)
            {
				aiType = -1;
				float num4 = Main.rand.Next(30, 43);
				Vector2 vector35 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
				float num5 = Main.npc[npc].Center.X - vector35.X;
				float num6 = Main.npc[npc].Center.Y - vector35.Y;
				float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
				float num8 = num4 / num7;
				num5 *= num8;
				num6 *= num8;
				projectile.velocity.X = (projectile.velocity.X * 25f + num5) / 30f;
				projectile.velocity.Y = (projectile.velocity.Y * 25f + num6) / 30f;
				if (num7 < 16f)
                {
					projectile.Center = Main.npc[npc].Center;
					if (hitTimer > 60)
                    {
						Main.npc[npc].StrikeNPC(projDamage, 0f, 0, false, false, false);
						hitTimer = 0;
						curHits++;
						totalHits++;
						if (totalHits >= 10)
                        {
							Main.PlaySound(SoundID.Item14, projectile.Center);
							Player player = Main.player[projectile.owner];
							MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
							if (modPlayer.graniteSet && modPlayer.graniteTime >= 1800)
							{
								for (int i = 0; i < Main.npc.Length; i++)
								{
									if (projectile.Distance(Main.npc[i].Center) < 90)
										Main.npc[i].StrikeNPC(projDamage * 3, 0f, 0, false, false, false);
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
									if (projectile.Distance(Main.npc[i].Center) < 60)
										Main.npc[i].StrikeNPC(projDamage + projDamage / 2, 0f, 0, false, false, false);
								}
								for (int i = 0; i < 30; ++i)
								{
									int index2 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
									Main.dust[index2].noGravity = true;
									Main.dust[index2].velocity *= 2f;
								}
							}
							projectile.timeLeft = 0;
                        }
						if (curHits >= 3)
                        {
							Vector2 perturbedSpeed = new Vector2(0, 6).RotatedByRandom(MathHelper.ToRadians(360));
							projectile.velocity = perturbedSpeed;
							retargetTimer = 120;
							curHits = 0;
						}
					}

				}
			}
            else if (retargetTimer < 0)
            {
				float num544 = 8f;
				Vector2 vector41 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
				float num545 = Main.player[projectile.owner].Center.X - vector41.X;
				float num546 = Main.player[projectile.owner].Center.Y - vector41.Y - 60f;
				float num547 = (float)Math.Sqrt((double)(num545 * num545 + num546 * num546));
				if (num547 < 100f && projectile.ai[0] == 1f && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
				{
					projectile.ai[0] = 0f;
				}
				if (num547 > 2000f)
				{
					projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
					projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.width / 2);
				}
				if (num547 > 70f)
				{
					num547 = num544 / num547;
					num545 *= num547;
					num546 *= num547;
					projectile.velocity.X = (projectile.velocity.X * 20f + num545) / 21f;
					projectile.velocity.Y = (projectile.velocity.Y * 20f + num546) / 21f;
				}
				else
				{
					if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
					{
						projectile.velocity.X = -0.15f;
						projectile.velocity.Y = -0.05f;
					}
					projectile.velocity *= 1.01f;
				}
			}
			else
            {
				aiType = -1;
				projectile.velocity *= .97f;
				
			}
		}
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 3; ++i)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 3.25f;
			}
		}
		
	}
	
}
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
		private int NPC;
		bool targetNPC = false;
		private int totalHits = 0;
		private int curHits = 0;
		private bool init = false;
		private int projDamage = 0;
		int hitTimer = 0;
		int retargetTimer = 60;
		bool initRetargDone = false;
		bool firstHits = true; 
		bool softUnlatch = false;
		public override void SetDefaults()
		{
			//Projectile.CloneDefaults(ProjectileID.Spazmamini);
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.netImportant = true;
			Projectile.width = 26;
			Projectile.height = 26;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 1000;
			Main.projFrames[Projectile.type] = 12;
			Projectile.minionSlots = 1;
			Projectile.minion = true;
			//AIType = -1;
			//

		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{

			return false;
		}
		public override void AI()
		{ 
			if (hitTimer > 60 && softUnlatch == false) //resets latch counter if enemy is killed early and deactivates firsthits
            {
				softUnlatch = true;
				firstHits = false;
				curHits = 0;
			}
			float projVelAbs = (float)Math.Sqrt((double)(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y));
			// animation
			//Projectile.frameCounter++;
			if (projVelAbs < 2)
				Projectile.frameCounter++;
			else
				Projectile.frameCounter = (int) ((float)Projectile.frameCounter + projVelAbs / 2);
			if (Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 12;
			}
			//
			retargetTimer--;
			hitTimer++;
			if (!init)
            {
				projDamage = Projectile.damage;
				Projectile.damage = 0;
				init = true;
            }
			bool flag64 = Projectile.type == ModContent.ProjectileType<GraniteMinion>();

			MyPlayer modPlayer1 = Main.player[Projectile.owner].GetModPlayer<MyPlayer>();
			if (flag64)
			{
				if (Main.player[Projectile.owner].dead)
					modPlayer1.graniteMinion = false;

				if (modPlayer1.graniteMinion)
					Projectile.timeLeft = 2;

			}
			targetNPC = false;
			NPC = -1;
			for (int npcFinder = 0; npcFinder < 200; ++npcFinder)
			{
				if ( Main.npc[npcFinder].CanBeChasedBy(Projectile, false) && Main.npc[npcFinder].GetGlobalNPC<MyNPC>().graniteMinID == Projectile.whoAmI) Main.npc[npcFinder].GetGlobalNPC<MyNPC>().graniteMinID = -1;
				if (Main.npc[npcFinder].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[npcFinder].Center, 1, 1) && !(Main.npc[npcFinder].GetGlobalNPC<MyNPC>().graniteMinID != -1 && Main.npc[npcFinder].GetGlobalNPC<MyNPC>().graniteMinID != Projectile.whoAmI))
				{
					Vector2 num1 = Main.npc[npcFinder].Center;
					float num2 = Math.Abs(Projectile.Center.X - num1.X) + Math.Abs(Projectile.Center.Y - num1.Y);
					if (num2 < 500f && retargetTimer < 0)
					{
						targetNPC = true;
						NPC = npcFinder;

					}
					if (num2 < 75f && !initRetargDone)
                    {
						//targetNPC = true;
						//NPC = npcFinder;
						initRetargDone = true;
						retargetTimer = -2;
					}

				}
			}
			if (targetNPC)
            {
				Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinID = Projectile.whoAmI;
				//Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinionLatched = true;
				//Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinionLatched = true;

			}
			if (retargetTimer < 0)
			{
				initRetargDone = true;
			}
			if (targetNPC && retargetTimer < 0)
            {
				AIType = -1;
				float num4 = Main.rand.Next(30, 43);
				Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num5 = Main.npc[NPC].Center.X - vector35.X;
				float num6 = Main.npc[NPC].Center.Y - vector35.Y;
				float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
				float num8 = num4 / num7;
				num5 *= num8;
				num6 *= num8;
				Projectile.velocity.X = (Projectile.velocity.X * 25f + num5) / 30f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 25f + num6) / 30f;
				if (num7 < 16f)
                {
					Projectile.Center = Main.npc[NPC].Center;
					Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinionLatched = true;
					int num310 = firstHits ? 30 : 60;
					if (hitTimer > num310) //hit the enemy
                    {
						softUnlatch = false;
						hitTimer = 0;
						curHits++;
						totalHits++;
						if (totalHits >= 8)
                        {
							Player player = Main.player[Projectile.owner];
							MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
							if (modPlayer.graniteSet && modPlayer.graniteTime >= 900)
							{
								for (int i = 0; i < Main.npc.Length; i++)
								{
									if (Projectile.Distance(Main.npc[i].Center) < 90)
										Main.npc[i].StrikeNPC(projDamage / 4 * 15, 0f, 0, false, false, false);
								}
								for (int i = 0; i < 45; ++i)
								{
									int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
									Main.dust[index2].noGravity = true;
									Main.dust[index2].velocity *= 3.25f;
								}
								modPlayer.graniteTime = 0;
								Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
							}
							else
							{
								for (int i = 0; i < Main.npc.Length; i++)
								{
									if (Projectile.Distance(Main.npc[i].Center) < 60)
										Main.npc[i].StrikeNPC(projDamage * 2 + projDamage / 2, 0f, 0, false, false, false);
								}
								for (int i = 0; i < 30; ++i)
								{
									int index2 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
									Main.dust[index2].noGravity = true;
									Main.dust[index2].velocity *= 2f;
								}
								Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
							}
							Projectile.timeLeft = 0;
							Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinID = -1;
						}
						else {
							Main.npc[NPC].StrikeNPC(firstHits ? projDamage + projDamage / 3 : projDamage, 0f, 0, false, false, false);
						}
						int num308 = firstHits ? 5 : 3;
						
						if (curHits >= num308) //unlatch
                        {
							Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinID = -1;
							firstHits = false;
							Vector2 perturbedSpeed = new Vector2(0, 6).RotatedByRandom(MathHelper.ToRadians(360));
							Projectile.velocity = perturbedSpeed;
							retargetTimer = 120;
							curHits = 0;
							softUnlatch = true;
							NPC = -1;
							targetNPC = false;
						}
					}

				}
			}
            else if (retargetTimer < 0)
            {
				float num544 = 6f;
				Vector2 vector41 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num545 = Main.player[Projectile.owner].Center.X - vector41.X;
				float num546 = Main.player[Projectile.owner].Center.Y - vector41.Y - 60f;
				float num547 = (float)Math.Sqrt((double)(num545 * num545 + num546 * num546));
				if (num547 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
				}
				if (num547 > 2000f)
				{
					Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
					Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.width / 2);
				}
				if (num547 > 70f)
				{
					num547 = num544 / num547;
					num545 *= num547;
					num546 *= num547;
					Projectile.velocity.X = (Projectile.velocity.X * 20f + num545) / 21f;
					Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num546) / 21f;
				}
				else
				{
					if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
					{
						Projectile.velocity.X = -0.15f;
						Projectile.velocity.Y = -0.05f;
					}
					Projectile.velocity *= 1.01f;
				}
			}
			else
            {
				AIType = -1;
				Projectile.velocity *= .97f;
				
			}
			
		}
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 3; ++i)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 3.25f;
			}
		}
		
	}
	
}
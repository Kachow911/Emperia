using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;
namespace Emperia.Projectiles.Granite
{
	public class GraniteMinion : ModProjectile
	{
		private int NPC;
		bool targetingNPC = false;
		private int totalHits = 0;
		private int curHits = 0;
		private bool init = false;
		private int projDamage = 0;
		int hitTimer = 0;
		int retargetTimer = 60;
		bool initRetargDone = false;
		bool initialDamageBoost = true;
		bool softUnlatch = false;
		public override void SetDefaults()
		{
			//Projectile.CloneDefaults(ProjectileID.Spazmamini);
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.netImportant = true;
			Projectile.width = 26;
			Projectile.height = 26;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 1000;
			Main.projFrames[Projectile.type] = 12;
			Projectile.minionSlots = 1;
			Projectile.minion = true;
			//AIType = -1;
			//

		}
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Granite Elemental");
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			player.AddBuff(ModContent.BuffType<GraniteMinionBuff>(), 2);
			if (hitTimer > 60 && softUnlatch == false) //resets latch counter if enemy is killed early and deactivates initialDamageBoost
			{
				softUnlatch = true;
				initialDamageBoost = false;
				curHits = 0;
			}
			float projVelAbs = (float)Math.Sqrt((Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y));
			// animation
			//Projectile.frameCounter++;
			if (projVelAbs < 2)
				Projectile.frameCounter++;
			else
				Projectile.frameCounter = (int)(Projectile.frameCounter + projVelAbs / 2);
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
				Projectile.originalDamage = 0;
				init = true;
			}
			bool flag64 = Projectile.type == ModContent.ProjectileType<GraniteMinion>();
			MyPlayer modPlayer1 = player.GetModPlayer<MyPlayer>();
			if (flag64)
			{
				if (player.dead)
					modPlayer1.graniteMinion = false;

				if (modPlayer1.graniteMinion)
					Projectile.timeLeft = 2;

			}
			targetingNPC = false;
			NPC = -1;
			for (int i = 0; i < 200; ++i)
			{
				if (player.MinionAttackTargetNPC > -1) FindNPC(player.MinionAttackTargetNPC);
				if (NPC == -1) FindNPC(i);
				else break;
			}
			if (targetingNPC)
			{
				Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinID = Projectile.whoAmI;
				//Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinionLatched = true;
				//Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinionLatched = true;

			}
			if (retargetTimer < 0)
			{
				initRetargDone = true;
			}
			if (targetingNPC && retargetTimer < 0)
			{
				AIType = -1;
				float num4 = Main.rand.Next(30, 43);
				Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num5 = Main.npc[NPC].Center.X - vector35.X;
				float num6 = Main.npc[NPC].Center.Y - vector35.Y;
				float num7 = (float)Math.Sqrt((num5 * num5 + num6 * num6));
				float num8 = num4 / num7;
				num5 *= num8;
				num6 *= num8;
				Projectile.velocity.X = (Projectile.velocity.X * 25f + num5) / 30f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 25f + num6) / 30f;
				if (num7 < 16f)
				{
					Projectile.Center = Main.npc[NPC].Center;
					Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinionLatched = true;
					int hitDelay = initialDamageBoost ? 30 : 60;
					if (hitTimer > hitDelay) //hit the enemy
					{
						softUnlatch = false;
						hitTimer = 0;
						curHits++;
						totalHits++;
						if (totalHits >= 8)
						{
							MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
							if (modPlayer.graniteSet && modPlayer.graniteTime >= 900)
							{
								for (int i = 0; i < Main.npc.Length; i++)
								{
									if (Projectile.Distance(Main.npc[i].Center) < 90)
										Main.npc[i].SimpleStrikeNPC((int)(projDamage * 3.75f), 0);
								}
								for (int i = 0; i < 45; ++i)
								{
									int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.MagicMirror, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
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
										Main.npc[i].SimpleStrikeNPC((int)(projDamage * 2.5f), 0);
								}
								for (int i = 0; i < 30; ++i)
								{
									int index2 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, DustID.MagicMirror, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
									Main.dust[index2].noGravity = true;
									Main.dust[index2].velocity *= 2f;
								}
								Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
							}
							Projectile.timeLeft = 0;
							Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinID = -1;
						}
						else
						{
							Main.npc[NPC].SimpleStrikeNPC(initialDamageBoost ? (int)(projDamage * 1.33f) : projDamage, 0);
						}
						int softHitLimit = initialDamageBoost ? 5 : 3;

						if (curHits >= softHitLimit) //unlatch
						{
							Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinID = -1;
							initialDamageBoost = false;
							Vector2 perturbedSpeed = new Vector2(0, 6).RotatedByRandom(MathHelper.ToRadians(360));
							Projectile.velocity = perturbedSpeed;
							retargetTimer = 120;
							curHits = 0;
							softUnlatch = true;
							NPC = -1;
							targetingNPC = false;
						}
					}

				}
			}
			else if (retargetTimer < 0)
			{
				float num544 = 6f;
				Vector2 projCenter = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
				float num545 = player.Center.X - projCenter.X;
				float num546 = player.Center.Y - projCenter.Y - 60f;
				float num547 = (float)Math.Sqrt(num545 * num545 + num546 * num546);
				if (num547 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
				}
				if (num547 > 2000f)
				{
					Projectile.position.X = player.Center.X - (Projectile.width / 2);
					Projectile.position.Y = player.Center.Y - (Projectile.width / 2);
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
		public override bool MinionContactDamage()
		{
			return true;
		}
		public override void OnKill(int timeLeft)
		{
			if (NPC != -1) Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinID = -1;
			for (int i = 0; i < 3; ++i)
			{
				int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.MagicMirror, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 3.25f;
			}
		}
		private bool FindNPC(int NPC)
		{
			if (Main.npc[NPC].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[NPC].Center, 1, 1) && !(Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinID != -1 && Main.npc[NPC].GetGlobalNPC<MyNPC>().graniteMinID != Projectile.whoAmI))
			{
				Vector2 npcCenter = Main.npc[NPC].Center;
				float dist = Math.Abs(Projectile.Center.X - npcCenter.X) + Math.Abs(Projectile.Center.Y - npcCenter.Y);
				if (dist < 500f && retargetTimer < 0)
				{
					targetingNPC = true;
					this.NPC = NPC;
					return true;
				}
				if (dist < 75f && !initRetargDone)
				{
					initRetargDone = true;
					retargetTimer = -2;
				}
			}
			return false;
		}
	}
	
}
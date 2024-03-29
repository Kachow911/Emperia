using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using static Terraria.Audio.SoundEngine;
using Emperia;
using Emperia.Projectiles;
using Emperia.Projectiles.Yeti;
using Emperia.Projectiles.Plants;
using Emperia.Projectiles.Desert;
using Emperia.Projectiles.Mushroom;
using Emperia.Buffs;
using Emperia.Items;
using Emperia.Items.Weapons;
using Emperia.Items.Sets.PreHardmode.Aquatic;
using Emperia.Items.Sets.PreHardmode.Desert;
using Emperia.Items.Accessories;
using Emperia.Items.Sets.PreHardmode.Seashell;

namespace Emperia
{
	public class MyPlayer : ModPlayer
	{
		public float gauntletBonus = 0;

		public bool ZoneGrotto = false;
		public bool ZoneVolcano = false;
		public bool cursedDash = false;
		public bool lunarDash = false;
		public bool rougeRage = false;
		public bool vermillionValor = false;
		public Item deathTalisman = null;
		public bool forbiddenOath = false;
		public bool vitalityCrystal = false;
		public bool defenseInsignia = false;
		public bool isMellowProjectile = false;
		public bool slightKnockback = false;
		public bool doubleKnockback = false;
		public bool sharkMinion = false;
		public bool graniteMinion = false;
		public bool EmberTyrant = false;
		public bool ancientPelt = false;
		public bool eruptionBottle = false;
		public bool sporeFriend = false;
		private bool dashActive = false;
		public bool goblinSet = false;
		//public bool aquaticSet = false;
		public Item aquaticSet = null;
		public bool yetiMount = false;
		public Item woodGauntlet = null;
		public float gelGauntlet = 0f;
		public bool metalGauntlet = false;
		public Item magicGauntlet = null;
		public Item boneGauntlet = null;
		public bool bloodGauntlet = false;
		public bool frostGauntlet = false;
		public bool meteorGauntlet = false;
		public bool ferocityGauntlet = false;
		public bool thermalGauntlet = false;
		public bool floralGauntlet = false;
		//public bool terraGauntlet = false;
		public Item terraGauntlet = null;
		public bool wristBrace = false;
		public bool renewedLife = false;
		public bool breakingPoint = false;
		public bool forestSetMelee = false;
		public bool forestSetRanged = false;
		public bool forestSetMage = false;
		public bool forestSetThrown = false;
		public bool forestSetSummon = false;
		public bool graniteSet = false;
		//public bool carapaceSet = false;
		public Item carapaceSet = null;
		public bool rotfireSet = false;
		public bool bloodboilSet = false;
		public bool lightningSet = false;
		public bool chillsteelSet = false;
		public bool frostleafSet = false;
		public bool frostFang = false;
		public bool warlockTorc = false;
		public bool arcaneShieldHold = false;
		public bool arcaneShieldRaised = false;
		public int manaOverdoseTime = 0;
		public int poundTime = 0;
		public int carapaceTime = 0;
		bool velocityPos = false;
		public int dayVergeProjTime = 0;
		bool canJump = false;
		bool placedPlant = false;
		bool changedVelocity;
		bool clickedLeft = false;
		bool clickedRight = false;
		List<int> hitEnemies = new List<int>();
		//List<NPC> enemiesOnscreen = new List<NPC>(); //not actually onscreen
		private int terraTime = 0;
		public int yetiCooldown = 30;
		public int graniteTime = 0;
		private int leftPresses = 0;
		private int rightPresses = 0;
		private int dashDelay = 0;
		private int pressTime = 0;
		public int sporeCount = 0;
		public int sporeBuffCount = 0;
		public int lightningDamage = 0;
		public int OathCooldown = 720;
		private int peltCounter = 120;
		private int peltRadius = 256;
		public int bloodstainedDmg = 0;
		public int nightFlame = 0;
		public int nightFlameLength = 0;

		private int forestSetMeleeCooldown = 60;
		int SporeHealCooldown = 60;
		int ferocityTime = 0;
		private int primalRageTime = 0;
		public int vileTimer = 0;
		public int eschargo = -10;
		public int desertSpikeDirection = 0;
		public int iceCannonLoad = 0;
		public int clubSwing = -1;
		public int frostFangTimer = 0;
		public int fastFallLength = 0;
		public int osmiumCooldown;
		public Vector2? sacrificingBloodCandle = null;
		public List<Vector2> bloodCandles = new List<Vector2>();
		public int candleSacrificing = -1;

		public Rectangle swordHitbox = new Rectangle(0, 0, 0, 0); //value taken from GlobalItem //also may not be necessary anymore
		public Rectangle currentItemHitbox = new Rectangle();
		public Vector2 hitboxEdge;
		public float itemLength;
		public bool noShieldSprite = false;
		//public bool mouseOverUI = false;

		public Item projItemOrigin = null;

		public Vector2 velocityBoost = Vector2.Zero;

		public Tile targetedTilePreMine;
		public Tile[,] tilesAroundCursor = new Tile[3, 3]; //also PreMine
		public int[,] wallsAroundCursorPre = new int[3, 3];
		public int targetedWallTypePre = 0;
		public bool targetedTileIsSpelunker = false;

		public override void ResetEffects()
		{
			gauntletBonus = 0;
			EmperialWorld.respawnFull = false;
			chillsteelSet = false;
			bloodboilSet = false;
			lightningSet = false;
			rotfireSet = false;
			//carapaceSet = false;
			EmberTyrant = false;
			breakingPoint = false;
			woodGauntlet = null;
			gelGauntlet = 0f;
			metalGauntlet = false;
			magicGauntlet = null;
			boneGauntlet = null;
			bloodGauntlet = false;
			//terraGauntlet = false;
			terraGauntlet = null;
			wristBrace = false;
			floralGauntlet = false;
			lunarDash = false;
			thermalGauntlet = false;
			ferocityGauntlet = false;
			meteorGauntlet = false;
			doubleKnockback = false;
			renewedLife = false;
			frostGauntlet = false;
			eruptionBottle = false;
			sharkMinion = false;
			graniteMinion = false;
			cursedDash = false;
			yetiMount = false;
			slightKnockback = false;
			sporeFriend = false;
			rougeRage = false;
			vermillionValor = false;
			deathTalisman = null;
			forbiddenOath = false;
			vitalityCrystal = false;
			defenseInsignia = false;
			goblinSet = false;
			ancientPelt = false;
			//aquaticSet = false;
			aquaticSet = null;
			forestSetMelee = false;
			forestSetRanged = false;
			forestSetMage = false;
			forestSetThrown = false;
			forestSetSummon = false;
			graniteSet = false;
			frostleafSet = false;
			frostFang = false;
			warlockTorc = false;
			arcaneShieldHold = false;
			arcaneShieldRaised = false;

			sporeBuffCount = 0;
			//bloodstainedDmg = 0;

			projItemOrigin = null;
			carapaceSet = null;
			noShieldSprite = false;


			currentItemHitbox = Rectangle.Empty;
		}

        /*public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        //public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType)
		{
			//if (junk)
			//{
			//	return;
			//}
			if (Main.hardMode && worldLayer == 0 && liquidType == 0)
			{
				int icarusChance = Convert.ToInt32(12 - power / 30); //(15 - power / 20); more basic but less fair with low power, 10 / 50 to make more common
																	 //string chanceText = icarusChance.ToString();
																	 //Main.NewText(chanceText, 255, 240, 20, false);
				if (Main.rand.NextBool(icarusChance))
				{
					caughtType = ModContent.ItemType<Icarusfish>();
				}
			}
		}*/
        /*public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
			Main.NewText(Player.position);
			//Main.NewText(Player.VisualPosition);
			Main.NewText(drawInfo.Position);
			//Main.NewText(drawInfo.mountOffSet);
			drawInfo.drawPlayer.sleeping.GetSleepingOffsetInfo(drawInfo.drawPlayer, out var posOffset2);
			//Main.NewText(posOffset2);
		}*/
        public override void PostUpdate()
		{
			targetedWallTypePre = targetedTilePreMine.WallType;
			targetedTilePreMine = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);
			targetedTileIsSpelunker = Main.tileSpelunker[targetedTilePreMine.TileType];
			for (int x = -1; x < 2; x++)
			{
				for (int y = -1; y < +2; y++)
				{
					Tile tile = (Tile)tilesAroundCursor.GetValue(x + 1, y + 1);
					//wallsAroundCursorSubset.SetValue(Collision.HitWallSubstep(Player.tileTargetX + x, Player.tileTargetY + y ), x + 1, y + 1);
					wallsAroundCursorPre.SetValue(tile.WallType, x + 1, y + 1);
					tilesAroundCursor.SetValue(Framing.GetTileSafely(Player.tileTargetX + x, Player.tileTargetY + y), x + 1, y + 1);
				}
			}
			//Main.NewText(Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY).TileFrameX + "," + Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY).TileFrameY);
			//Main.NewText(Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY).TileFrameNumber);
			//Main.NewText(Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY).TileType);
			//Main.NewText(Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY).Slope);

			//Main.NewText(terraGauntlet.ToString());
			if (graniteMinion) { Player.maxMinions += 1; } //first minion is free
			if (iceCannonLoad < 0)
			{
				iceCannonLoad++;
				if (iceCannonLoad == -1) iceCannonLoad = 2;
			}
			if (clubSwing > 0)
			{
				clubSwing--;
			}
			if (clubSwing == 0)
			{
				clubSwing = -1;
				int clubSwingDamage = Player.GetWeaponDamage(Player.inventory[Player.selectedItem]) / 3;
				if (Player.velocity.Y == 0 && !Player.mount.Active)
				{
					PlaySound(SoundID.Item27, Player.Center);
					Vector2 perturbedSpeed;
					perturbedSpeed = new Vector2(2 * Player.direction, 0);
					Projectile.NewProjectile(Player.GetSource_ItemUse(projItemOrigin), Player.position.X + 75 * Player.direction, Player.Bottom.Y - 10, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<IceSpikePre>(), clubSwingDamage, 1, Main.myPlayer, 0, 0);
				}
			}
			if (!lightningSet)
				lightningDamage = 0;
			if (lightningSet && lightningDamage >= 500)
			{
				Player.AddBuff(ModContent.BuffType<Supercharged>(), 300);
				for (int i = 0; i < 360; i++)
				{
					Vector2 vec = Vector2.Transform(new Vector2(-15, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
					if (i % 20 == 0)
					{
						int b = Dust.NewDust(Player.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), DustID.Electric);
						Main.dust[b].noGravity = true;
						Main.dust[b].velocity = vec;
					}
				}
				lightningDamage = 0;
			}
			if (graniteSet && graniteTime <= 900)
				graniteTime++;
			if (forestSetMage && primalRageTime >= 0)
			{
				primalRageTime--;
				if (primalRageTime % 5 == 0)
					Player.statMana += 2;

			}
			if (forestSetMelee)
			{
				bool doWave = false;
				forestSetMeleeCooldown--;
				for (int i = 0; i < 200; i++)
				{
					if (Player.Distance(Main.npc[i].Center) < 64 && forestSetMeleeCooldown <= 0 && !Main.npc[i].boss && !Main.npc[i].townNPC)
					{
						forestSetMeleeCooldown = 120;
						Vector2 direction = Player.Center - Main.npc[i].Center;
						direction.Normalize();
						Main.npc[i].velocity = (-direction) * 8f;
						doWave = true;
						Main.npc[i].SimpleStrikeNPC(10, 0);
					}
				}
				if (doWave)
				{
					for (int j = 0; j < 90; j++)
					{
						Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(4 * j));
						Color rgb = new Color(50, 205, 50);
						int index3 = Dust.NewDust(Player.Center, 8, 8, DustID.WhiteTorch, (float)0, (float)0, 0, rgb, 1.1f);
						Main.dust[index3].velocity = perturbedSpeed;
					}
					doWave = false;
				}
			}
			if (eruptionBottle)
			{
				if (Player.velocity.Y == 0 && Player.releaseJump)
				{
					canJump = true;
				}
				if (Player.controlJump && canJump && Player.velocity.Y != 0)
				{
					for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
						int dust = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Torch);
						int dust1 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Torch);
						Main.dust[dust1].scale = 0.8f;
						Main.dust[dust1].velocity *= 1.5f;
					}
					Player.velocity.Y = -17f;
					canJump = false;
				}
			}
			if (aquaticSet != null)
			{
				for (int i = (int)Player.position.X / 16 - 25; i < (int)Player.position.X / 16 + 25; i++)
				{
					for (int j = (int)Player.position.Y / 16 - 25; j < (int)Player.position.Y / 16 + 25; j++)
					{
						if (!Main.tile[i, j - 1].HasTile && Main.tile[i, j].HasTile && Main.rand.NextBool(5000) && !(Main.tile[i, j].TileType == TileID.Trees))
						{
							int egg = Main.rand.Next(3);
							int type;
							if (egg == 0) type = ModContent.ProjectileType<plant1>();
							else if (egg == 1) type = ModContent.ProjectileType<plant2>();
							else type = ModContent.ProjectileType<plant3>();
							Projectile.NewProjectile(Player.GetSource_Misc(ModContent.ItemType<AquaticFaceGuard>().ToString()), i * 16, j * 16 - 14, 0, 0, type, 0, 1, Main.myPlayer, 0, 0);
						}

					}
				}
			}

			dashDelay--;
			if (dashDelay >= 70)
			{
				Player.velocity.X *= .95f;
				int dust = Dust.NewDust(Player.position, Player.width, Player.height, DustID.CursedTorch, 0f, 0f, 91, new Color(2, 249, 2), 1.5f);
				Main.dust[dust].velocity = Vector2.Zero;
				Main.dust[dust].noGravity = true;
				//if (dashDelay % 3 == 0)
				{
					for (int i = 0; i < 200; i++)
					{
						if (Player.Hitbox.Intersects(Main.npc[i].Hitbox) && !hitEnemies.Contains(i) && Main.npc[i].life > 2)
						{
							hitEnemies.Add(i);
							Main.npc[i].SimpleStrikeNPC(60, 0);
							Main.npc[i].AddBuff(BuffID.CursedInferno, 120);
							if (!changedVelocity)
							{
								changedVelocity = true;
								Player.velocity.X = (-3 * Player.velocity.X) / 4;
								Player.velocity.Y -= 5;
							}
						}
					}
				}
			}
			pressTime--;
			if (pressTime <= 0)
			{
				leftPresses = 0;
				rightPresses = 0;
			}
			if (Player.releaseLeft && clickedLeft)
			{
				pressTime = 16;
				leftPresses++;
			}
			if (Player.releaseRight && clickedRight)
			{
				pressTime = 16;
				rightPresses++;
			}


			if (leftPresses >= 2 && dashDelay <= 0 && (cursedDash || lunarDash) && leftPresses > rightPresses)
			{

				pressTime = 0;
				//leftPresses = 0;
				if (!lunarDash)
				{
					changedVelocity = false;
					hitEnemies = new List<int>();
					Player.velocity.X = -19f;
					dashDelay = 100;
					for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
						int dust = Dust.NewDust(Player.position, Player.width, Player.height, DustID.CursedTorch);
						int dust1 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.CursedTorch);
						Main.dust[dust1].scale = 0.8f;
						Main.dust[dust1].velocity *= 2f;
						Main.dust[dust1].noGravity = true;
					}

				}
				else
				{
					Color rgb = new Color(0, 0, 0);
					hitEnemies = new List<int>();
					Player.velocity.X = -22f;
					dashDelay = 100;
					for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
						int ex = Main.rand.Next(4);
						if (ex == 0) rgb = new Color(254, 126, 229);
						else if (ex == 1) rgb = new Color(254, 105, 47);
						else if (ex == 2) rgb = new Color(0, 242, 170);
						else if (ex == 3) rgb = new Color(104, 214, 255);
						int index3 = Dust.NewDust(Player.position, 8, 8, DustID.Snow, 0, 0, 0, rgb, 1.1f);
						int index4 = Dust.NewDust(Player.position, 8, 8, DustID.Snow, (float)Player.velocity.X, (float)Player.velocity.Y, 0, rgb, 1.1f);
						Main.dust[index4].scale = 0.8f;
						Main.dust[index4].velocity *= 2f;
						Main.dust[index4].noGravity = true;
					}
				}
			}
			if (rightPresses >= 2 && dashDelay <= 0 && (cursedDash || lunarDash) && rightPresses > leftPresses)
			{

				pressTime = 0;
				//rightPresses = 0;
				if (!lunarDash)
				{
					changedVelocity = false;
					hitEnemies = new List<int>();
					Player.velocity.X = 19f;
					dashDelay = 100;
					for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
						int dust = Dust.NewDust(Player.position, Player.width, Player.height, DustID.CursedTorch);
						int dust1 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.CursedTorch);
						Main.dust[dust1].scale = 0.8f;
						Main.dust[dust1].velocity *= 2f;
						Main.dust[dust1].noGravity = true;
					}
				}
				else
				{
					Color rgb = new Color(0, 0, 0);
					hitEnemies = new List<int>();
					Player.velocity.X = 20f;
					dashDelay = 100;
					for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
						int ex = Main.rand.Next(4);
						if (ex == 0) rgb = new Color(254, 126, 229);
						else if (ex == 1) rgb = new Color(254, 105, 47);
						else if (ex == 2) rgb = new Color(0, 242, 170);
						else if (ex == 3) rgb = new Color(104, 214, 255);
						int index3 = Dust.NewDust(Player.position, 8, 8, DustID.Snow, 0, 0, 0, rgb, 1.1f);
						int index4 = Dust.NewDust(Player.position, 8, 8, DustID.Snow, (float)Player.velocity.X, (float)Player.velocity.Y, 0, rgb, 1.1f);
						Main.dust[index4].scale = 0.8f;
						Main.dust[index4].velocity *= 2f;
						Main.dust[index4].noGravity = true;
					}
				}

			}
			if (pressTime <= 0)
			{
				leftPresses = 0;
				rightPresses = 0;
			}

			if (forbiddenOath && Player.statLife <= Player.statLifeMax2 / 2)
			{
				OathCooldown--;
			}
			if (OathCooldown <= 0)
			{
				Player.HealEffect(20);
				Player.statLife += 20;
				OathCooldown = 720;
			}
			if (ancientPelt)
			{
				peltCounter--;
				if (peltCounter <= 0)
				{
					peltCounter = 120;
					Color rgb = new Color(160, 243, 255);
					for (int i = 0; i < 360; i += 6)
					{
						Vector2 Position = new Vector2(0, -256).RotatedBy(MathHelper.ToRadians(i));
						int dust = Dust.NewDust(Player.Center + Position, Player.width / 8, Player.height / 8, DustID.Snow, 0f, 0f, 0, rgb, 1.5f);
						Main.dust[dust].noGravity = true;
					}
					for (int i = 0; i < Main.npc.Length; i++)
					{
						if (Player.Distance(Main.npc[i].Center) < peltRadius)
						{
							Main.npc[i].AddBuff(BuffID.Chilled, 120);
							Main.npc[i].AddBuff(BuffID.Frostburn, 120);
						}
					}
				}
			}
			if (yetiMount)
			{
				yetiCooldown--;
				for (int npcFinder = 0; npcFinder < 200; ++npcFinder)
				{

					if (Player.Distance(Main.npc[npcFinder].Center) < 256 && yetiCooldown <= 0)
					{
						yetiCooldown = 30;
						Vector2 direction = Main.npc[npcFinder].Center - Player.Center - new Vector2(0, 16);
						direction.Normalize();
						//Projectile.NewProjectile(GetProjectileSource_Mount(terraGauntlet), Player.Center.X, Player.Center.Y + 16, direction.X * 7f, direction.Y * 7f, ProjectileID.SnowBallFriendly, 25, 1, Main.myPlayer, 0, 0);
						//i believe the fix for this is to put the projectile fire code in the mount as the game handles that automatically
					}
				}
			}
			clickedLeft = false;
			clickedRight = false;
			dashDelay--;
			if (Player.controlLeft) clickedLeft = true;
			if (Player.controlRight) clickedRight = true;
			if (vileTimer > 0)
			{
				vileTimer--;
				if (vileTimer == 0)
				{
					Player.statLife += 130;
					Player.HealEffect(130);
					PlaySound(SoundID.Item4, Player.Center);
				}
			}
			if (eschargo >= 0) eschargo++;
			if (eschargo > 600) eschargo = -10;
			if (Player.HasBuff(ModContent.BuffType<Waxwing>()))
			{
				if (Player.wingTime == Player.wingTimeMax)
				{
					Player.wingTime *= 0.9f;//maybe 30% boost but 15% reduction?
				}
			}
			if (frostFang == true && Player.velocity.Y == 0 && Player.velocity.X == 0)
			{
				frostFangTimer++;
			}
			else if (frostFangTimer >= 0) frostFangTimer = 0;
			else frostFangTimer++;
			if (frostFangTimer >= 60)
			{
				for (int k = 0; k < 200; k++)
				{
					NPC NPC = Main.npc[k];
					if (NPC.CanBeChasedBy(Player, false) && NPC.knockBackResist > 0f)
					{
						float distance = Vector2.Distance(NPC.Center, Player.Center);
						if ((distance < 200) && Collision.CanHitLine(Player.position, Player.width, Player.height, NPC.position, NPC.width, NPC.height))
						{
							//distance 400
							//Main.NewText("yeet2", 255, 240, 20, false);	
							NPC.AddBuff(ModContent.BuffType<Cryogenized>(), 120);
							frostFangTimer = -420;
							k = 200;
						}
					}
				}
			}
			if (manaOverdoseTime > 0) manaOverdoseTime--;
			if (arcaneShieldRaised)
			{
				Player.bodyFrame.Y = Player.bodyFrame.Height * 10;
				if (Player.velocity.Y == 0) Player.velocity.X -= Player.velocity.X / 15;
				else Player.velocity.X -= Player.velocity.X / 50;
			}
			if (velocityBoost != Vector2.Zero)
			{
				Player.velocity -= velocityBoost;
				velocityBoost = Vector2.Zero;
			}
			if (osmiumCooldown > 0) osmiumCooldown--;
		}
		public override void PostUpdateEquips()
		{
			if (warlockTorc)
			{
				//reduces mana by a third, rounded up
				Player.statManaMax2 -= (Player.statManaMax2 / 3) - ((Player.statManaMax2 / 3) % 20);
				//reduces any mana over 200 by another half (but calculated after decrease, so numbers are adjusted accordingly) | change to / 3 to buff
				if (Player.statManaMax2 > 140) Player.statManaMax2 -= ((Player.statManaMax2 - 140) / 2) - (((Player.statManaMax2 - 140) / 2) % 20);
			}
		}
		public override void ProcessTriggers(TriggersSet triggersSet)
		{

		}
		public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
		{
			if (Player.HasBuff(ModContent.BuffType<SkullBuff>()))
			{
				modifiers.FinalDamage *= 0.5f;
				Player.ClearBuff(ModContent.BuffType<SkullBuff>());
			}
		}
		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			return true;
		}
        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (dashDelay > 70) return true;
            return false;
        }
        public override void PreUpdate()
		{
			if (carapaceSet != null && poundTime == 0)
			{
				if (Main.rand.Next(20) < carapaceTime / 60)
				{
					int dust = Dust.NewDust(Player.position, Player.width, Player.height, ModContent.DustType<Dusts.CarapaceDust>());
				}
				if (carapaceTime == 479)
				{
					//int dust = Dust.NewDust(Player.position, Player.width, Player.height, 262);
					//Main.dust[dust].noGravity = true;
					PlaySound(SoundID.MaxMana, Player.Center); //change sound
				}
			}
			if (carapaceSet != null && carapaceTime < 480)
				carapaceTime++;
			if (carapaceSet != null && carapaceTime == 480 && Player.controlDown && Player.velocity.Y != 0 && !Player.mount.Active)
			{
				poundTime = 10;
				carapaceTime = 0;
			}
			if (poundTime > 0 && Player.velocity.Y != 0)
			{
				int dust = Dust.NewDust(new Vector2(Player.position.X, (float)((double)Player.position.Y + (double)Player.height - 16.0)), Player.width, 16, ModContent.DustType<Dusts.CarapaceDust>(), 0.0f, 0.0f, 0, new Color(), 1.5f);
				Main.dust[dust].velocity *= 0.1f;
				Main.dust[dust].velocity += Player.velocity * 0.2f;
				Main.dust[dust].position.X = Player.Center.X + 4f + (float)Main.rand.Next(-2, 3);
				Main.dust[dust].position.Y = Player.Center.Y + (float)Main.rand.Next(-2, 3);
				Main.dust[dust].noGravity = true;
				Player.velocity.Y = 36f;
				Player.maxFallSpeed = 36f;
				poundTime--;
			}
			if (carapaceSet != null && poundTime > 0 && Player.velocity.Y == 0)
			{
				for (int i = -12; i < 12; i += 3)
				{
					int dust = Dust.NewDust(new Vector2(Player.position.X + i, (float)((double)Player.position.Y + Player.height - 12)), Player.width, 16, ModContent.DustType<Dusts.CarapaceDust>(), 0.0f, 0.0f, 0, new Color(), 1.5f);
					//Main.dust[dust].velocity = new Vector2(i/4, -1);
					Main.dust[dust].velocity = new Vector2(i / 7, 0);
				}
				Projectile.NewProjectile(Player.GetSource_Misc(ModContent.ItemType<DuneKingMaw>().ToString()), Player.Center.X, Player.Center.Y, 2 * Player.direction, 0, ModContent.ProjectileType<DesertBurrow>(), 8, 0, Main.myPlayer, 0, 0);
				PlaySound(SoundID.NPCHit11, Player.Center);
				poundTime = 0;
			}
			//velocityPos = (Player.velocity.Y > 0);
			{
				{
					{
						if (terraTime > 0)
						{
							Player.statDefense += 15;
							//Player.thrownDamage += 0.08f;
							Player.GetDamage(DamageClass.Melee) += 0.08f;
							Player.GetDamage(DamageClass.Summon) += 0.08f;
							Player.GetDamage(DamageClass.Magic) += 0.08f;
							Player.GetDamage(DamageClass.Ranged) += 0.08f;
							Player.moveSpeed += 0.25f;
						}
						if (ferocityTime > 0)
						{
							Player.statDefense += 5;
							//Player.thrownDamage += 0.05f;
							Player.GetDamage(DamageClass.Melee) += 0.05f;
							Player.GetDamage(DamageClass.Summon) += 0.05f;
							Player.GetDamage(DamageClass.Magic) += 0.05f;
							Player.GetDamage(DamageClass.Ranged) += 0.05f;
							Player.moveSpeed += 0.20f;
						}
						SporeHealCooldown--;
						if (SporeHealCooldown <= 0)
						{
							if (sporeBuffCount > 0 && Player.active && !Player.dead)
							{
								if (sporeBuffCount > 6)
									sporeBuffCount = 6;
								Player.statLife += sporeBuffCount;
								Player.HealEffect(sporeBuffCount);
							}
							SporeHealCooldown = 120;
						}
					}
				}
			}
			if (fastFallLength > 0)
			{
				Player.maxFallSpeed = 20f;
				fastFallLength--;
			}

			/*enemiesOnscreen.Clear();
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				if ((Main.npc[k].Center - Player.Center).Length() < 1672 && Main.npc[k].active) enemiesOnscreen.Add(Main.npc[k]);
			}*/
		}

        public override void PostUpdateBuffs()
        {
			if (Player.HasBuff(ModContent.BuffType<GraniteMinionBuff>())) Player.maxMinions ++;

			if (sacrificingBloodCandle != null)
			{
				Vector2 bloodCandlePos = new Vector2(sacrificingBloodCandle.Value.X * 16 + 4, sacrificingBloodCandle.Value.Y * 16);
				if (Vector2.Distance(bloodCandlePos, Player.Center) > 800)
                {
					sacrificingBloodCandle = null;
					candleSacrificing = -1;
				}
				else
				{
					if (Player.statLife > 5)
					{
						candleSacrificing++;

						if (candleSacrificing % 2 == 0)
						{
							Player.statLife -= 5;
							CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y - 20, Player.width, Player.height), Color.Red, "5", false, true);
						}

						for (int d = 0; d < 3; d++)
						{
							Vector2 vel = bloodCandlePos - Player.Center;
							vel.Normalize();
							vel *= Vector2.Distance(bloodCandlePos, Player.Center) / 11;
							Dust dust = Dust.NewDustPerfect(Player.Center, 183, vel, 0, default(Color), 1.5f);
							dust.noGravity = true;
							dust.fadeIn = 1.7f;

							if (d == 1) dust.position -= vel * 0.33f;
							if (d == 2) dust.position -= vel * 0.66f;
						} //spawn 3 evenly spaced dusts
					}
					else
					{
						Main.tile[(int)sacrificingBloodCandle.Value.X, (int)sacrificingBloodCandle.Value.Y].TileFrameX = 0;
						Tiles.BloodCandleTile.FlameSwitchFX(bloodCandlePos);
						bloodCandles.Add((Vector2)sacrificingBloodCandle);
						sacrificingBloodCandle = null;
						candleSacrificing = -1;
						//if (Player.pStone) could make it give potion sickness
					}
				}
			}
		}
		public override void PreUpdateBuffs()
		{
			if (bloodCandles.Any())
			{
				foreach (Vector2 candle in bloodCandles.ToList())
				{
					Point point = new Point((int)(Player.Center.X / 16), (int)(Player.Center.Y / 16));
					Rectangle effectRectangle = new Rectangle(point.X - Main.buffScanAreaWidth / 2, point.Y - Main.buffScanAreaHeight / 2, Main.buffScanAreaWidth, Main.buffScanAreaHeight);
					//Rectangle tileRectangle = new Rectangle((int)candle.X, (int)candle.Y, 1, 1);
					if (Main.tile[(int)candle.X, (int)candle.Y].TileType != ModContent.TileType<Tiles.BloodCandleTile>() || Main.tile[(int)candle.X, (int)candle.Y].TileFrameX != 0) bloodCandles.Remove(candle);
					//else if (Vector2.Distance(Player.Center, candle * 16) < 500) //else if ()

					else if (effectRectangle.Contains((int)candle.X, (int)candle.Y))
					{
						Player.AddBuff(ModContent.BuffType<BloodCandleBuff>(), 2);
						Player.ClearBuff(BuffID.WaterCandle); //this is purely visual, actual spawn stuff code is in MyNPC EditSpawnRate
					}
					else
					{
						UnloadBloodCandle(candle);
						bloodCandles.Remove(candle);
					}
					//Main.NewText(effectRectangle);
					//Main.NewText(new Point((int)candle.X, (int)candle.Y));
				}
			}
        }
		//public override world
		public override void UpdateDead()
		{
			UnloadBloodCandles();
		}
		public void UnloadBloodCandles(bool dust = true)
		{
			if (bloodCandles.Any())
			{
				foreach (Vector2 candle in bloodCandles.ToList())
				{
					UnloadBloodCandle(candle, dust);
				}
				bloodCandles.Clear();
			}
		}
		public void UnloadBloodCandle(Vector2 candle, bool dust = true)
        {
			if (Main.tile[(int)candle.X, (int)candle.Y].TileType == ModContent.TileType<Tiles.BloodCandleTile>()) //paranoia
			{
				Main.tile[(int)candle.X, (int)candle.Y].TileFrameX = 18;
				if (dust) Tiles.BloodCandleTile.FlameSwitchFX(new Vector2(candle.X * 16, candle.Y * 16));
			}
		}
		public override void ModifyLuck(ref float luck)
		{
			if (Player.HasBuff(ModContent.BuffType<BloodCandleBuff>()))
			{
				luck += 0.1f;
				//Player.luckMaximumCap += 0.1f; this doesnt reset per frame
			}
		}
		public override void UpdateBadLifeRegen()
		{
			if (Player.HasBuff(ModContent.BuffType<NocturnalFlame>()))
			{
				if (Player.lifeRegen > 0) Player.lifeRegen = 0;
				Player.lifeRegenTime = 0;
				nightFlameLength++;
				nightFlame = (1 + (int)Math.Floor(nightFlameLength / 600f)) * 2;
				if (nightFlame > 10) nightFlame = 10;
				Player.lifeRegen -= nightFlame * 2;
			}
			else
			{
				nightFlameLength = 0;
				nightFlame = 0;
			}
		}
		public override void UpdateLifeRegen()
        {

		}
        public override void OnRespawn()
		{
			if (EmperialWorld.respawnFull)
			{
				Player.statLife = Player.statLifeMax2;
				Player.statMana = Player.statManaMax2;

			}
		}
		public override void OnHurt(Player.HurtInfo info)
		{
			graniteTime = 0;
			if (terraGauntlet != null)
				terraTime = 180;
			for (int i = 0; i < Main.projectile.Length; i++)
			{
				if (Main.projectile[i].type == ModContent.ProjectileType<Needle>() || Main.projectile[i].type == ModContent.ProjectileType<HauntedRevolver>())
					Main.projectile[i].Kill();
			}
			if (thermalGauntlet)
			{
				Vector2 placePosition = Player.Center + new Vector2(Main.rand.Next(-100, 100), -500);
				Vector2 direction = Player.Center - placePosition;
				int p = Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), placePosition.X, placePosition.Y, direction.X * 12f, direction.Y * 12f, ProjectileID.Meteor1, 60, 1, Main.myPlayer, 0, 0);
				Main.projectile[p].friendly = true;
				Main.projectile[p].hostile = false;
				Main.projectile[p].scale = 0.7f;
			}
			if (Player.HasBuff(ModContent.BuffType<Bloodstained>()))
			{
				bloodstainedDmg = info.Damage;
				Player.AddBuff(ModContent.BuffType<Bloodstained2>(), 600);
				Player.ClearBuff(ModContent.BuffType<Bloodstained>());
			}
			/*else if (Player.HasBuff(ModContent.BuffType<Bloodstained2>()))
			{
				bloodstainedDmg = 0;
				Player.ClearBuff(ModContent.BuffType<Bloodstained2>());
			}*/ //nerf for bloodstained gauntlet, could also make it reduce the buff timer
		}

		public void FindClosestEntityPoint(Vector2 point, Entity entity, out Vector2 closestPoint)
        {
			closestPoint = new Vector2(entity.Center.X, entity.Center.Y);

			if (entity.Top.Y > point.Y) closestPoint.Y = entity.Top.Y;
			else if (entity.Bottom.Y < point.Y) closestPoint.Y = entity.Bottom.Y;
			else closestPoint.Y = point.Y;

			if (entity.Left.X > point.X) closestPoint.X = entity.Left.X;
			else if (entity.Right.X < point.X) closestPoint.X = entity.Right.X;
			else closestPoint.X = point.X;
			//Projectile.NewProjectile(new EntitySource_Misc("heh"), closestPoint, Vector2.Zero, ModContent.ProjectileType<RedPixel>(), 0, 0);
		}
		public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)/* tModPorter If you don't need the Item, consider using ModifyHitNPC instead */
		{
			if (item.CountsAsClass(DamageClass.Melee))
			{
				if (gauntletBonus > 0)
				{
					FindClosestEntityPoint(Player.Center, target, out Vector2 closestPoint);
					float distance = Vector2.Distance(Player.Center, closestPoint);

					float distanceMult = ((itemLength * 0.85f) - distance) / (itemLength * 0.85f); //itemlength is based on the technical longest reach possible which almost never happens in normal gameplay; i'd rather reduce that so i can balance the damage better
					if (distanceMult < 0) distanceMult = 0;
					/*Main.NewText(itemLength);
					Main.NewText(distance);
					Main.NewText(distanceMult);
					Main.NewText((((distanceMult > .8f) ? .8f : distanceMult) + .2f).ToString());
					Main.NewText(Vector2.Distance(Player.Center, closestPoint).ToString());*/
					float damageMult = gauntletBonus * (((distanceMult > .8f) ? .8f : distanceMult) + .2f) + 1; //caps damage multiplier at 65% distance
					{
						//int oldDamage = damage;
						if ((target.width + target.height / 2) > 48) //this is for big enemies or bosses
						{
							modifiers.SourceDamage *= damageMult;
						}
						else
						{
							modifiers.SourceDamage *= damageMult / 2;
						}
						//Main.NewText((damage - oldDamage).ToString(), 255, 240, 20);
					}
				}
				if (slightKnockback)
				{
					modifiers.Knockback *= 1.1f;
				}

				if (doubleKnockback)
				{
					modifiers.Knockback *= 2f;
				}
				if ((ferocityGauntlet || terraGauntlet != null) && Main.rand.Next(10) == 0)
					modifiers.SourceDamage *= 2;
				if (Player.HasBuff(ModContent.BuffType<Goliath>())) modifiers.SourceDamage *= 1.1f;
			}
		}
		public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (Player.HasBuff(ModContent.BuffType<Goliath>()) && proj.aiStyle == ProjAIStyleID.ShortSword) modifiers.SourceDamage *= 1.1f;
		}
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			if (rougeRage)
			{
				modifiers.CritDamage *= 1.1f;
			}
			if (vermillionValor)
			{
				modifiers.CritDamage *= 1.13f;
			}
		}
        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
		{
			if (item.CountsAsClass(DamageClass.Melee))
			{
				if (target.life <= 0 && terraGauntlet != null)
				{
					//Item item = this.terraGauntletItem;
					for (int i = 0; i < Main.rand.Next(3, 6); i++)
					{
						Vector2 perturbedSpeed = new Vector2(0, 4).RotatedByRandom(MathHelper.ToRadians(360));
						int p = Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Projectiles.TerraG.TerraBoltHost>(), 75, 1, Main.myPlayer, 0, 0);
					}
				}
				if (floralGauntlet && Main.rand.Next(20) == 0)
				{
					int lifeToHeal = damageDone;
					if (lifeToHeal > 25)
						lifeToHeal = 25;
					Player.statLife += lifeToHeal;
					Player.HealEffect(lifeToHeal);
				}
				if (ferocityGauntlet)
				{
					ferocityTime = 180;
				}
				if (meteorGauntlet)
				{
					target.AddBuff(BuffID.OnFire, 120);
					if (Main.rand.Next(3) == 0)
					{
						Vector2 placePosition = target.Center + new Vector2(Main.rand.Next(-100, 100), -500);
						Vector2 direction = target.Center - placePosition;
						int p = Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), placePosition.X, placePosition.Y, direction.X * 12f, direction.Y * 12f, ProjectileID.Meteor1, 30, 1, Main.myPlayer, 0, 0);
						Main.projectile[p].friendly = true;
						Main.projectile[p].hostile = false;
						Main.projectile[p].scale = 0.7f;
					}
				}
				if (thermalGauntlet)
				{
					if (target.life <= 0)
					{
						for (int i = 0; i < Main.rand.Next(3, 6); i++)
						{
							int type = 0;
							int x = Main.rand.Next(2);
							if (x == 0) type = ModContent.ProjectileType<ThermalBoltCold>();
							if (x == 1) type = ModContent.ProjectileType<ThermalBoltHot>();
							Vector2 perturbedSpeed = new Vector2(0, 4).RotatedByRandom(MathHelper.ToRadians(360));
							int p = Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, 20, 1, Main.myPlayer, 0, 0);
						}
					}
					else
					{
						target.AddBuff(BuffID.OnFire, 180);
						target.AddBuff(BuffID.Frostburn, 180);
						if (Main.rand.NextBool(7) && !target.boss)
						{
							target.AddBuff(ModContent.BuffType<Frozen>(), 120);
						}
					}
				}
				if (terraGauntlet != null)
				{
					target.AddBuff(BuffID.OnFire, 180);
					target.AddBuff(BuffID.Frostburn, 180);
					target.AddBuff(BuffID.ShadowFlame, 180);

				}
				if (frostGauntlet)
				{
					if (target.life <= 0)
					{
						for (int i = 0; i < Main.rand.Next(2, 4); i++)
						{
							Vector2 perturbedSpeed = new Vector2(0, 4).RotatedByRandom(MathHelper.ToRadians(360));
							int p = Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<IceShard2>(), 20, 1, Main.myPlayer, 0, 0);
						}
					}
					else
					{
						if (Main.rand.NextBool(10) && !target.boss)
						{
							target.AddBuff(ModContent.BuffType<Frozen>(), 120);
						}
					}
				}
				if (woodGauntlet != null && !woodGauntlet.IsAir)
				{
					Vector2 rotVector = target.Center - Player.Center;
					if (Main.rand.Next(6 + (30 / damageDone)) == 0)
					{
						rotVector.Normalize();
						Projectile.NewProjectile(Player.GetSource_Accessory(woodGauntlet), Player.Center.X, Player.Center.Y, rotVector.X * 10f, rotVector.Y * 10f, ModContent.ProjectileType<Splinter>(), 7, hit.Knockback, Main.myPlayer, 0, 0);
					}
				}
				if (gelGauntlet > 0 && target.knockBackResist == 0f && !item.GetGlobalItem<GItem>().noGelGauntlet)
				{
					FindClosestEntityPoint(Player.Center, target, out Vector2 closestPoint);
					float distance = Vector2.Distance(Player.Center, closestPoint);

					Vector2 direction = Player.Center - target.Center;
					direction.Normalize();

					Vector2 oldSpeedFactor = Player.velocity;
					if (Math.Abs(oldSpeedFactor.X) > 7f) oldSpeedFactor.X = 7f * Math.Sign(oldSpeedFactor.X);
					if (Math.Abs(oldSpeedFactor.Y) > 7f) oldSpeedFactor.X = 7f * Math.Sign(oldSpeedFactor.Y);

					Player.velocity.Y = (direction.Y * gelGauntlet) + oldSpeedFactor.Y / 2;
					Player.velocity.X = (direction.X * (7f - (distance / 100)) * gelGauntlet) + oldSpeedFactor.X / 2;

					//old formula, doesn't calculate for disance
					/*Vector2 direction = Player.Center - target.Center;
					direction.Normalize();
					Player.velocity.Y = direction.Y;
					float minimumSpeed = 2f;
					if (direction.X < 0) { minimumSpeed = -2f; }
					Player.velocity.X = direction.X * (5f) + minimumSpeed;
					*/
				}
				if (metalGauntlet)
				{
					Player.AddBuff(ModContent.BuffType<AlloyArmor>(), 90);
				}
				if (magicGauntlet != null && !magicGauntlet.IsAir)
				{
					if (Main.rand.Next(8 + (60 / damageDone)) == 0 || target.life <= 0 && Main.rand.Next(3) == 0)
					{ //chance based on damage and if the attack killed
						NPC chosenNPC = target;
						for (int k = 0; k < 200; k++)
						{
							NPC NPC = Main.npc[k];
							if (NPC.CanBeChasedBy(Player, false))
							{
								float distance = Vector2.Distance(NPC.Center, Player.Center);
								if (distance < 400 && chosenNPC == target || distance < 400 && NPC.life > chosenNPC.life && NPC != target)
								{ //finds the highest hp enemy besides the target
									chosenNPC = NPC;
								}
							}
							if (k == 199 && chosenNPC != target)
							{
								float xPosition = chosenNPC.Left.X - 30f;
								float projDirection = 0.1f;
								if (chosenNPC.Center.X < Player.Center.X)
								{
									xPosition = chosenNPC.Right.X + 30f;
									projDirection = -0.1f;
								}
								Projectile.NewProjectile(Player.GetSource_Accessory(magicGauntlet), xPosition, chosenNPC.Center.Y - 35f, projDirection, 0, ModContent.ProjectileType<EnchantedBlade>(), 40, 4f, Player.whoAmI);
								PlaySound(SoundID.Item8, chosenNPC.Center);
							}
						}
					}
				}
				if (boneGauntlet != null && !boneGauntlet.IsAir && !Player.HasBuff(ModContent.BuffType<SkullBuff>()))
				{
					if (Main.rand.NextBool(30 + (160 / damageDone)))
					{
						Projectile.NewProjectile(Player.GetSource_Accessory(boneGauntlet), Player.Center.X, Player.Center.Y, 0, 0, ModContent.ProjectileType<GauntletSkull>(), 0, 4f, Player.whoAmI);
						Player.AddBuff(ModContent.BuffType<SkullBuff>(), 1200);
						PlaySound(SoundID.Item8, Player.Center);
					}
				}
				if (bloodGauntlet && !Player.HasBuff(ModContent.BuffType<Bloodstained>()) && !Player.HasBuff(ModContent.BuffType<Bloodstained2>()))
				{
					if (Main.rand.NextBool(25 + (240 / damageDone))) //this can somehow sometimes divide by zero? i dont see how onhit can activate if damage is 0
					{
						Player.AddBuff(ModContent.BuffType<Bloodstained>(), 1200);
						PlaySound(SoundID.Item8, Player.Center);
					}
				}
				if (bloodGauntlet && Player.HasBuff(ModContent.BuffType<Bloodstained2>()))
				{
					if (bloodstainedDmg >= damageDone / 2)
					{
						Player.statLife += damageDone / 2;
						Player.HealEffect(damageDone / 2);
						bloodstainedDmg -= damageDone / 2;
					}
					else
					{
						Player.statLife += bloodstainedDmg;
						Player.HealEffect(bloodstainedDmg);
						bloodstainedDmg = 0;
						Player.ClearBuff(ModContent.BuffType<Bloodstained2>());
						PlaySound(SoundID.Item28, Player.Center);
					}

				}
				if (goblinSet) Player.AddBuff(ModContent.BuffType<GoblinsCelerity>(), 180);
			}

			if (hit.Crit && target.life <= 0 && deathTalisman != null && !deathTalisman.IsAir && !target.HasBuff(ModContent.BuffType<FatesDemise>()))
			{
				int damage1 = 0;
				if (target.lifeMax > 1500)
				{
					damage1 = 300;
				}
				else
				{
					damage1 = target.lifeMax / 5;
				}
				for (int i = 0; i < 6; i++)
				{
					Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
					Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FatesFlames>(), damage1, 1, Main.myPlayer, 0, 0);
					PlaySound(SoundID.NPCDeath52, target.Center);
				}
			}
			if (hit.Crit && deathTalisman != null && !deathTalisman.IsAir)
			{
				target.AddBuff(ModContent.BuffType<FatesDemise>(), 720);
			}
			if (breakingPoint)
			{
				target.AddBuff(BuffID.OnFire, 240);
			}
			if (defenseInsignia)
			{
				int increasedChance = damageDone / 50;
				if (increasedChance > 8) increasedChance = 8;
				if (Main.rand.NextFloat(80) <= (2 + increasedChance))
				{
					Item.NewItem(Player.GetSource_OnHit(target, ModContent.ItemType<InsigniaofDefense>().ToString()), (int)target.position.X, (int)target.position.Y, target.width, target.height, ModContent.ItemType<ProtectiveEnergy>());
				}
			}
			if (hit.Crit && sporeFriend && Main.rand.NextBool(3))
			{
				if (sporeCount <= 0)
				{
					for (int i = 0; i < 6; i++)
					{
						Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), Player.Center.X, Player.Center.Y, 0, 0, ModContent.ProjectileType<HelpfulSpore>(), 30, 0, Player.whoAmI, ai1: i);
						sporeCount++;
					}
				}
				Player.AddBuff(ModContent.BuffType<Spored>(), 2);
			}
		}
		public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
		{
			if (proj.CountsAsClass(DamageClass.Magic) && forestSetMage && Main.rand.Next(10) == 0)
			{
				primalRageTime = 600;
			}
			if (breakingPoint)
			{
				target.AddBuff(BuffID.OnFire, 240);
			}
			if (terraGauntlet != null)
			{
				target.AddBuff(BuffID.OnFire, 180);
				target.AddBuff(BuffID.Frostburn, 180);
				target.AddBuff(BuffID.ShadowFlame, 180);

			}
			if (floralGauntlet && Main.rand.Next(20) == 0)
			{
				int lifeToHeal = damageDone;
				if (lifeToHeal > 25)
					lifeToHeal = 25;
				Player.statLife += lifeToHeal;
				Player.HealEffect(lifeToHeal);
			}
			if (target.life <= 0 && terraGauntlet != null)
			{
				for (int i = 0; i < Main.rand.Next(3, 6); i++)
				{
					Vector2 perturbedSpeed = new Vector2(0, 4).RotatedByRandom(MathHelper.ToRadians(360));
					int p = Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Projectiles.TerraG.TerraBoltHost>(), 75, 1, Main.myPlayer, 0, 0);
				}
			}
			if (ferocityGauntlet)
			{
				ferocityTime = 180;
			}
			if (hit.Crit && target.life <= 0 && deathTalisman != null && !deathTalisman.IsAir && !target.HasBuff(ModContent.BuffType<FatesDemise>()))
			{
				int damage1 = 0;
				if (target.lifeMax > 1500)
				{
					damage1 = 300;
				}
				else
				{
					damage1 = target.lifeMax / 5;
				}
				for (int i = 0; i < 6; i++)
				{
					Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
					Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FatesFlames>(), damage1, 1, Main.myPlayer, 0, 0);
					PlaySound(SoundID.NPCDeath52, target.Center);
				}
			}
			if (meteorGauntlet)
			{
				target.AddBuff(BuffID.OnFire, 120);
				if (Main.rand.Next(3) == 0)
				{
					Vector2 placePosition = target.Center + new Vector2(Main.rand.Next(-100, 100), -500);
					Vector2 direction = target.Center - placePosition;
					int p = Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), placePosition.X, placePosition.Y, direction.X * 12f, direction.Y * 12f, ProjectileID.Meteor1, 30, 1, Main.myPlayer, 0, 0);
					Main.projectile[p].friendly = true;
					Main.projectile[p].hostile = false;
					Main.projectile[p].scale = 0.7f;
				}
			}
			if (frostGauntlet)
			{
				if (target.life <= 0)
				{
					for (int i = 0; i < Main.rand.Next(2, 4); i++)
					{
						Vector2 perturbedSpeed = new Vector2(0, 6).RotatedByRandom(MathHelper.ToRadians(360));
						int p = Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<IceShard2>(), 20, 1, Main.myPlayer, 0, 0);
					}
				}
				else
				{
					if (Main.rand.NextBool(10) && !target.boss)
					{
						target.AddBuff(ModContent.BuffType<Frozen>(), 120);
					}
				}
			}
			if (thermalGauntlet)
			{
				if (target.life <= 0)
				{
					for (int i = 0; i < Main.rand.Next(3, 6); i++)
					{
						int type = 0;
						int x = Main.rand.Next(2);
						if (x == 0) type = ModContent.ProjectileType<ThermalBoltCold>();
						if (x == 1) type = ModContent.ProjectileType<ThermalBoltHot>();
						Vector2 perturbedSpeed = new Vector2(0, 4).RotatedByRandom(MathHelper.ToRadians(360));
						int p = Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, 20, 1, Main.myPlayer, 0, 0);
					}
				}
				else
				{
					target.AddBuff(BuffID.OnFire, 180);
					target.AddBuff(BuffID.Frostburn, 180);
					if (Main.rand.NextBool(7) && !target.boss)
					{
						target.AddBuff(ModContent.BuffType<Frozen>(), 120);
					}
				}
			}
			if (hit.Crit && deathTalisman != null && !deathTalisman.IsAir)
			{
				target.AddBuff(ModContent.BuffType<FatesDemise>(), 720);
			}
			if (defenseInsignia)
			{
				int increasedChance = damageDone / 50;
				if (increasedChance > 9) increasedChance = 9;
				if (Main.rand.NextFloat(80) <= (1 + increasedChance))
				{
					Item.NewItem(Player.GetSource_OnHit(target, ModContent.ItemType<InsigniaofDefense>().ToString()), (int)target.position.X, (int)target.position.Y, target.width, target.height, ModContent.ItemType<ProtectiveEnergy>());
				}
			}
			if (hit.Crit && sporeFriend && Main.rand.NextBool(3))
			{
				if (sporeCount <= 0)
				{
					for (int i = 0; i < 6; i++)
					{
						Projectile.NewProjectile(Player.GetSource_Accessory(terraGauntlet), Player.Center.X, Player.Center.Y, 0, 0, ModContent.ProjectileType<HelpfulSpore>(), 30, 0, Player.whoAmI, ai1: i);
						sporeCount++;
					}
				}
				Player.AddBuff(ModContent.BuffType<Spored>(), 2);
			}
		}
		public override void PreUpdateMovement()
		{
			if ((Player.inventory[Player.selectedItem].type == ModContent.ItemType<SeashellPickaxe>() || Player.inventory[Player.selectedItem].type == ModContent.ItemType<SeashellHamaxe>()) && (!Player.mount.Active || !Player.mount.Cart)) Player.trident = true;
			if (velocityBoost != Vector2.Zero)
			{
				if (Player.velocity.Y > 0 && velocityBoost.Y < 0) Player.velocity.Y = -0.1f;
				//Main.NewText(Player.velocity.ToString());
				Player.velocity += velocityBoost;
				//Main.NewText(velocityBoost.ToString(), 0);
				//Main.NewText(Player.velocity.ToString());
			}
		}
		public override void FrameEffects()
		{
			if (arcaneShieldHold)
			{
				Player.shield = (sbyte)EquipLoader.GetEquipSlot(Mod, "ArcaneShield", EquipType.Shield);
			}
			if (noShieldSprite)
            {
				Player.shield = (sbyte)EquipLoader.GetEquipSlot(Mod, "BlankShield", EquipType.Shield); //this is a dummy sprite that just makes nothing else cover up the palette
			}
		}
		//public override void ApplyEquipFunctional(int itemSlot, Item currentItem)
		//{
		//	if (currentItem.type == ModContent.ItemType<Items.Accessories.Gauntlets.BloodGauntlet>())
		//	{
		//		terraGauntlet2 = currentItem;
		//	}
		//}
		public override void UpdateEquips()
		{
			Item strongestGauntlet = null;
			for (int i = 3; i <= (8 + Player.GetAmountOfExtraAccessorySlotsToShow()); i++)
			{
				if (!Player.armor[i].IsAir && Player.armor[i].GetGlobalItem<GItem>().gauntletPower > 0)
                {
					if (strongestGauntlet == null) strongestGauntlet = Player.armor[i];
					else if (strongestGauntlet.GetGlobalItem<GItem>().gauntletPower < Player.armor[i].GetGlobalItem<GItem>().gauntletPower)
                    {
						strongestGauntlet.GetGlobalItem<GItem>().inactiveGauntlet = true;
						strongestGauntlet = Player.armor[i];
					}
					else Player.armor[i].GetGlobalItem<GItem>().inactiveGauntlet = true;
				}

				//Main.NewText(Player.armor[i].type.ToString());
				//Main.NewText(ModContent.ItemType<Items.Accessories.Gauntlets.PrimordialGauntlet>().ToString());
				if (Player.armor[i].type == ModContent.ItemType<Items.Accessories.Gauntlets.PrimordialGauntlet>()) { terraGauntlet = Player.armor[i]; }
				//if (Player.armor[i].type == ModContent.ItemType<Items.Accessories.Gauntlets.PrimordialGauntlet>()); { Main.NewText("yuup"); }

				//if (Player.armor[i].type == ModContent.ItemType<Items.Accessories.Gauntlets.EnchantedGauntlet>()); { terraGauntlet2 = Player.armor[i]; }
				//if (Player.armor[i].type == ModContent.ItemType<Items.Accessories.Gauntlets.WoodweaversGauntlet>()); { terraGauntlet2 = Player.armor[i]; }

				//IMPORTANT TODO: if primordial gauntlet works as expected, change other accessories.

			}
			if (strongestGauntlet != null)
			{
				strongestGauntlet.GetGlobalItem<GItem>().inactiveGauntlet = false;
				gauntletBonus = strongestGauntlet.GetGlobalItem<GItem>().gauntletPower;
			}
		}
        public override void LoadData(TagCompound tag)
        {
			base.LoadData(tag);
        }
        public override void SaveData(TagCompound tag)
        {
			UnloadBloodCandles(false);
            base.SaveData(tag);
        }
        public Vector2 MouseDirection()
        {
			Vector2 mouseDirection = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - Player.Center; //depending on the side of the player the mouse is, the sprite wobbles?? why??
			if (Player.gravDir == -1f)
			{
				mouseDirection.Y = (Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - Player.Center.Y;
			}
			mouseDirection = new Vector2(Math.Sign((mouseDirection.X == 0f) ? (Player.direction) : mouseDirection.X), 0f);
			return mouseDirection;
		}  //simplifies to either be 1 or -1
		/*private static List<ushort> ItemCheck_GetTileCutIgnoreList(Item item)
		{
			List<ushort> result = null;
			if (item.type == ModContent.ItemType<ArcaneShield>())
			{
				result = new List<ushort>(new ushort[23]
				{
					3, 24, 52, 61, 62, 71, 73, 74, 82, 83,
					84, 110, 113, 115, 184, 205, 201, 519, 518, 528,
					529, 530, 549
				});
			}
			return result;
		}
		public override void ItemCheck_CutTiles(Item sItem, Rectangle itemRectangle, List<ushort> ignoreList)
		{

			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			int minX = itemRectangle.X / 16;
			int maxX = (itemRectangle.X + itemRectangle.Width) / 16 + 1;
			int minY = itemRectangle.Y / 16;
			int maxY = (itemRectangle.Y + itemRectangle.Height) / 16 + 1;
			Utils.ClampWithinWorld(ref minX, ref minY, ref maxX, ref maxY);
			for (int i = minX; i < maxX; i++)
			{
				for (int j = minY; j < maxY; j++)
				{
					if (Main.tile[i, j] == null || !Main.tileCut[Main.tile[i, j].TileType] || (ignoreList != null && ignoreList.Contains(Main.tile[i, j].TileType)) || !WorldGen.CanCutTile(i, j, Terraria.Enums.TileCuttingContext.AttackMelee))
					{
						continue;
					}
					if (sItem.type == 1786)
					{
						int type = Main.tile[i, j].TileType;
						WorldGen.KillTile(i, j);
						if (!Main.tile[i, j].HasTile)
						{
							int num = 0;
							if (type == 3 || type == 24 || type == 61 || type == 110 || type == 201)
							{
								num = Main.rand.Next(1, 3);
							}
							if (type == 73 || type == 74 || type == 113)
							{
								num = Main.rand.Next(2, 5);
							}
							if (num > 0)
							{
								int number = Item.NewItem(new EntitySource_ItemUse(Player, sItem), i * 16, j * 16, 16, 16, 1727, num);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(21, -1, -1, null, number, 1f);
								}
							}
						}
						if (Main.netMode == 1)
						{
							NetMessage.SendData(17, -1, -1, null, 0, i, j);
						}
					}
					else
					{
						WorldGen.KillTile(i, j);
						if (Main.netMode == 1)
						{
							NetMessage.SendData(17, -1, -1, null, 0, i, j);
						}
					}
				}
			}
		}*/
	}
}

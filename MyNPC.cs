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
using Terraria.Audio;
using Emperia;
using Emperia.Projectiles;
using Emperia.Projectiles.Ethereal;
using Emperia.Items;
using Emperia.Items.Weapons;
using Emperia.Items.Weapons.Skeletron;
using Emperia.Items.Accessories;
using Terraria.GameContent.ItemDropRules;
using Emperia.Buffs;
using static Emperia.Projectiles.HarpoonBladeProj;




namespace Emperia
{
	public class MyNPC : GlobalNPC
	{
		public bool vermillionVenom = false;
		public bool moreDamage = false;
		public bool cuttingLeaves = false;
		public bool indigoInfirmary = false;
		public bool burningNight = false;
		public bool fatesDemise = false;
		public int nightFlame = 0;
		public int nightFlameLength = 0;
		public bool sporeStorm = false;
		public bool scoriaExplosion = false;
		public bool electrified = false;
		public bool moreCoins = false;
		public bool graniteMinionLatched = false;
		public bool crushFreeze = false;
		public bool cryogenized = false;
		public int graniteMinID = -1;
		public int spineCount = 0;
		public int chillStacks = 0;
		public int etherealTimer = 0;
		public int strikeCount = 0;
		public int desertSpikeTime = 0;
		public int impaledDirection = 0;
		public float desertSpikeHeight = 0;
		public bool impaledGravity = true;
		public float reflectVelocity = 0f;
		public bool reflectsProjectilesCustom = false;
		public int maceSlam = 0;
		public int maceSlamDamage = 0;
		int infirmaryTimer = 30;
		int poisonTimer = 0;

		public int hitboxMinusX = 0;
		public int hitboxMinusY = 0;

		public int hurtboxPlusX = 0;
		public int hurtboxPlusY = 0;

		public List<int> etherealDamages = new List<int>();
		public List<int> etherealCounts = new List<int>();
		public Projectile etherealSource = null;

		public override void ResetEffects(NPC npc)
		{
			cuttingLeaves = false;
			electrified = false;
			//spineCount = 0;
			vermillionVenom = false;
			indigoInfirmary = false;
			crushFreeze = false;
			burningNight = false;
			fatesDemise = false;
			sporeStorm = false;
			moreDamage = false;
			moreCoins = false;
			strikeCount = 0;
			cryogenized = false;
			//graniteMinionLatched = false;
		}
		public override bool InstancePerEntity { get { return true; } }

        public override void ModifyShop(NPCShop shop)
        {
			if (shop.NpcType == NPCID.Merchant)
			{
				shop.Add(new Item(ModContent.ItemType<Lasagna>()));
			}
		}

        public override void SetupTravelShop(int[] shop, ref int nextSlot)
		{
			if (!NPC.downedBoss1 || !Main.hardMode && Main.rand.Next(5) == 0 || Main.rand.Next(5) == 0) //eoc defeat bool temp until i add a bool for the item having appeared instead, the two random chances stack in phm for 2/5th odds
			{
				shop[nextSlot] = (ModContent.ItemType<PlatformLayer>());
				nextSlot++;

			}
		}
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			//etherealDamages.Add(2);
			//Main.NewText(etherealDamages[0]);
			if (burningNight)  //this tells the game to use the public bool customdebuff from NPCsINFO.cs
			{
				npc.lifeRegen = -15;

				damage = 2;

			}
			if (fatesDemise)  //this tells the game to use the public bool customdebuff from NPCsINFO.cs
			{
				npc.lifeRegen = -25;

				damage = 4;

			}
			if (sporeStorm)
			{
				npc.lifeRegen = -5;
				damage = 4;
			}
			if (npc.HasBuff(ModContent.BuffType<NocturnalFlame>()))
			{
				nightFlameLength++;
				nightFlame = (1 + (int)Math.Floor(nightFlameLength / 600f)) * 2;
				if (nightFlame > 10) nightFlame = 10;
				npc.lifeRegen -= nightFlame * 2;
				damage = nightFlame;
			}
			else
			{
				nightFlameLength = 0;
				nightFlame = 0;
			}

		}
		/*public override void SetDefaults(NPC npc)
        {

        }*/
		public override bool PreAI(NPC npc)
		{
			if (cryogenized == true)
			{
				return false;
			}
			else return true;
		}
		public override void AI(NPC npc)
		{
			if (etherealDamages.Count > 0)
				etherealTimer++;
			if (etherealTimer >= 20)
			{
				if (etherealDamages.Count > 0)
					Projectile.NewProjectile(Projectile.InheritSource(etherealSource), npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<EtherealFlux>(), 0, 0, Main.myPlayer, 0, 0);
				etherealTimer = 0;
				List<int> newEtherealCounts = new List<int>();
				List<int> newEtherealDamages = new List<int>();
				for (int i = 0; i < etherealDamages.Count; i++)
				{
					if (etherealCounts[i] > 0)
					{
						npc.SimpleStrikeNPC(etherealDamages[i], 0);
						etherealCounts[i]--;
					}
					if (etherealCounts[i] > 0)
					{
						newEtherealCounts.Add(etherealCounts[i]);
						newEtherealDamages.Add(etherealDamages[i] / 2);
					}
				}
				etherealCounts = newEtherealCounts;
				etherealDamages = newEtherealDamages;
			}
			if (!crushFreeze)
			{
				chillStacks = 0;
			}
			poisonTimer++;
			if (spineCount > 0 && poisonTimer % 25 == 0)
			{
				//Main.NewText(spineCount);
				npc.life -= spineCount;
				//NPC.StrikeNPCNoInteraction(spineCount, 0, 0, false, false, false);
				Color color2 = CombatText.DamagedHostile;
				CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), color2, spineCount, false, false);

				npc.HitEffect(0, 10);
			}
			infirmaryTimer--;
			if (infirmaryTimer <= 0)
			{
				infirmaryTimer = 30;
				if (indigoInfirmary)
				{
					Player player = Main.player[npc.target];

					int fourthOfMaxHP = (int)(player.statLife / (float)player.statLifeMax2 * 4);

					int damage = fourthOfMaxHP + 1;
					npc.SimpleStrikeNPC(damage + npc.defense / 2, 0);
				}
				if (crushFreeze)
				{
					int damage = chillStacks;
					npc.SimpleStrikeNPC(damage, 0);
				}
			}
			if (sporeStorm && IsNormalEnemy(npc))
			{
				Player player = Main.player[npc.target];
				MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
				if (npc.boss)
					modPlayer.sporeBuffCount += 2;
				else
					modPlayer.sporeBuffCount += 1;
			}
			spineCount = 0;

			if (desertSpikeTime < 0)
			{
				desertSpikeTime++;
				npc.velocity.X = 0;
			}
			if (desertSpikeTime > 0)
			{
				desertSpikeTime--;
				npc.direction = impaledDirection;
				npc.velocity.X = 0;

				if (npc.Bottom.Y > desertSpikeHeight)
				{
					npc.velocity.Y = -8;
					if (npc.noGravity == false) //prevent enemies with gravity from falling
					{
						npc.noGravity = true;
						impaledGravity = false;
					}
				}
				else
				{
					npc.velocity.Y = 0.0001f; //so game thinks they're airbone
				}

				if (desertSpikeTime == 1) //deactivate gravity effects
				{
					npc.noGravity = impaledGravity;
					impaledGravity = true;
				}
			}
			maceSlam--;
			if (npc.collideY == true && maceSlam >= 0)
			{
				maceSlam = 0;
				PlaySound(SoundID.Item14, npc.Center);
				for (int i = 0; i < Main.npc.Length; i++)
				{
					if (npc.Distance(Main.npc[i].Center) < 1 && !Main.npc[i].townNPC)
						Main.npc[i].SimpleStrikeNPC(maceSlamDamage, 0);
				}
				for (int i = 0; i < 15; ++i)
				{
					int index2 = Dust.NewDust(npc.Center, npc.width, npc.height, ModContent.DustType<Dusts.CarapaceDust>(), 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 2f;
				}
			}
			/*if (NPC.type == NPCID.KingSlime)
			{
				NPC.width = (int)(NPC.height * 1.75f);
			}*/
		}

		/*public override void PostAI(NPC npc)
        {
			if (hitboxWidthDecrease != 0 || hitboxHeightDecrease != 0)
				smallerHitbox = new Rectangle(npc.Hitbox.X + hitboxWidthDecrease / 2, npc.Hitbox.Y + hitboxHeightDecrease / 2,
				npc.Hitbox.Width - hitboxWidthDecrease, npc.Hitbox.Height - hitboxHeightDecrease);
		}*/
		/*public override void DrawEffects(NPC npc, ref Color drawColor)
        {
			Emperia.DrawPixelRect(npc.Hitbox, Color.Blue * 0.2f);
			if (smallerHitbox != Rectangle.Empty) Emperia.DrawPixelRect(smallerHitbox, Color.Red * 0.3f);
		}*/
		public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
		{
			if (hitboxMinusX != 0 || hitboxMinusY != 0)
			{
				Rectangle smallerHitbox = new Rectangle(npc.Hitbox.X + hitboxMinusX / 2, npc.Hitbox.Y + hitboxMinusY / 2,
					npc.Hitbox.Width - hitboxMinusX, npc.Hitbox.Height - hitboxMinusY);

				//Emperia.DrawPixelRect(npc.Hitbox, Color.Blue * 0.2f);
				//Emperia.DrawPixelRect(smallerHitbox, Color.Red * 0.3f);
				if (target.Hitbox.Intersects(npc.Hitbox) && !target.Hitbox.Intersects(smallerHitbox)) Main.NewText(Emperia.AbsoluteSum(npc.velocity)); //Main.NewText("safe range!");
				if (!target.Hitbox.Intersects(smallerHitbox) && Emperia.AbsoluteSum(npc.velocity) < 6) return false;
			}
			return base.CanHitPlayer(npc, target, ref cooldownSlot);
		}

		public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
		{
			if (hurtboxPlusX != 0 || hurtboxPlusY != 0)
			{
				Rectangle largerHurtbox = new Rectangle(npc.Hitbox.X - hurtboxPlusX / 2, npc.Hitbox.Y - hurtboxPlusY / 2,
					npc.Hitbox.Width + hurtboxPlusX, npc.Hitbox.Height + hurtboxPlusY);

				Rectangle itemHitbox = player.GetModPlayer<MyPlayer>().currentItemHitbox;

				//Emperia.DrawPixelRect(npc.Hitbox, Color.Blue * 0.2f);
				//Emperia.DrawPixelRect(largerHurtbox, Color.Green * 0.3f);
				//Emperia.DrawPixelRect(itemHitbox, Color.Purple * 0.3f);
				if (!itemHitbox.Intersects(npc.Hitbox) && itemHitbox.Intersects(largerHurtbox)) Main.NewText("abnormal hit"); //seems to only run once; maybe because of return null?
				if (!itemHitbox.Intersects(npc.Hitbox) && itemHitbox.Intersects(largerHurtbox)) return true;
			}
			return null;
		}
		public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {

			if (hurtboxPlusX != 0 || hurtboxPlusY != 0)
			{
				Rectangle largerHurtbox = new Rectangle(npc.Hitbox.X - hurtboxPlusX / 2, npc.Hitbox.Y - hurtboxPlusY / 2,
					npc.Hitbox.Width + hurtboxPlusX, npc.Hitbox.Height + hurtboxPlusY);

				Emperia.DrawPixelRect(npc.Hitbox, Color.Blue * 0.2f);
				Emperia.DrawPixelRect(largerHurtbox, Color.Green * 0.3f);
				Emperia.DrawPixelRect(projectile.Hitbox, Color.Purple * 0.3f);
				if (!projectile.Hitbox.Intersects(npc.Hitbox) && projectile.Hitbox.Intersects(largerHurtbox)) Main.NewText("abnormal hit"); //seems to only run once; maybe because of return null?
				if (!projectile.Hitbox.Intersects(npc.Hitbox) && projectile.Hitbox.Intersects(largerHurtbox)) return true;
			}
			return null;
		}

		private bool CheckLargerHurtbox(NPC npc, Player player, Rectangle attackHitbox)
		{
			Rectangle largerHurtbox = new Rectangle(npc.Hitbox.X - hurtboxPlusX / 2, npc.Hitbox.Y - hurtboxPlusY / 2,
			npc.Hitbox.Width + hurtboxPlusX, npc.Hitbox.Height + hurtboxPlusY);


			//Emperia.DrawPixelRect(npc.Hitbox, Color.Blue * 0.2f);
			//Emperia.DrawPixelRect(largerHurtbox, Color.Green * 0.3f);
			//Emperia.DrawPixelRect(itemHitbox, Color.Purple * 0.3f);
			if (!attackHitbox.Intersects(npc.Hitbox) && attackHitbox.Intersects(largerHurtbox)) Main.NewText("abnormal hit"); //seems to only run once; maybe because of return null?
			if (!attackHitbox.Intersects(npc.Hitbox) && attackHitbox.Intersects(largerHurtbox)) return true;
			else return false;
		}

		public override void SetDefaults(NPC npc)
        {
			if (npc.type == NPCID.EyeofCthulhu) { hitboxMinusX = 28; hitboxMinusY = 28; } //unfortunately a lot of his attacks are designed to only barely graze you
			if (npc.type == NPCID.KingSlime) { hurtboxPlusX = 320 ; }//{ hurtboxPlusX = 32; }
		}
		public override void OnKill(NPC npc)
        {
			if (moreCoins)
			{
				npc.value += npc.value += Item.buyPrice(0, 0, 10, 0);
			}
			if (scoriaExplosion)
			{
				int expDamage = npc.lifeMax / 5;
				if (expDamage > 50)
				{
					expDamage = 50;
				}

				for (int num621 = 0; num621 < 20; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.LavaMoss, 0f, 0f, 100, default(Color));
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 35; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.LavaMoss, 0f, 0f, 100, default(Color));
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.LavaMoss, 0f, 0f, 100, default(Color));
					Main.dust[num624].velocity *= 2f;
				}
				PlaySound(SoundID.Item14, npc.position);
				for (int i = 0; i < Main.npc.Length; i++)
				{
					if (npc.Distance(Main.npc[i].Center) < 64 && !Main.npc[i].townNPC)
						Main.npc[i].SimpleStrikeNPC(expDamage, 0);
				}
			}
			if (fatesDemise)
			{
				int damage1 = 0;
				if (npc.lifeMax > 1500)
				{
					damage1 = 300;
				}
				else
				{
					damage1 = npc.lifeMax / 5;
				}
				for (int i = 0; i < 6; i++)
				{
					Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
					Projectile.NewProjectile(npc.GetSource_Buff(npc.FindBuffIndex(ModContent.BuffType<Buffs.FatesDemise>())), npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FatesFlames>(), damage1, 1, Main.myPlayer, 0, 0);
				}
				PlaySound(SoundID.NPCDeath52, npc.Center);
				//this code also exists in FatesFlames and MyPlayer, be sure to make all changes consistent
			}
			//if (NPC.type == 4)
			//{
			//	EmperialWorld.downedEye = true;
			//}
			//if (NPC.type == 3) { Main.NewText(EmperialWorld.downedEye.ToString());}
		}

		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)  
        {
			if (npc.type == NPCID.SkeletronHead)
			{
				IItemDropRule normalRule = new LeadingConditionRule(new Conditions.NotExpert());
				normalRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<Skelebow>(), ModContent.ItemType<NecromanticFlame>(), ModContent.ItemType<BoneWhip>()));
				npcLoot.Add(normalRule);
			}
			if (npc.type == NPCID.Wraith)
            {
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<DeathTalisman>(), 100, 80));
            }
            if (npc.type == NPCID.TheGroom || npc.type == NPCID.TheBride || npc.type == NPCID.BloodZombie || npc.type == NPCID.Drippler )
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ForbiddenOath>(), 100, 65));
            }
			if (npc.type == NPCID.ZombieMerman || npc.type == NPCID.EyeballFlyingFish)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ForbiddenOath>(), 15));
			}
			if (npc.type == NPCID.GoblinShark || npc.type == NPCID.BloodEelHead)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Accessories.Gauntlets.BloodGauntlet>(), 8));
			}
			if (npc.type == NPCID.BloodNautilus)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Accessories.Gauntlets.BloodGauntlet>(), 2));
			}
			if (npc.type == NPCID.GoblinShark || npc.type == NPCID.BloodEelHead)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.BloodCandle>(), 8));
			}
			if (npc.type == NPCID.BloodNautilus)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.BloodCandle>(), 2));
			}
			if (npc.type == NPCID.Piranha)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<TetheredPiranha>(), 50, 40));
			}
			if(npc.type == NPCID.Gastropod)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Escargun>(), 75, 60));
			}
			if (npc.type == NPCID.EyeofCthulhu)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new EOCDropCondition(), ModContent.ItemType<SetStone>(), 1)); //If the world is in master mode, drop ExampleSouls 20% of the time from every npc.
				//EmperialWorld.downedEye = true;
				//Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<SetStone>());
				//THIS DOESNT WORK!!! ITS A LIE FR FR. also downedeye does NOT save so it gets reset on reload
			}
		}
		public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
		{
			if (vermillionVenom)
			{
				modifiers.SourceDamage *= 0.9f;
			}
			if (crushFreeze)
				modifiers.SourceDamage *= (1 - .05f * chillStacks);
		}
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
			if (desertSpikeTime > 0 && desertSpikeTime < 100)
			{
				desertSpikeTime = 0;
				npc.noGravity = impaledGravity;
				impaledGravity = true;
			}
        }
		public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
		{
			if (desertSpikeTime > 0 && desertSpikeTime < 100)
			{
				desertSpikeTime = 0;
				npc.noGravity = impaledGravity;
				impaledGravity = true;
			}
			if (item.type == ModContent.ItemType<Items.Weapons.Mushor.Fungallows>()) //wip
			{
				//if (npc.life <= damage - npc.defense * 0.5)
				{
					//Main.NewText("wow", 255, 240, 20, false);
					//damage = NPC.life + NPC.defense * 0.5 - 1;
					//NPC = target;
					//NPC.GetGlobalNPC<MyNPC>().variableName = true;	
				}
			}
		}
		public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
		{
			if (moreDamage)
			{
				modifiers.SourceDamage *= 1.1f;
			}
		}
        public override void OnHitByItem(NPC npc, Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
			bool Unchained = false;
			for (int l = 0; l < 1000; l++)
			{
				if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == ModContent.ProjectileType<HarpoonBladeProj>() && Main.projectile[l].GetGlobalProjectile<GProj>().latchedNPC == npc)
				{
					//Main.projectile[l].ModProjectile.OnTileCollide(Vector2.Zero);
					(Main.projectile[l].ModProjectile as HarpoonBladeProj).Unchain(Main.projectile[l]);
					npc.SimpleStrikeNPC(Main.projectile[l].damage, 0); //damage multiplied by 0.75f to nerf? make damage occur in projectile to count towards dps
					Unchained = true;
				}
			}
			if (Unchained) { PlaySound(new SoundStyle("Emperia/Sounds/Custom/ChainPull"), player.Center); }
		}
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
			if (player.HasBuff(ModContent.BuffType<BloodCandleBuff>()))
			{
				if (player.ZoneWaterCandle || player.inventory[player.selectedItem].type == ItemID.WaterCandle) //cancels out water candle effect, referenced from vanilla code
				{
					if (!player.ZonePeaceCandle && player.inventory[player.selectedItem].type != ItemID.PeaceCandle)
					{
						spawnRate = (int)(spawnRate / 0.75);
						maxSpawns = (int)Math.Ceiling(maxSpawns / 1.5f);
						if (player.ZoneWaterCandle && (player.position.Y / 16f) < Main.worldSurface * 0.34999999403953552)
						{
							spawnRate = (int)(spawnRate / 0.5);
						}
					}
				} 
				spawnRate = (int)(spawnRate * 0.55); // effective boost is the multiplicative inverse, so 1 / 0.55 = ~1.8
				maxSpawns = (int)(maxSpawns * 1.75f);
			}
			//Main.NewText(spawnRate);
			//Main.NewText(maxSpawns);
		}
        public bool IsNormalEnemy(NPC npc, bool allowStatueSpawned = true)
        {
			if (npc.SpawnedFromStatue && !allowStatueSpawned) return false;
			if (npc.type == NPCID.TargetDummy || npc.lifeMax <= 5) return false;
			return true;
		} //canbechasedby code:	if (base.active && this.chaseable && this.lifeMax > 5 && (!this.dontTakeDamage || ignoreDontTakeDamage) && !this.friendly) return !this.immortal;

		/*public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
		{
			for (int i = 0; i < pool.Count; i++)
			{
				var npc = pool.ElementAt(i);
				if (NPCLoader.GetNPC(npc.Key) is not null && NPCLoader.GetNPC(npc.Key).Mod == Emperia.instance)
				{
					pool[i] = 10000f;
					Main.NewText(NPCLoader.GetNPC(npc.Key).Name);
					Main.NewText(NPCLoader.GetNPC(npc.Key).SpawnChance(spawnInfo));
				}
			}
		}*/
    }
}

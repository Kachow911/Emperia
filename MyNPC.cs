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
    public class MyNPC: GlobalNPC
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
		public int maceSlam = 0;
		public int maceSlamDamage = 0;
		int InfirmaryTimer = 30;
        int poisonTimer = 0;
		
		public List<int> etherealDamages = new List<int>();
		public List<int> etherealCounts = new List<int>();
		public Projectile etherealSource = null;

public override void ResetEffects(NPC NPC)
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
		public override bool InstancePerEntity {get{return true;}}
		
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			if (type == NPCID.Merchant)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Lasagna>());
				nextSlot++;

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
        public override void UpdateLifeRegen(NPC NPC, ref int damage)
        {
			//etherealDamages.Add(2);
			//Main.NewText(etherealDamages[0]);
			if (burningNight)  //this tells the game to use the public bool customdebuff from NPCsINFO.cs
            {
                NPC.lifeRegen = -15;     

                damage = 2; 
      
            }
			if (fatesDemise)  //this tells the game to use the public bool customdebuff from NPCsINFO.cs
            {
                NPC.lifeRegen = -25;     

                damage = 4; 
      
            }
			if (sporeStorm)
			{
				NPC.lifeRegen = -5;
				damage = 4;
			}
			if (NPC.HasBuff(ModContent.BuffType<NocturnalFlame>()))
			{
				nightFlameLength++;
				nightFlame = (1 + (int)Math.Floor(nightFlameLength / 600f)) * 2;
				if (nightFlame > 10) nightFlame = 10;
				NPC.lifeRegen = -nightFlame * 2;
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
        public override bool PreAI(NPC NPC)
		{
			if (cryogenized == true)
			{
				return false;
			}
			else return true;
		}
		public override void AI(NPC NPC)
		{
            if (etherealDamages.Count > 0)
				etherealTimer++;
			if (etherealTimer >= 20)
			{
				if (etherealDamages.Count > 0)
						Projectile.NewProjectile(Projectile.InheritSource(etherealSource), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<EtherealFlux>(), 0, 0, Main.myPlayer, 0, 0);
				etherealTimer = 0;
				List<int> newEtherealCounts = new List<int>();
				List<int> newEtherealDamages = new List<int>();
				for (int i = 0; i < etherealDamages.Count; i++)
				{
					if (etherealCounts[i] > 0)
					{
						NPC.StrikeNPCNoInteraction(etherealDamages[i], 0, 0, false, false, false);
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
				NPC.life -= spineCount;
				//NPC.StrikeNPCNoInteraction(spineCount, 0, 0, false, false, false);
				Color color2 = CombatText.DamagedHostile;
				CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height), color2, spineCount, false, false);
				
				NPC.HitEffect(0, 10);
			}
			InfirmaryTimer--;
			if (InfirmaryTimer <= 0)
			{
				InfirmaryTimer = 30;
				if (indigoInfirmary)
				{
					int damage = 1;
					Player player = Main.player[NPC.target];
					if (player.statLife == player.statLifeMax2)
						damage = 5;
					if (player.statLife < player.statLifeMax2 && player.statLife >= (player.statLifeMax2 /4) * 3)
						damage = 4;
					if (player.statLife < (player.statLifeMax2 /4) * 3 && player.statLife >= (player.statLifeMax2 / 2))
						damage = 3;
					if (player.statLife < (player.statLifeMax2 / 2) && player.statLife >= (player.statLifeMax2 / 4))
						damage = 2;
					if (player.statLife < (player.statLifeMax2 / 4))
						damage = 1;
					NPC.StrikeNPCNoInteraction(damage, 0, 0, false, false, false);
				}
				if (crushFreeze)
                {
					int damage = chillStacks;
					NPC.StrikeNPCNoInteraction(damage, 0, 0, false, false, false);
				}
			}
			if (NPC.life <= 0)
			{
				
			}
			if (sporeStorm && IsNormalEnemy(NPC))
			{
				Player player = Main.player[NPC.target];
				MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
				if (NPC.boss)
					modPlayer.sporeBuffCount += 2;
				else
					modPlayer.sporeBuffCount += 1;
			}
            spineCount = 0;
			
			if (desertSpikeTime < 0)
			{
				desertSpikeTime++;
				NPC.velocity.X = 0;				
			}
			if (desertSpikeTime > 0)
			{
				desertSpikeTime--;
				NPC.direction = impaledDirection;
				NPC.velocity.X = 0;

				if (NPC.Bottom.Y > desertSpikeHeight)
				{
					NPC.velocity.Y = -8;
					if (NPC.noGravity == false) //prevent enemies with gravity from falling
					{
						NPC.noGravity = true;
						impaledGravity = false;
					}
				}
				else
				{
					NPC.velocity.Y = 0.0001f; //so game thinks they're airbone
				}

				if (desertSpikeTime == 1) //deactivate gravity effects
				{
					NPC.noGravity = impaledGravity;
					impaledGravity = true;
				}
			}
			maceSlam--;
			if (NPC.collideY == true && maceSlam >= 0)
			{
				maceSlam = 0;
				PlaySound(SoundID.Item14, NPC.Center);
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (NPC.Distance(Main.npc[i].Center) < 1 && !Main.npc[i].townNPC)
                        Main.npc[i].StrikeNPC(maceSlamDamage, 0f, 0, false, false, false);
                }
				for (int i = 0; i < 15; ++i)
				{
					int index2 = Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.CarapaceDust>(), 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 2f;
				}
			}
			/*if (NPC.type == NPCID.KingSlime)
			{
				NPC.width = (int)(NPC.height * 1.75f);
			}*/
		}
        public override void OnKill(NPC NPC)
        {
			if (moreCoins)
			{
				NPC.value += NPC.value += Item.buyPrice(0, 0, 10, 0);
			}
			if (scoriaExplosion)
			{
				int expDamage = NPC.lifeMax / 5;
				if (expDamage > 50)
				{
					expDamage = 50;
				}

				for (int num621 = 0; num621 < 20; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 258, 0f, 0f, 100, default(Color));
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 35; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 258, 0f, 0f, 100, default(Color));
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 258, 0f, 0f, 100, default(Color));
					Main.dust[num624].velocity *= 2f;
				}
				PlaySound(SoundID.Item14, NPC.position);
				for (int i = 0; i < Main.npc.Length; i++)
				{
					if (NPC.Distance(Main.npc[i].Center) < 64 && !Main.npc[i].townNPC)
						Main.npc[i].StrikeNPC(expDamage, 0f, 0, false, false, false);
				}
			}
			if (fatesDemise)
			{
				int damage1 = 0;
				if (NPC.lifeMax > 1500)
				{
					damage1 = 300;
				}
				else
				{
					damage1 = NPC.lifeMax / 5;
				}
				for (int i = 0; i < 6; i++)
				{
					Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
					Projectile.NewProjectile(NPC.GetSource_Buff(NPC.FindBuffIndex(ModContent.BuffType<Buffs.FatesDemise>())), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FatesFlames>(), damage1, 1, Main.myPlayer, 0, 0);
				}
				PlaySound(SoundID.NPCDeath52, NPC.Center);
				//this code also exists in FatesFlames and MyPlayer, be sure to make all changes consistent
			}
			//if (NPC.type == 4)
			//{
			//	EmperialWorld.downedEye = true;
			//}
			//if (NPC.type == 3) { Main.NewText(EmperialWorld.downedEye.ToString());}
		}

        public override void ModifyNPCLoot(NPC NPC, NPCLoot npcLoot)  
        {
			if (NPC.type == NPCID.SkeletronHead)
			{
				IItemDropRule normalRule = new LeadingConditionRule(new Conditions.NotExpert());
				normalRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<Skelebow>(), ModContent.ItemType<NecromanticFlame>(), ModContent.ItemType<BoneWhip>()));
				npcLoot.Add(normalRule);
			}
			if (NPC.type == 82)
            {
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<DeathTalisman>(), 100, 80));
            }
            if (NPC.type == 53 || NPC.type == 536 || NPC.type == 489 || NPC.type == 490 )
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ForbiddenOath>(), 100, 65));
            }
			if (NPC.type == 586 || NPC.type == 587)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ForbiddenOath>(), 15));
			}
			if (NPC.type == 620 || NPC.type == 621)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Accessories.Gauntlets.BloodGauntlet>(), 8));
			}
			if (NPC.type == 618)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Accessories.Gauntlets.BloodGauntlet>(), 2));
			}
			if (NPC.type == 620 || NPC.type == 621)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.BloodCandle>(), 8));
			}
			if (NPC.type == 618)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.BloodCandle>(), 2));
			}
			if (NPC.type == 58)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<TetheredPiranha>(), 50, 40));
			}
			if(NPC.type == 122)
			{
				npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<Escargun>(), 75, 60));
			}
			if (NPC.type == 4)
			{
				npcLoot.Add(ItemDropRule.ByCondition(new EOCDropCondition(), ModContent.ItemType<SetStone>(), 1)); //If the world is in master mode, drop ExampleSouls 20% of the time from every npc.
				//EmperialWorld.downedEye = true;
				//Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<SetStone>());
				//THIS DOESNT WORK!!! ITS A LIE FR FR. also downedeye does NOT save so it gets reset on reload
			}
		}
		public override void OnHitPlayer(NPC NPC, Player target, int damage, bool crit)
		{
		}

		public override void ModifyHitPlayer(NPC NPC, Player target, ref int damage, ref bool crit)
		{
			if (vermillionVenom)
			{
				damage = (int)(damage * 0.9f);
			}
			if (crushFreeze)
				damage = (int)(damage * (1 - .05 * chillStacks));
		}
        public override void ModifyHitByProjectile(NPC NPC, Projectile Projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (moreDamage)
            {
                damage = (int) (1.1 * damage);
            }

			if (desertSpikeTime > 0 && desertSpikeTime < 100)
			{
				desertSpikeTime = 0;
				NPC.noGravity = impaledGravity;
				impaledGravity = true;
			}
        }
        public override void ModifyHitByItem(NPC NPC, Player player, Item Item, ref int damage, ref float knockback, ref bool crit)
        {
            if (moreDamage)
            {
                damage = (int)(1.1 * damage);
            }

			if (desertSpikeTime > 0 && desertSpikeTime < 100)
			{
				desertSpikeTime = 0;
				NPC.noGravity = impaledGravity;
				impaledGravity = true;
			}
			if (Item.type == ModContent.ItemType<Items.Weapons.Mushor.Fungallows>()) //wip
			{
				if (NPC.life <= damage - NPC.defense * 0.5)
				{
					//Main.NewText("wow", 255, 240, 20, false);
					//damage = NPC.life + NPC.defense * 0.5 - 1;
					//NPC = target;
        	    	//NPC.GetGlobalNPC<MyNPC>().variableName = true;	
				}
			}
		}
        public override void OnHitByItem(NPC NPC, Player player, Item item, int damage, float knockback, bool crit)
        {
			bool Unchained = false;
			for (int l = 0; l < 1000; l++)
			{
				if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == ModContent.ProjectileType<HarpoonBladeProj>() && Main.projectile[l].GetGlobalProjectile<GProj>().latchedNPC == NPC)
				{
					//Main.projectile[l].ModProjectile.OnTileCollide(Vector2.Zero);
					(Main.projectile[l].ModProjectile as HarpoonBladeProj).Unchain(Main.projectile[l]);
					NPC.StrikeNPC(Main.projectile[l].damage, 0f, 0); //damage multiplied by 0.75f to nerf? make damage occur in projectile to count towards dps
					Unchained = true;
				}
			}
			if (Unchained) { PlaySound(new SoundStyle("Emperia/Sounds/Custom/ChainPull"), player.Center); }
		}
		public bool IsNormalEnemy(NPC NPC, bool allowStatueSpawned = true)
        {
			if (NPC.SpawnedFromStatue && !allowStatueSpawned) return false;
			if (NPC.type == NPCID.TargetDummy || NPC.lifeMax <= 5) return false;
			return true;
        }

	}
}

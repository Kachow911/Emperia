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
using Emperia;
using Emperia.Projectiles;

namespace Emperia
{
    public class MyPlayer : ModPlayer
    {
		public bool ZoneVolcano = false;
		public bool cursedDash = false;
		public bool lunarDash = false;
		public bool rougeRage = false;
		public bool vermillionValor = false;
		public bool deathTalisman = false;
		public bool forbiddenOath = false;
		public bool vitalityCrystal = false;
		public bool defenseInsignia = false;
		public bool isMellowProjectile = false;
		public bool slightKnockback = false;
        public bool doubleKnockback = false;
        public bool sharkMinion = false;
		public bool EmberTyrant = false;
        public bool ancientPelt = false;
		public bool eruptionBottle = false;
		public bool sporeFriend = false;
		private bool dashActive = false;
		public bool goblinSet = false;
        public bool aquaticSet = false;
        public bool yetiMount = false;
        public bool frostGauntlet = false;
        public bool meteorGauntlet = false;
		public bool ferocityGauntlet = false;
		public bool thermalGauntlet = false;
		public bool floralGauntlet = false;
		public bool terraGauntlet = false;
        public bool renewedLife = false;
		public bool breakingPoint = false;
		public bool forestSetMelee = false;
		public bool forestSetRanged = false;
		public bool forestSetMage = false;
        public bool forestSetThrown = false;
        public bool forestSetSummon = false;
		public bool graniteSet = false;
        public int dayVergeProjTime = 0;
		bool canJump = false;
        bool placedPlant = false;
		bool changedVelocity;
		bool clickedLeft = false;
		bool clickedRight = false;
		List<int> hitEnemies = new List<int>();
		private int terraTime = 0;
		public int yetiCooldown = 30;
		public int graniteTime = 0;
		private int leftPresses = 0;
		private int rightPresses = 0;
		private int dashDelay = 0;
		private int pressTime = 0;
		public int sporeCount = 0;
		public int sporeBuffCount = 0;
		public int OathCooldown = 720;
		private int peltCounter = 120;
		private int peltRadius = 256;
		private int forestSetMeleeCooldown = 60;
		int SporeHealCooldown = 60;
        int incDefTime = 0;
		int ferocityTime = 0;
		private int primalRageTime = 0;
		public int vileTimer = 0;
		private bool waxwingActive = false;
		public int eschargo = -5;
        public override void ResetEffects()
        {

			EmberTyrant = false;
			breakingPoint = false;
			terraGauntlet = false;
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
			cursedDash = false;
			yetiMount = false;
			slightKnockback = false;
			sporeFriend = false;
			rougeRage = false;
			vermillionValor = false;
			deathTalisman = false;
			forbiddenOath = false;
			vitalityCrystal = false;
			defenseInsignia = false;
			goblinSet = false;
			ancientPelt = false;
            aquaticSet = false;
			forestSetMelee = false;
			forestSetRanged = false;
			forestSetMage = false;
            forestSetThrown = false;
            forestSetSummon = false;
			graniteSet = false;
			
            sporeBuffCount = 0;
        }
		public override void UpdateBiomes()
		{
			ZoneVolcano = EmperialWorld.VolcanoTiles > 250;
		}
        public override void UpdateBiomeVisuals()
		{
			player.ManageSpecialBiomeVisuals("Emperia:Volcano", ZoneVolcano);
		}
        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
		{
			if (junk)
				{
					return;
				}
			if (Main.hardMode && worldLayer == 0 && liquidType == 0)
			{
				int icarusChance = Convert.ToInt32(12 - power / 30); //(15 - power / 20); more basic but less fair with low power, 10 / 50 to make more common
				//string chanceText = icarusChance.ToString();
				//Main.NewText(chanceText, 255, 240, 20, false);
				if (Main.rand.NextBool(icarusChance))
				{
					caughtType = mod.ItemType("Icarusfish");
				}
			}
		}
        public override void PostUpdate()
        {
			if (graniteSet && graniteTime <= 1800)
				graniteTime++;
			if (forestSetMage && primalRageTime >= 0)
			{
				primalRageTime--;
                if (primalRageTime % 5 == 0)
                    player.statMana += 2;
                
			}
			if (forestSetMelee)
			{
				bool doWave = false;
				forestSetMeleeCooldown--;
				for (int i = 0; i < 200; i++)
				{
					if (player.Distance(Main.npc[i].Center) < 64 && forestSetMeleeCooldown <= 0 && !Main.npc[i].boss && !Main.npc[i].townNPC)
					{
						forestSetMeleeCooldown = 120;
						Vector2 direction = player.Center - Main.npc[i].Center;
						direction.Normalize();
						Main.npc[i].velocity = (-direction) * 8f;
						doWave = true;
						Main.npc[i].StrikeNPC(10, 0f, 0, false, false, false);
					}
				}
				if (doWave)
				{
					for (int j = 0; j < 90; j++)
					{
						Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(4 * j));
						Color rgb = new Color(50,205,50);
						int index3 = Dust.NewDust(player.Center, 8, 8, 63, (float)0, (float)0, 0, rgb, 1.1f);
						Main.dust[index3].velocity = perturbedSpeed;
					}
					doWave = false;
				}
			}
			if (incDefTime > 0)
            {
                player.statDefense += 5;
                incDefTime--;
            }
			if (eruptionBottle)
			{
				if (player.velocity.Y == 0 && player.releaseJump)
				{
					canJump = true;
				}
				if (player.controlJump && canJump && player.velocity.Y != 0)
				{
					for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
						int dust = Dust.NewDust(player.position, player.width, player.height, 6);
						int dust1 = Dust.NewDust(player.position, player.width, player.height,6);
						Main.dust[dust1].scale = 0.8f;
						Main.dust[dust1].velocity *= 1.5f;
					}
					player.velocity.Y = -17f;
					canJump = false;
				}
			}
			if (aquaticSet)
            {
                 for (int i = (int) player.position.X / 16 - 25; i < (int)player.position.X / 16 + 25; i++)
                 {
                      for (int j = (int)player.position.Y / 16 - 25; j < (int)player.position.Y / 16 + 25; j++)
                      {
                          if (!Main.tile[i, j - 1].active() && Main.tile[i, j].active() && Main.rand.NextBool(5000) && !(Main.tile[i, j].type == TileID.Trees))
                          {
                            int egg = Main.rand.Next(3);
                            int type;
                            if (egg == 0) type = mod.ProjectileType("plant1");
                            else if (egg == 1) type = mod.ProjectileType("plant2");
                            else type = mod.ProjectileType("plant3");
                            Projectile.NewProjectile(i * 16 , j * 16 - 14, 0, 0, type, 0, 1, Main.myPlayer, 0, 0);
                           }

                       }
                    }
                }
            
			dashDelay--;
			if (dashDelay >= 70)
			{
				player.velocity.X *= .95f;
				int dust = Dust.NewDust(player.position, player.width, player.height, 75, 0f, 0f, 91, new Color(2, 249, 2), 1.5f);
				Main.dust[dust].velocity = Vector2.Zero;
				Main.dust[dust].noGravity = true;
				//if (dashDelay % 3 == 0)
				{
					for (int i = 0; i < 200; i++)
					{
						if (player.Hitbox.Intersects(Main.npc[i].Hitbox) && !hitEnemies.Contains(i) && Main.npc[i].life > 2)
						{
							hitEnemies.Add(i);
							Main.npc[i].StrikeNPC(60, 0f, 0, false, false, false);
							Main.npc[i].AddBuff(BuffID.CursedInferno, 120);
							if (!changedVelocity)
							{
								changedVelocity = true;
								player.velocity.X = (-3  * player.velocity.X) / 4;
								player.velocity.Y -= 5;
							}
						}
					}
				}
			}				
			if (player.releaseLeft && clickedLeft)
			{
				pressTime = 15;
				leftPresses++;
			}
			if (player.releaseRight && clickedRight)
			{
				pressTime = 15;
				rightPresses++;
			}
			if (leftPresses >= 2 && dashDelay <= 0 && (cursedDash || lunarDash))
			{
				if (!lunarDash)
				{
					changedVelocity = false;
					hitEnemies = new List<int>();
					player.velocity.X = -19f;
					dashDelay = 100;
					for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
						int dust = Dust.NewDust(player.position, player.width, player.height, 75);
						int dust1 = Dust.NewDust(player.position, player.width, player.height, 75);
						Main.dust[dust1].scale = 0.8f;
						Main.dust[dust1].velocity *= 2f;
						Main.dust[dust1].noGravity = true;
					}
				}
				else
				{
					Color rgb = new Color(0, 0, 0);
					hitEnemies = new List<int>();
					player.velocity.X = -22f;
					dashDelay = 100;
					for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
						int ex = Main.rand.Next(4);
						if (ex == 0) rgb = new Color(254, 126, 229);
						else if (ex == 1) rgb = new Color(254, 105, 47);
						else if (ex == 2) rgb = new Color(0, 242, 170);
						else if (ex == 3) rgb = new Color(104, 214, 255);
						int index3 = Dust.NewDust(player.position, 8, 8, 76, 0, 0, 0, rgb, 1.1f);
						int index4 = Dust.NewDust(player.position, 8, 8, 76, (float)player.velocity.X, (float)player.velocity.Y, 0, rgb, 1.1f);
						Main.dust[index4].scale = 0.8f;
						Main.dust[index4].velocity *= 2f;
						Main.dust[index4].noGravity = true;
					}
				}
			}
			if (rightPresses >= 2 && dashDelay <= 0 && (cursedDash || lunarDash))
			{
				if (!lunarDash)
				{
					changedVelocity = false;
					hitEnemies = new List<int>();
					player.velocity.X = 19f;
					dashDelay = 100;
					for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
						int dust = Dust.NewDust(player.position, player.width, player.height, 75);
						int dust1 = Dust.NewDust(player.position, player.width, player.height, 75);
						Main.dust[dust1].scale = 0.8f;
						Main.dust[dust1].velocity *= 2f;
						Main.dust[dust1].noGravity = true;
					}
				}
				else
				{
					Color rgb = new Color(0, 0, 0);
					hitEnemies = new List<int>();
					player.velocity.X = 20f;
					dashDelay = 100;
					for (int i = 0; i < 50; ++i) //Create dust after teleport
					{
						int ex = Main.rand.Next(4);
						if (ex == 0) rgb = new Color(254, 126, 229);
						else if (ex == 1) rgb = new Color(254, 105, 47);
						else if (ex == 2) rgb = new Color(0, 242, 170);
						else if (ex == 3) rgb = new Color(104, 214, 255);
						int index3 = Dust.NewDust(player.position, 8, 8, 76, 0, 0, 0, rgb, 1.1f);
						int index4 = Dust.NewDust(player.position, 8, 8, 76, (float)player.velocity.X, (float)player.velocity.Y, 0, rgb, 1.1f);
						Main.dust[index4].scale = 0.8f;
						Main.dust[index4].velocity *= 2f;
						Main.dust[index4].noGravity = true;
					}
				}
		
			}
			pressTime--;
			if (pressTime <= 0)
			{
				leftPresses = 0;
				rightPresses = 0;
			}
			if (forbiddenOath && player.statLife <= (int) ((float) player.statLifeMax2 * .4f))
			{
				OathCooldown--;
			}
			if (OathCooldown <= 0)
			{
				player.HealEffect(20);
				player.statLife += 20;
				OathCooldown = 720;
			}
			if (ancientPelt)
			{
				peltCounter--;
				if (peltCounter <= 0)
				{
					peltCounter = 120;
					Color rgb = new Color(160, 243, 255);
					for (int i = 0; i < 360; i+= 6)
					{
						Vector2 Position = new Vector2(0, -256).RotatedBy(MathHelper.ToRadians(i));
						int dust = Dust.NewDust(player.Center + Position, player.width / 8, player.height / 8, 76, 0f, 0f, 0, rgb, 1.5f);
						Main.dust[dust].noGravity = true;
					}
					for (int i = 0; i < Main.npc.Length; i++)
                    {
			            if (player.Distance(Main.npc[i].Center) < peltRadius)
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
				for (int npcFinder = 0; npcFinder <200; ++npcFinder)
				{
				
					if (player.Distance(Main.npc[npcFinder].Center) < 256 && yetiCooldown <= 0)
					{
						yetiCooldown = 30;
						Vector2 direction = Main.npc[npcFinder].Center - player.Center - new Vector2(0, 16);
						direction.Normalize();
						Projectile.NewProjectile(player.Center.X, player.Center.Y + 16, direction.X * 7f, direction.Y * 7f, ProjectileID.SnowBallFriendly, 25, 1, Main.myPlayer, 0, 0);  
					}
			    }
			}
			clickedLeft = false;
			clickedRight = false;
			dashDelay--;
			if (player.controlLeft) clickedLeft = true;
			if (player.controlRight) clickedRight = true;
			if (vileTimer > 0) 
			{
				vileTimer--;
				if (vileTimer == 0) 
				{
			        player.statLife += 130;
           			player.HealEffect(130);
					Main.PlaySound(SoundID.Item4, player.Center);
				}
			}
			if (player.HasBuff(mod.BuffType("Waxwing"))) {
				if ((player.controlJump) && (player.wingTimeMax > 0) && (player.velocity.Y != 0) && (!player.mount.Active))
            	{
					string airTimer = player.wingTime.ToString();
					Main.NewText(airTimer, 255, 240, 20, false);
					waxwingActive = true;
            	}
			}
			if (eschargo >= 0)
			{
				eschargo++;
			}
			if (eschargo > 600)
			{
				eschargo = -5;
			}
        }
	
		public override void ProcessTriggers(TriggersSet triggersSet)
        {

		}
		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
			return true;
		}
		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore,ref PlayerDeathReason damageSource)		
        {
			return dashDelay <= 70;
		}
		public override void PreUpdate()
		{
			{
				{
					{
						if (terraTime > 0)
						{
							player.statDefense += 15;
							player.thrownDamage += 0.08f;
							player.meleeDamage += 0.08f;
							player.minionDamage += 0.08f;
							player.magicDamage += 0.08f;
							player.rangedDamage += 0.08f;
							player.moveSpeed += 0.25f;
						}
						if (ferocityTime > 0)
						{
							player.statDefense += 5;
							player.thrownDamage += 0.05f;
							player.meleeDamage += 0.05f;
							player.minionDamage += 0.05f;
							player.magicDamage += 0.05f;
							player.rangedDamage += 0.05f;
							player.moveSpeed += 0.20f;
						}
						SporeHealCooldown--;
						if (SporeHealCooldown <= 0)
						{
							if (sporeBuffCount > 0 && player.active && !player.dead)
							{
								if (sporeBuffCount > 6)
									sporeBuffCount = 6;
								player.statLife += sporeBuffCount;
								player.HealEffect(sporeBuffCount);
							}
							SporeHealCooldown = 120;
						}
					}
				}
			}
		}
		
       

        public override void UpdateBadLifeRegen()
        {
          
        }
		public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
		{
			graniteTime = 0;
			if (terraGauntlet)
				terraTime = 180;
			for (int i = 0; i < Main.projectile.Length; i++)
            {
				if (Main.projectile[i].type == mod.ProjectileType("Needle") || Main.projectile[i].type == mod.ProjectileType("HauntedRevolver"))
					Main.projectile[i].Kill();
			}
			if (thermalGauntlet)
            {
                Vector2 placePosition = player.Center + new Vector2(Main.rand.Next(-100, 100), -500);
                Vector2 direction = player.Center - placePosition;
                int p = Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * 12f, direction.Y * 12f, ProjectileID.Meteor1, 60, 1, Main.myPlayer, 0, 0);
                Main.projectile[p].friendly = true;
                Main.projectile[p].hostile = false;
                Main.projectile[p].scale = 0.7f;
            }
		}
		public override void ModifyHitNPC (Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			if (slightKnockback)
			{
				knockback *= 1.1f;
			}
            if (item.type == mod.ItemType("LifesFate") && renewedLife)
            {
                damage = (int) ((float) damage * 1.15f);
            }
            if (doubleKnockback)
            {
                knockback *= 2f;
            }
			if ((ferocityGauntlet || terraGauntlet) && Main.rand.Next(10) == 0)
				damage *= 2;
			if (crit && rougeRage)
			{
				damage = damage += (damage / 10);
			}
			if (crit && vermillionValor)
			{
				damage = damage += ((damage * 13) / 100);
			}
        }
		public override void OnHitNPC (Item item, NPC target, int damage, float knockback, bool crit)
		{
			if (target.life <= 0 && terraGauntlet)
            {
                for (int i = 0; i < Main.rand.Next(3, 6); i++)
			    {
                    Vector2 perturbedSpeed = new Vector2(0, 4).RotatedByRandom(MathHelper.ToRadians(360));
                    int p = Projectile.NewProjectile(target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("TerraBoltHost"), 75, 1, Main.myPlayer, 0, 0);
               }
            }
			if (floralGauntlet && Main.rand.Next(20) == 0)
			{
				int lifeToHeal = damage;
				if (lifeToHeal > 25)
					lifeToHeal = 25;
				player.statLife += lifeToHeal;
				player.HealEffect(lifeToHeal);
			}
			if (ferocityGauntlet)
			{
				ferocityTime = 180;
			}
			if (crit && target.life <= 0 && deathTalisman)
			{
				int damage1 = 0;
				if (target.lifeMax > 3000)
				{
					damage1 = 300;
				}
				else
				{
					damage1 = target.lifeMax / 10;
				}
				//for (int i = 0; i < 5; i++)
				//{
					Vector2 perturbedSpeed = new Vector2(0, 5).RotatedByRandom(MathHelper.ToRadians(360));
					Projectile.NewProjectile(target.Center.X, target.Center.Y, 0, 0, mod.ProjectileType("FateFlame"), damage1, 1, Main.myPlayer, 0, 0);
					
				//}
			}
			if (breakingPoint)
			{
				target.AddBuff(BuffID.OnFire,240);
			}
            if (meteorGauntlet)
            {
                target.AddBuff(BuffID.OnFire, 120);
                if (Main.rand.Next(3) == 0)
                {
                    Vector2 placePosition = target.Center + new Vector2(Main.rand.Next(-100, 100), -500);
                    Vector2 direction = target.Center - placePosition;
                    int p = Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * 12f, direction.Y * 12f, ProjectileID.Meteor1, 30, 1, Main.myPlayer, 0, 0);
                    Main.projectile[p].friendly = true;
                    Main.projectile[p].hostile = false;
                    Main.projectile[p].scale = 0.7f;
                }
            }
            if (doubleKnockback)
                incDefTime = 180;
			if (thermalGauntlet)
			{
				if (target.life <= 0)
                {
                    for (int i = 0; i < Main.rand.Next(3, 6); i++)
                    {
						int type = 0;
						int x = Main.rand.Next(2);
						if (x == 0) type = mod.ProjectileType("ThermalBoltCold");
						if (x == 1) type = mod.ProjectileType("ThermalBoltHot");
                        Vector2 perturbedSpeed = new Vector2(0, 4).RotatedByRandom(MathHelper.ToRadians(360));
                        int p = Projectile.NewProjectile(target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, 20, 1, Main.myPlayer, 0, 0);
                    }
                }
                else
                {
					target.AddBuff(BuffID.OnFire, 180);
					target.AddBuff(BuffID.Frostburn, 180);
                    if (Main.rand.NextBool(7) && !target.boss)
                    {
                        target.AddBuff(mod.BuffType("Frozen"), 120);
                    }
                }
			}
			if (terraGauntlet)
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
                        int p = Projectile.NewProjectile(target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("IceShard2"), 20, 1, Main.myPlayer, 0, 0);
                    }
                }
                else
                {
                    if (Main.rand.NextBool(10) && !target.boss)
                    {
                        target.AddBuff(mod.BuffType("Frozen"), 120);
                    }
                }
            }
            if (crit && item.type == mod.ItemType("LifesFate"))
            {
                player.AddBuff(mod.BuffType("LifesFateBuff"), Main.rand.Next(840, 960));
            }
            if (item.type == mod.ItemType("LifesFate") && renewedLife)
            {
                int x = Main.rand.Next(1, 3);
                player.statLife += x;
                player.HealEffect(x);
            }
			if (crit && deathTalisman)
			{
				target.AddBuff(mod.BuffType("FatesDemise"), 720);
			}
			if (defenseInsignia && damage > 50)
			{
				int increasedChance = 4 + ((damage - 50) % 25);
				if (increasedChance > 12) increasedChance = 12;
				if (Main.rand.Next(100 / increasedChance) == 0)
				{
					Item.NewItem((int)target.position.X, (int)target.position.Y, target.width, target.height, mod.ItemType("ProtectiveEnergy"));
				}
			}
			if (crit && sporeFriend && Main.rand.NextBool(3))
			{
				if (sporeCount <= 0)
				{
					for (int i = 0; i < 6; i++)
					{
						Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("HelpfulSpore"), 30, 0, player.whoAmI, ai1: i);
						sporeCount++;
					}
				}
				player.AddBuff(mod.BuffType("Spored"), 2);
				
			}
			if (goblinSet) player.AddBuff(mod.BuffType("GoblinsCelerity"), 180);
		}
		public override void OnHitNPCWithProj (Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			if (projectile.magic && forestSetMage && Main.rand.Next(10) == 0)
			{
				primalRageTime = 600;
			}
			if (breakingPoint)
			{
				target.AddBuff(BuffID.OnFire,240);
			}
			if (terraGauntlet)
			{
				target.AddBuff(BuffID.OnFire, 180);
				target.AddBuff(BuffID.Frostburn, 180);
				target.AddBuff(BuffID.ShadowFlame, 180);
				
			}
			if (floralGauntlet && Main.rand.Next(20) == 0)
			{
				int lifeToHeal = damage;
				if (lifeToHeal > 25)
					lifeToHeal = 25;
				player.statLife += lifeToHeal;
				player.HealEffect(lifeToHeal);
			}
			if (target.life <= 0 && terraGauntlet)
            {
                for (int i = 0; i < Main.rand.Next(3, 6); i++)
			    {
                    Vector2 perturbedSpeed = new Vector2(0, 4).RotatedByRandom(MathHelper.ToRadians(360));
                    int p = Projectile.NewProjectile(target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("TerraBoltHost"), 75, 1, Main.myPlayer, 0, 0);
               }
            }
			if (ferocityGauntlet)
			{
				ferocityTime = 180;
			}
			if (crit && target.life <= 0 && deathTalisman)
			{
				int damage1 = 0;
				if (target.lifeMax > 3000)
				{
					damage1 = 300;
				}
				else
				{
					damage1 = target.lifeMax / 10;
				}
				//for (int i = 0; i < 5; i++)
				//{
					Vector2 perturbedSpeed = new Vector2(0, 5).RotatedByRandom(MathHelper.ToRadians(360));
					Projectile.NewProjectile(target.Center.X, target.Center.Y, 0, 0, mod.ProjectileType("FateFlame"), damage1, 1, Main.myPlayer, 0, 0);
					
				//}
			}
            if (meteorGauntlet)
            {
                target.AddBuff(BuffID.OnFire, 120);
                if (Main.rand.Next(3) == 0)
                {
                    Vector2 placePosition = target.Center + new Vector2(Main.rand.Next(-100, 100), -500);
                    Vector2 direction = target.Center - placePosition;
                    int p = Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * 12f, direction.Y * 12f, ProjectileID.Meteor1, 30, 1, Main.myPlayer, 0, 0);
                    Main.projectile[p].friendly = true;
                    Main.projectile[p].hostile = false;
                    Main.projectile[p].scale = 0.7f;
                }
            }
            if (doubleKnockback)
                incDefTime = 180;
            if (frostGauntlet)
            {
                if (target.life <= 0)
                {
                    for (int i = 0; i < Main.rand.Next(2, 4); i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(0, 6).RotatedByRandom(MathHelper.ToRadians(360));
                        int p = Projectile.NewProjectile(target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("IceShard2"), 20, 1, Main.myPlayer, 0, 0);
                    }
                }
                else
                {
                    if (Main.rand.NextBool(10) && !target.boss)
                    {
                        target.AddBuff(mod.BuffType("Frozen"), 120);
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
						if (x == 0) type = mod.ProjectileType("ThermalBoltCold");
						if (x == 1) type = mod.ProjectileType("ThermalBoltHot");
                        Vector2 perturbedSpeed = new Vector2(0, 4).RotatedByRandom(MathHelper.ToRadians(360));
                        int p = Projectile.NewProjectile(target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, type, 20, 1, Main.myPlayer, 0, 0);
                    }
                }
                else
                {
					target.AddBuff(BuffID.OnFire, 180);
					target.AddBuff(BuffID.Frostburn, 180);
                    if (Main.rand.NextBool(7) && !target.boss)
                    {
                        target.AddBuff(mod.BuffType("Frozen"), 120);
                    }
                }
			}
			if (crit && deathTalisman)
			{
				target.AddBuff(mod.BuffType("FatesDemise"), 720);
			}
			if (defenseInsignia && damage > 150)
			{
				if (Main.rand.Next(12) == 0)
				{
					Item.NewItem((int)target.position.X, (int)target.position.Y, target.width, target.height, mod.ItemType("ProtectiveEnergy"));
				}
			}
			if (crit && sporeFriend && Main.rand.NextBool(3))
			{
				if (sporeCount <= 0)
				{
					for (int i = 0; i < 6; i++)
					{
						Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("HelpfulSpore"), 30, 0, player.whoAmI, ai1: i);
						sporeCount++;
					}
				}
				player.AddBuff(mod.BuffType("Spored"), 2);
			}
		}
		
		public override void ModifyHitNPCWithProj (Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (slightKnockback)
			{
				knockback *= 1.1f;
			}
            if (doubleKnockback)
            {
                knockback *= 2f;
            }
			if ((ferocityGauntlet || terraGauntlet) && Main.rand.Next(10) == 0)
				damage *= 2;
			if (crit && rougeRage)
			{
				damage = damage += (damage / 10);
			}
			if (crit && vermillionValor)
			{
				damage = damage += ((damage * 13) / 100);
			}
        }
		public override void PreUpdateMovement()
		{
            if (waxwingActive)
			{
                player.velocity.Y *= 1.25f;
				Main.NewText("what", 255, 240, 20, false);
				waxwingActive = false;
            }
        }
    }
}

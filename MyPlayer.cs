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
        public bool ancientPelt = false;
		public bool eruptionBottle = false;
		public bool sporeFriend = false;
		private bool dashActive = false;
		public bool goblinSet = false;
        public bool aquaticSet = false;
        public bool yetiMount = false;
        public bool frostGauntlet = false;
        public bool meteorGauntlet = false;
        public bool renewedLife = false;
        public int dayVergeProjTime = 0;
		bool canJump = false;
        bool placedPlant = false;
		bool changedVelocity;
		bool clickedLeft = false;
		bool clickedRight = false;
		List<int> hitEnemies = new List<int>();
		public int yetiCooldown = 30;
		private int leftPresses = 0;
		private int rightPresses = 0;
		private int dashDelay = 0;
		private int pressTime = 0;
		public int sporeCount = 0;
		public int sporeBuffCount = 0;
		public int OathCooldown = 720;
		private int peltCounter = 120;
		private int peltRadius = 256;
		int SporeHealCooldown = 60;
        int incDefTime = 0;
        public override void ResetEffects()
        {
            meteorGauntlet = false;
            doubleKnockback = false;
            renewedLife = false;
            frostGauntlet = false;
            eruptionBottle = false;
            sharkMinion = false;
			cursedDash = false;
			ZoneVolcano = false;
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
            sporeBuffCount = 0;
        }
		public override void UpdateBiomes()
		{
			ZoneVolcano = EmperialWorld.VolcanoTiles > 100;
		}
		public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
		{
			if (Main.hardMode && player.ZoneSkyHeight)
			{
				if (Main.rand.Next(7) == 0)
				{
					caughtType = mod.ItemType("Glidefin");
				}
			}
		}
        public override void PostUpdate()
        {
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
                            Projectile.NewProjectile(i * 16 , j * 16 - 8, 0, 0, type, 0, 1, Main.myPlayer, 0, 0);
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
						if (player.Hitbox.Intersects(Main.npc[i].Hitbox) && !hitEnemies.Contains(i) && Main.npc[i].life > 1)
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
        }
		
		
		public override void UpdateBiomeVisuals()
		{

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
			for (int i = 0; i < Main.projectile.Length; i++)
            {
				if (Main.projectile[i].type == mod.ProjectileType("Needle"))
					Main.projectile[i].Kill();
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
                damage = (int) ((float) damage * 1.2f);
            }
            if (doubleKnockback)
            {
                knockback *= 2f;
            }
        }
		public override void OnHitNPC (Item item, NPC target, int damage, float knockback, bool crit)
		{
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
            if (crit && rougeRage)
			{
				damage = damage += (damage / 10);
			}
			if (crit && vermillionValor)
			{
				damage = damage += ((damage * 13) / 100);
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
            if (crit && rougeRage)
			{
				damage = damage += (damage / 10);
			}
			if (crit && vermillionValor)
			{
				damage = damage += ((damage * 13) / 100);
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
        }
    }
}
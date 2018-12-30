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
		public bool rougeRage = false;
		public bool vermillionValor = false;
		public bool deathTalisman = false;
		public bool forbiddenOath = false;
		public bool vitalityCrystal = false;
		public bool defenseInsignia = false;
		public bool isMellowProjectile = false;
		public bool slightKnockback = false;
		public bool ancientPelt = false;
		public bool sporeFriend = false;
		public bool yetiMount = false;
		public int yetiCooldown = 30;
		public int sporeCount = 0;
		public int sporeBuffCount = 0;
		public int OathCooldown = 720;
		private int peltCounter = 120;
		private int peltRadius = 256;
		int SporeHealCooldown = 60;
        public override void ResetEffects()
        {
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
			ancientPelt = false;
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
			if (forbiddenOath && player.statLife <= (player.statLifeMax2 / 5))
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
		public override void PreUpdate()
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
		}
		public override void OnHitNPC (Item item, NPC target, int damage, float knockback, bool crit)
		{
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
		}
		public override void OnHitNPCWithProj (Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
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
		public override bool PreHurt(bool pvp,bool quiet,ref int damage,ref int hitDirection,ref bool crit,ref bool customDamage,ref bool playSound,ref bool genGore,ref PlayerDeathReason damageSource)
		{
			return true;
		}
		public override void ModifyHitNPCWithProj (Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (slightKnockback)
			{
				knockback *= 1.1f;
			}
		}
    }
}
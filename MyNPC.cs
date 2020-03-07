﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;
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
		public bool sporeStorm = false;
        public bool scoriaExplosion = false;
		public bool electrified = false;
		public bool moreCoins = false;
        public int spineCount = 0;
		int InfirmaryTimer = 30;
        int poisonTimer = 0;
        public override void ResetEffects(NPC npc)
        {
            cuttingLeaves = false;
			electrified = false;
			//spineCount = 0;
			vermillionVenom = false;
			indigoInfirmary = false;
			burningNight = false;
			fatesDemise = false;
			sporeStorm = false;
            moreDamage = false;
			moreCoins = false;
		} 
		public override bool InstancePerEntity {get{return true;}}
		
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			/*if (type == NPCID.Dryad)
			{
				shop.item[nextSlot].SetDefaults(mod.ItemType<Items.ChestnutSeeds>());
				nextSlot++;

			}*/
		}
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
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
            
        }
		
		public override void AI(NPC npc)
		{
			poisonTimer++;
			if (spineCount > 0 && poisonTimer % 25 == 0)
			{
				//Main.NewText(spineCount);
				npc.life -= spineCount;
				//npc.StrikeNPCNoInteraction(spineCount, 0, 0, false, false, false);
				Color color2 = CombatText.DamagedHostile;
				CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), color2, spineCount, false, false);
				
				npc.HitEffect(0, 10);
			}
			InfirmaryTimer--;
			if (InfirmaryTimer <= 0)
			{
				InfirmaryTimer = 30;
				if (indigoInfirmary)
				{
					int damage = 1;
					Player player = Main.player[npc.target];
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
					npc.StrikeNPCNoInteraction(damage, 0, 0, false, false, false);
				}
			}
			if (npc.life <= 0)
			{
				
			}
			if (sporeStorm && !(npc.type == 488))
			{
				Player player = Main.player[npc.target];
				MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
				if (npc.boss)
					modPlayer.sporeBuffCount += 2;
				else
					modPlayer.sporeBuffCount += 1;
			}
            spineCount = 0;
			
			
		}
		public override void NPCLoot(NPC npc)  
        {
            if (scoriaExplosion)
            {
                int expDamage = npc.lifeMax / 5;
                if (expDamage > 50)
                {
                    expDamage = 50;
                }

                for (int num621 = 0; num621 < 20; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 258, 0f, 0f, 100, default(Color));
                    Main.dust[num622].velocity *= 3f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num622].scale = 0.5f;
                        Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int num623 = 0; num623 < 35; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 258, 0f, 0f, 100, default(Color));
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 5f;
                    num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 258, 0f, 0f, 100, default(Color));
                    Main.dust[num624].velocity *= 2f;
                }
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 14);
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (npc.Distance(Main.npc[i].Center) < 64 && !Main.npc[i].townNPC)
                        Main.npc[i].StrikeNPC(expDamage, 0f, 0, false, false, false);
                }
            }
			if (!Main.expertMode && npc.type == NPCID.SkeletronHead)
			{
				int x = Main.rand.Next(3);
				if (x == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Skelebow")); 
				}
				else if (x == 1)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NecromanticFlame")); 
				}
				else if (x == 2)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BoneWhip")); 
				}
			}
			if (Main.expertMode)
			{
				if (Main.rand.Next(150) == 0) 
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireBlade")); 
				}
			}
			else
			{
				if (Main.rand.Next(200) == 0) 
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FireBlade")); 
				}
			}
			if(npc.type == 82 && !Main.expertMode)
			{
				if (Main.rand.Next(50) == 0) 
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DeathTalisman")); 
				}
			}
			if(npc.type == 82 && Main.expertMode)
			{
				if (Main.rand.Next(40) == 0) 
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DeathTalisman")); 
				}
			}
			if(!Main.expertMode && npc.type == 53 || npc.type == 536 || npc.type == 489 || npc.type == 490 || npc.type == 47 || npc.type == 464 || npc.type == 57 || npc.type == 465 || npc.type == 168 || npc.type == 470 || npc.type == 109)
				{
					 if (Main.rand.Next(100) == 0) 
					 {
						 Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ForbiddenOath")); 
					 }
				}
				if(Main.expertMode && npc.type == 53 || npc.type == 536 || npc.type == 489 || npc.type == 490 || npc.type == 47 || npc.type == 464 || npc.type == 57 || npc.type == 465 || npc.type == 168 || npc.type == 470 || npc.type == 109)
				{
					 if (Main.rand.Next(65) == 0) 
					 {
						 Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ForbiddenOath")); 
					 }
				}
			if(npc.type == 58)
			{
				if (Main.rand.Next(25) == 0) 
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TetheredPiranha")); 
				}
			}
			if(npc.type == 122)
			{
				if (Main.rand.Next(40) == 0) 
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Escargun")); 
				}
			}
			if (fatesDemise)
			{
				
				int damage1 = 0;
				if (npc.lifeMax > 3000)
				{
					damage1 = 300;
				}
				else
				{
					damage1 = npc.lifeMax / 10;
				}
				//for (int i = 0; i < 5; i++)
				//{
					Vector2 perturbedSpeed = new Vector2(0, 5).RotatedByRandom(MathHelper.ToRadians(360));
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, mod.ProjectileType("FateFlame"), damage1, 1, Main.myPlayer, 0, 0);
					
				//}
			}
		}
		public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
		{
		}
		public override bool PreNPCLoot(NPC npc)
        {
			if (moreCoins)
            {
				npc.value += npc.value += Item.buyPrice(0, 0, 10, 0);
				return true;
			}
			return true;
        }
		public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
		{
			if (vermillionVenom)
			{
				damage = (int)(damage * 0.9f);
			}
		}
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (moreDamage)
            {
                damage = (int) (1.1 * damage);
            }
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (moreDamage)
            {
                damage = (int)(1.1 * damage);
            }
        }
    }
}

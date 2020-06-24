using Microsoft.Xna.Framework;
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
		public bool graniteMinionLatched = false;
		public bool crushFreeze = false;
		public int graniteMinID = -1;
        public int spineCount = 0;
		public int chillStacks = 0;
		
        public int strikeCount = 0;
		public int desertSpikeTime = 0;
		public int impaledDirection = 0;
		public float desertSpikeHeight = 0;
		public bool impaledGravity = true;
		public int maceSlam = 0;
		public int maceSlamDamage = 0;
		int InfirmaryTimer = 30;
        int poisonTimer = 0;

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
			//graniteMinionLatched = false;
		} 
		public override bool InstancePerEntity {get{return true;}}
		
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			if (type == NPCID.Merchant)
			{
				shop.item[nextSlot].SetDefaults(mod.ItemType("Lasagna"));
				nextSlot++;

			}
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
			if (!crushFreeze)
            {
				chillStacks = 0;
            }
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
				if (crushFreeze)
                {
					int damage = chillStacks;
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
				Main.PlaySound(SoundID.Item14, npc.Center);
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (npc.Distance(Main.npc[i].Center) < 1 && !Main.npc[i].townNPC)
                        Main.npc[i].StrikeNPC(maceSlamDamage, 0f, 0, false, false, false);
                }
				for (int i = 0; i < 15; ++i)
				{
					int index2 = Dust.NewDust(npc.Center, npc.width, npc.height, mod.DustType("CarapaceDust"), 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 2f;
				}
			}
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
			/*if (Main.expertMode)
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
			}*/
			//change this to be obtained another way
			if(npc.type == 82)
			{
				if ((!Main.expertMode && Main.rand.Next(50) == 0) || (Main.expertMode && Main.rand.Next(40) == 0)) 
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DeathTalisman")); 
				}
			}
			if (npc.type == 53 || npc.type == 536 || npc.type == 489 || npc.type == 490 || npc.type == 47 || npc.type == 464 || npc.type == 57 || npc.type == 465 || npc.type == 168 || npc.type == 470 || npc.type == 109)
			{
				if ((!Main.expertMode && Main.rand.Next(100) == 0) || (Main.expertMode && Main.rand.Next(65) == 0))
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ForbiddenOath")); 
				}			
			}
			if(npc.type == 58)
			{
				if (Main.rand.Next(50) == 0) 
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TetheredPiranha")); 
				}
			}
			if(npc.type == 122)
			{
				if (Main.rand.Next(50) == 0) 
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Escargun")); 
				}
			}
			if(npc.type == mod.NPCType("Yeti"))
			{
				if (!Main.expertMode) 
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Frostleaf"), Main.rand.Next(20, 30)); 
				}
			}
			if (!EmperialWorld.downedEye && npc.type == 4)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SetStone"));
				EmperialWorld.downedEye = true;
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
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FatesFlames"), damage1, 1, Main.myPlayer, 0, 0);	
				}
				Main.PlaySound(SoundID.NPCDeath52, npc.Center);
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
			if (crushFreeze)
				damage = (int)(damage * (1 - .05 * chillStacks));
		}
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (moreDamage)
            {
                damage = (int) (1.1 * damage);
            }

			if (desertSpikeTime > 0 && desertSpikeTime < 100)
			{
				desertSpikeTime = 0;
				npc.noGravity = impaledGravity;
				impaledGravity = true;
			}
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (moreDamage)
            {
                damage = (int)(1.1 * damage);
            }

			if (desertSpikeTime > 0 && desertSpikeTime < 100)
			{
				desertSpikeTime = 0;
				npc.noGravity = impaledGravity;
				impaledGravity = true;
			}
			if (item.type == mod.ItemType("Fungallows")) //wip
			{
				if (npc.life <= damage - npc.defense * 0.5)
				{
					Main.NewText("wow", 255, 240, 20, false);
					//damage = npc.life + npc.defense * 0.5 - 1;
					//npc = target;
        	    	//npc.GetGlobalNPC<MyNPC>().variableName = true;	
				}
			}
        }
    }
}

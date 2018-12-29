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
		public bool indigoInfirmary = false;
		public bool burningNight = false;
		public bool fatesDemise = false;
		public bool sporeStorm = false;
		int InfirmaryTimer = 30;
        public override void ResetEffects(NPC npc)
        {
            vermillionVenom = false;
			indigoInfirmary = false;
			burningNight = false;
			fatesDemise = false;
			sporeStorm = false;
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
				MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
				if (npc.boss)
					modPlayer.sporeBuffCount += 2;
				else
					modPlayer.sporeBuffCount += 1;
			}
			
			
		}
		public override void NPCLoot(NPC npc)  
        {
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
				if (Main.rand.Next(20) == 0) 
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DeathTalisman")); 
				}
			}
			if(npc.type == 82 && Main.expertMode)
			{
				if (Main.rand.Next(16) == 0) 
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
		public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
		{
			if (vermillionVenom)
			{
				damage = (int)(damage * 0.9f);
			}
		}
    }
}
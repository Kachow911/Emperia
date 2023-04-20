using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Desert
{
	//[AutoloadBossHead]
	public class DuneKing : ModNPC
	{
		int timer = 0;
		int moveSpeed = 0;
		int moveSpeedY = 0;
		float yDist = 125f;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dune King");
			Main.npcFrameCount[NPC.type] = 6;
		}

		public override void SetDefaults()
		{
			NPC.width = 80;
			NPC.height = 100;
			NPC.damage = 20;
			NPC.lifeMax = 850;
			NPC.knockBackResist = 0;

			NPC.boss = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;

			//NPC.HitSound = SoundID.NPCHit7;
			//NPC.DeathSound = SoundID.NPCDeath5;
		}

		private int Counter;
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.2f;
			NPC.frameCounter %= 6;
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}
		public override void AI()
		{
			{
				NPC.spriteDirection = NPC.direction;
				Player player = Main.player[NPC.target];
				if (NPC.Center.X > player.Center.X && moveSpeed >= -40) 
				{
					moveSpeed--;
				}

				if (NPC.Center.X <= player.Center.X && moveSpeed <=40)
				{
					moveSpeed++;
				}

				NPC.velocity.X = moveSpeed * 0.1f;

				if (NPC.Center.Y > player.Center.Y - yDist && moveSpeedY >= -30) 
				{
					moveSpeedY--;
					yDist = 150f;
				}

				if (NPC.Center.Y <= player.Center.Y - yDist && moveSpeedY <= 30)
				{
					moveSpeedY++;
				}
				NPC.spriteDirection = -NPC.direction;
				NPC.velocity.Y = moveSpeedY * 0.1f;
				/*if (Main.rand.Next(220) == 6)
				{
					HomeY = -35f;
				}*/
 
			}
			
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			
		}



		

		/*public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
		}*/

		
	}
}

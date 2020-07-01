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
			DisplayName.SetDefault("Dune King");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 80;
			npc.height = 100;
			npc.damage = 20;
			npc.lifeMax = 850;
			npc.knockBackResist = 0;

			npc.boss = true;
			npc.noGravity = true;
			npc.noTileCollide = true;

			//npc.HitSound = SoundID.NPCHit7;
			//npc.DeathSound = SoundID.NPCDeath5;
		}

		private int Counter;
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.2f;
			npc.frameCounter %= 6;
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
		public override void AI()
		{
			{
				npc.spriteDirection = npc.direction;
				Player player = Main.player[npc.target];
				if (npc.Center.X > player.Center.X && moveSpeed >= -40) 
				{
					moveSpeed--;
				}

				if (npc.Center.X <= player.Center.X && moveSpeed <=40)
				{
					moveSpeed++;
				}

				npc.velocity.X = moveSpeed * 0.1f;

				if (npc.Center.Y > player.Center.Y - yDist && moveSpeedY >= -30) 
				{
					moveSpeedY--;
					yDist = 150f;
				}

				if (npc.Center.Y <= player.Center.Y - yDist && moveSpeedY <= 30)
				{
					moveSpeedY++;
				}
				npc.spriteDirection = -npc.direction;
				npc.velocity.Y = moveSpeedY * 0.1f;
				/*if (Main.rand.Next(220) == 6)
				{
					HomeY = -35f;
				}*/
 
			}
			
		}

		public override void NPCLoot()
		{
			
		}



		

		/*public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.GreaterHealingPotion;
		}*/

		
	}
}

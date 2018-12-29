using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.GoblinArmy
{
    public class GoblinBattleRam: ModNPC
    {
		private bool charging;
		int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Battle Ram");
			Main.npcFrameCount[npc.type] = 14;
		}
        public override void SetDefaults()
        {
            npc.lifeMax = 250;
            npc.damage = 15;
            npc.defense = 3;
            npc.knockBackResist = -3f;
            npc.width = 96;
            npc.height = 56;
            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.npcSlots = 1f;
            npc.boss = false;
            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
			npc.scale = 1f;
			npc.aiStyle = 3;
			aiType = 508;
        }
		public override void FindFrame(int frameHeight)
		{	
			if (charging)
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 6; 
				int frame = (int)npc.frameCounter + 8; 
				npc.frame.Y = frame * frameHeight; 
			}
			else
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 8; 
				int frame = (int)npc.frameCounter; 
				npc.frame.Y = frame * frameHeight; 
			}
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 300;
            npc.damage = 20;
        }

        public override void AI()
		{
			counter++;
			Player player = Main.player[npc.target];
			if (Math.Abs(npc.velocity.X) < 5f)
			{
				if (npc.Center.X > player.Center.X)
					npc.velocity.X -= .07f;
				else if (npc.Center.X < player.Center.X)
					npc.velocity.X += .07f;
			}
			if (npc.velocity.X > 5f)
				npc.velocity.X = 5f;
			if (npc.velocity.X < -5f)
				npc.velocity.X = -5f;
			if (npc.velocity.X > 0)
				npc.spriteDirection = 1;
			else if (npc.velocity.X < 0)
				npc.spriteDirection = -1; 
			charging = (Math.Abs(npc.velocity.X) > 4.5f);
			if (charging)
			{
				npc.velocity.Y = 5f;
				Color rgb = new Color(252, 207, 83);
				npc.damage = 100;
				if (counter % 5 == 0)
					Dust.NewDust(npc.Center, npc.width, npc.height, 76, npc.velocity.X, (float) npc.velocity.Y, 0, rgb, 0.9f);
			}
			else
			{
				npc.damage = 20;
			}
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (charging)
			{
				Main.PlaySound(SoundID.Item, npc.Center, 14);
				Color rgb = new Color(252, 207, 83);
				for (int i = - 50; i < 50; i++)
				{
					int index2 = Dust.NewDust(npc.Center + new Vector2(i, 0), npc.width, npc.height, 76, npc.velocity.X / 5, (float) npc.velocity.Y, 0, rgb, 0.9f);
				}
				
				Gore.NewGore(npc.position, new Vector2(0, -2), mod.GetGoreSlot("Gores/BattleRam"), 1f);
				npc.life = 0;
				NPC.NewNPC((int)npc.Center.X + 25, (int)npc.Center.Y, mod.NPCType("GoblinRamCarrier"));
				NPC.NewNPC((int)npc.Center.X - 50, (int)npc.Center.Y, mod.NPCType("GoblinRamCarrier"));
			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.spawnTileX;
			int y = spawnInfo.spawnTileY;
			int tile = Main.tile[x, y].type;
			return Main.invasionType == 1 ? 0.2f : 0;
		}
       

       /* private void SmoothMoveToPosition(Vector2 toPosition, float addSpeed, float maxSpeed, float slowRange = 64, float slowBy = .95f)
        {
            if (Math.Abs((toPosition - npc.Center).Length()) >= slowRange)
            {
                npc.velocity += Vector2.Normalize((toPosition - npc.Center) * addSpeed);
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -maxSpeed, maxSpeed);
                npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -maxSpeed, maxSpeed);
            }
            else
            {
                npc.velocity *= slowBy;
            }
        }*/
		public override void NPCLoot()
		{
			Main.PlaySound(SoundID.Item, npc.Center, 14);
			Color rgb = new Color(252, 207, 83);
			for (int i = - 50; i < 50; i++)
			{
				int index2 = Dust.NewDust(npc.Center + new Vector2(i, 0), npc.width, npc.height, 76, npc.velocity.X / 5, (float) npc.velocity.Y, 0, rgb, 0.9f);
			}
				
			Gore.NewGore(npc.position, new Vector2(0, -2), mod.GetGoreSlot("Gores/BattleRam"), 1f);
			NPC.NewNPC((int)npc.Center.X + 25, (int)npc.Center.Y, mod.NPCType("GoblinRamCarrier"));
			NPC.NewNPC((int)npc.Center.X - 50, (int)npc.Center.Y, mod.NPCType("GoblinRamCarrier"));
		}
        
    }
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Emperia.Npcs.Twilight
{
	public class FlyingPolyp : ModNPC
	{
		int move = 0;
		int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flying Polyp");
			Main.npcFrameCount[npc.type] = 20;
		}

		public override void SetDefaults()
		{
			npc.width = 48;
			npc.height = 92;
			npc.damage = 35;
			npc.defense = 15;
			npc.lifeMax = 380;
			npc.HitSound = SoundID.NPCHit3;
			npc.DeathSound = SoundID.NPCDeath6;
			npc.value = 10f;
			npc.knockBackResist = .40f;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.aiStyle = 22;
			aiType = NPCID.Wraith;
			npc.stepSpeed = .3f;
		}
		public override void FindFrame(int frameHeight)
		{
			if (move == 0)
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 10;
				int frame = (int)npc.frameCounter;
				npc.frame.Y = frame * frameHeight;
			}
			else if (move == 1)
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 10;
				int frame = (int)npc.frameCounter + 10;
				npc.frame.Y = frame * frameHeight;
			}
		}
		public override void AI()
		{
			if (npc.velocity.X < 0)
				npc.spriteDirection = -1;
			else
				npc.spriteDirection = 1;
			if (move == 0)
			{
				counter++;
				if (counter >= 600)
                {
					move = 1;
					counter = 0;
                }
			}
			if (move == 1)
            {
				counter++;
				npc.velocity.X = 0;
				npc.velocity.Y = 0.5f * (float)Math.Cos(MathHelper.ToRadians(counter * 3));
				if (counter >= 50)
                {
					for (int i = 0; i < 5; i++)
					{

						Vector2 perturbedSpeed = new Vector2(-3, 0).RotatedBy(MathHelper.ToRadians(36 * i));
						int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 30, mod.NPCType("PolypMinion"));
						Main.npc[n].velocity = perturbedSpeed * 2f;
					}
					counter = 0;
					move = 0;
                }
            }
		}
		private static int[] SpawnTiles = { };
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (SpawnTiles.Length == 0)
			{
				int[] Tiles = { mod.TileType("TwilightGrass"), mod.TileType("TFWood"), mod.TileType("TFLeaf") };
				SpawnTiles = Tiles;
			}
			return SpawnTiles.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && !spawnInfo.playerSafe && !spawnInfo.invasion ? 2f : 0f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, 13);
				Gore.NewGore(npc.position, npc.velocity, 12);
				Gore.NewGore(npc.position, npc.velocity, 11);
			}
		}

		public override void NPCLoot()
		{
			
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = Convert.ToInt32(npc.lifeMax * 1.4);
			npc.damage = Convert.ToInt32(npc.damage * 1.4);
		}

	}
}
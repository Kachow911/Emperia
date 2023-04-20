using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
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
			Main.npcFrameCount[NPC.type] = 20;
		}

		public override void SetDefaults()
		{
			NPC.width = 48;
			NPC.height = 92;
			NPC.damage = 35;
			NPC.defense = 15;
			NPC.lifeMax = 380;
			NPC.HitSound = SoundID.NPCHit3;
			NPC.DeathSound = SoundID.NPCDeath6;
			NPC.value = 10f;
			NPC.knockBackResist = .40f;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.aiStyle = 22;
			AIType = NPCID.Wraith;
			NPC.stepSpeed = .3f;
		}
		public override void FindFrame(int frameHeight)
		{
			if (move == 0)
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 10;
				int frame = (int)NPC.frameCounter;
				NPC.frame.Y = frame * frameHeight;
			}
			else if (move == 1)
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 10;
				int frame = (int)NPC.frameCounter + 10;
				NPC.frame.Y = frame * frameHeight;
			}
		}
		public override void AI()
		{
			if (NPC.velocity.X < 0)
				NPC.spriteDirection = -1;
			else
				NPC.spriteDirection = 1;
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
				NPC.velocity.X = 0;
				NPC.velocity.Y = 0.5f * (float)Math.Cos(MathHelper.ToRadians(counter * 3));
				if (counter >= 50)
                {
					for (int i = 0; i < 5; i++)
					{

						Vector2 perturbedSpeed = new Vector2(-3, 0).RotatedBy(MathHelper.ToRadians(36 * i));
						int n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 30, NPCType<PolypMinion>());
						Main.npc[n].velocity = perturbedSpeed * 2f;
					}
					counter = 0;
					move = 0;
                }
            }
		}
		private static int[] SpawnTiles = { };
		/*public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (SpawnTiles.Length == 0)
			{
				int[] Tiles = { ModContent.TileType<Tiles.TwilightGrass>(), ModContent.TileType<Tiles.TFWood>(), ModContent.TileType<Tiles.TFLeaf>() };
				SpawnTiles = Tiles;
			}
			return SpawnTiles.Contains(Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].TileType) && !spawnInfo.PlayerSafe && !spawnInfo.Invasion ? 2f : 0f;
		}*/

		public override void HitEffect(int hitDirection, double damage)
		{
			if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, 13); //this should be getsource_onhurt but how do you get an attacker source and who cares
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, 12); //also there should be getsource_hiteffect for cases like this but it just doesnt exist
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, 11);
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			NPC.lifeMax = Convert.ToInt32(NPC.lifeMax * 1.4);
			NPC.damage = Convert.ToInt32(NPC.damage * 1.4);
		}

	}
}
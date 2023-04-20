using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Chasm
{
    public class UnstableSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Unstable Slime");
			Main.npcFrameCount[NPC.type] = 2;
		}

		int mult;
		int numTeleports = 5;
		public override void SetDefaults()
		{
			NPC.lifeMax = 160;
			NPC.damage = 25;
			NPC.defense = 5;
			NPC.width = 34;
			NPC.height = 28;
			NPC.aiStyle = 1;
			NPC.knockBackResist = 0f;
			AnimationType = 81;
			NPC.npcSlots = 1f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath4;
			NPC.value = Item.buyPrice(0, 0, 7, 8);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			NPC.lifeMax = Convert.ToInt32(NPC.lifeMax * 1.4);
			NPC.damage = Convert.ToInt32(NPC.damage * 1.4);
		}
		public override void HitEffect(int hitDirection, double Damage)
		{
			if (NPC.life <= 0)
			{
				return;
			}
			
			float teleLenX = (Main.rand.Next(-100, 100) / numTeleports);
			float teleLenY = (Main.rand.Next(-100, 0) / numTeleports);
			//divides the length it needs to teleport into squares based on the number of teleports specified
			
			for (int m = 0; m <= numTeleports; m++) //teleports vertically, creates a dust effect, then teleports horizontally and creates another dust effect.
			//also it waits to make the teleport perceivable
			{
				teleportRelative(0, teleLenY);
				for (int n = 0; n <= 10; n++)
					{
						int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 58, 0f, 0f, 0, new Color(), 1.5f);
						Main.dust[dust].noGravity = true;
					}
				waitFor(1000);
				teleportRelative(teleLenX, 0);
				for (int n = 0; n <= 10; n++)
					{
						int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 58, 0f, 0f, 0, new Color(), 1.5f);
						Main.dust[dust].noGravity = true;
					}
				waitFor(1000);
			}
			
			//Eventually it will teleport in a zigzag pattern to the target as defined by the list
		}
		
		/*public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.SpawnTileX;
			int y = spawnInfo.SpawnTileY;
			int tile = Main.tile[x, y].TileType;
			return (tile == ModContent.TileType<Tiles.AphoticStone>() || tile == ModContent.TileType<Tiles.GloomStone>()) ? 0.80f : 0;
		}*/
		
		private void teleportRelative(float x, float y)
		{
			NPC.position.X = NPC.position.X + x;
			NPC.position.Y = NPC.position.Y + y;
		}
		
		private void waitFor(int milliseconds) //a hacked version of waiting, shouldn't pause the entire game
		{
			milliseconds = milliseconds / (16 + (2/3));
			for (int x = 0; x <= milliseconds; x++)
			{
				if (x >= milliseconds)
				{
					return;
				}
			}
			return;
		}
	}
}

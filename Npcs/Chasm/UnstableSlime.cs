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
			Main.npcFrameCount[npc.type] = 2;
		}

		int mult;
		int numTeleports = 5;
		public override void SetDefaults()
		{
			npc.lifeMax = 160;
			npc.damage = 25;
			npc.defense = 5;
			npc.width = 34;
			npc.height = 28;
			npc.aiStyle = 1;
			npc.knockBackResist = 0f;
			animationType = 81;
			npc.npcSlots = 1f;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath4;
			npc.value = Item.buyPrice(0, 0, 7, 8);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = Convert.ToInt32(npc.lifeMax * 1.4);
			npc.damage = Convert.ToInt32(npc.damage * 1.4);
		}
		public override void HitEffect(int hitDirection, double Damage)
		{
			if (npc.life <= 0)
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
						int dust = Dust.NewDust(npc.position, npc.width, npc.height, 58, 0f, 0f, 0, new Color(), 1.5f);
						Main.dust[dust].noGravity = true;
					}
				waitFor(1000);
				teleportRelative(teleLenX, 0);
				for (int n = 0; n <= 10; n++)
					{
						int dust = Dust.NewDust(npc.position, npc.width, npc.height, 58, 0f, 0f, 0, new Color(), 1.5f);
						Main.dust[dust].noGravity = true;
					}
				waitFor(1000);
			}
			
			//Eventually it will teleport in a zigzag pattern to the target as defined by the list
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.spawnTileX;
			int y = spawnInfo.spawnTileY;
			int tile = Main.tile[x, y].type;
			return (tile == mod.TileType("AphoticStone") || tile == mod.TileType("GloomStone")) ? 0.80f : 0;
		}
		
		private void teleportRelative(float x, float y)
		{
			npc.position.X = npc.position.X + x;
			npc.position.Y = npc.position.Y + y;
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

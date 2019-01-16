using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs
{
	public class FishEnemy : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Torrential Spearer");
			Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Shark];
		}

		public override void SetDefaults()
		{
			npc.width = 118;
			npc.height = 42;
			npc.damage = 25;
			npc.defense = 5;
			npc.lifeMax = 650;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.noGravity = true;
			npc.value = 60f;
			npc.knockBackResist = .55f;
			npc.aiStyle = 16;
			aiType = NPCID.Arapaima;
			animationType = NPCID.Shark;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OceanMonster.Chance * 0.2f;
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(2) == 1)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AquaticScale"), Main.rand.Next(2, 4));
			}
		}


	}
}

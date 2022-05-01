using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Emperia.Npcs
{
	public class FishEnemy : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Torrential Spearer");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Shark];
		}

		public override void SetDefaults()
		{
			NPC.width = 118;
			NPC.height = 42;
			NPC.damage = 25;
			NPC.defense = 5;
			NPC.lifeMax = 650;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.noGravity = true;
			NPC.value = 60f;
			NPC.knockBackResist = .55f;
			NPC.aiStyle = 16;
			AIType = NPCID.Arapaima;
			AnimationType = NPCID.Shark;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OceanMonster.Chance * 0.2f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			if (Main.rand.Next(2) == 1)
			{
				Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.AquaticScale>(), Main.rand.Next(2, 4));
			}
		}


	}
}

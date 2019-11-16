using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Emperia.Npcs.Yeti;

namespace Emperia.Items
{
	public class YetiBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
		}
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.rare = 9;
			item.expert = true;
		}
		public override int BossBagNPC => NPCType<Yeti>();

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("YetiMask"));
			}
			if (Main.rand.Next(4) != 0)
			{
				player.QuickSpawnItem(mod.ItemType("ArcticIncantation"));
			}
			if (Main.rand.Next(4) != 0)
			{
				player.QuickSpawnItem(mod.ItemType("HuntersSpear"));
			}
			if (Main.rand.Next(4) != 0)
			{
				player.QuickSpawnItem(mod.ItemType("BigGameHunter"));
			}
			if (Main.rand.Next(4) != 0)
			{
				player.QuickSpawnItem(mod.ItemType("MammothineClub"));
			}
			if (Main.rand.Next(5) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("ChilledFootprint"));
			}
			player.QuickSpawnItem(mod.ItemType("AncientPelt"));
			
		}
	}
}
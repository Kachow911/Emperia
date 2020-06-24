using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class YetiBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag");
			Tooltip.SetDefault("Right Click to open");
		}


		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.rare = -2;

			item.maxStack = 30;

			item.expert = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("YetiMask"));
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("ArcticIncantation"));
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("HuntersSpear"));
			}
			if (Main.rand.Next(2) != 0)
			{
				player.QuickSpawnItem(mod.ItemType("BigGameHunter"));
			}
			if (Main.rand.Next(2) != 0)
			{
				player.QuickSpawnItem(mod.ItemType("MammothineClub"));
			}
			if (Main.rand.Next(5) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("ChilledFootprint"));
			}
			player.QuickSpawnItem(mod.ItemType("AncientPelt"));
			player.QuickSpawnItem(mod.ItemType("Frostleaf"), Main.rand.Next(25, 35));
		}
	}
}

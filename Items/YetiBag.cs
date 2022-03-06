using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Items.Weapons.Yeti;
using Emperia.Items.Accessories;
using Terraria.DataStructures;

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
			Item.width = 20;
			Item.height = 20;
			Item.rare = -2;

			Item.maxStack = 30;

			Item.expert = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			IEntitySource source = player.GetItemSource_OpenItem(ModContent.ItemType<YetiBag>());
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<Items.Armor.YetiMask>());
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<ArcticIncantation>());
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<HuntersSpear>());
			}
			if (Main.rand.Next(2) != 0)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<IcicleCannon>());
			}
			if (Main.rand.Next(2) != 0)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<MammothineClub>());
			}
			if (Main.rand.Next(5) == 0)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<ChilledFootprint>());
			}
			player.QuickSpawnItem(source, ModContent.ItemType<AncientPelt>());
			player.QuickSpawnItem(source, ModContent.ItemType<Items.Sets.PreHardmode.Frostleaf.Frostleaf>(), Main.rand.Next(25, 35));
		}
	}
}

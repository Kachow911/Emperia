using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Items.Weapons.Yeti;
using Emperia.Items.Accessories;

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
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<Items.Armor.YetiMask>());
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<ArcticIncantation>());
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<HuntersSpear>());
			}
			if (Main.rand.Next(2) != 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<IcicleCannon>());
			}
			if (Main.rand.Next(2) != 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<MammothineClub>());
			}
			if (Main.rand.Next(5) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<ChilledFootprint>());
			}
			player.QuickSpawnItem(ModContent.ItemType<AncientPelt>());
			player.QuickSpawnItem(ModContent.ItemType<Items.Sets.PreHardmode.Frostleaf.Frostleaf>(), Main.rand.Next(25, 35));
		}
	}
}

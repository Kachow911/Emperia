using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Emperia.Items.Weapons.Yeti;
using Emperia.Items.Accessories;
using Terraria.DataStructures;
using Emperia.Npcs.Yeti;

namespace Emperia.Items
{
	public class YetiBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag (Yeti)");
			Tooltip.SetDefault("Right Click to open");
			ItemID.Sets.BossBag[Type] = true;
			ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
		}

		public override int BossBagNPC => ModContent.NPCType<Yeti>();
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = -2;
			Item.maxStack = 999;
			Item.expert = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

        public override void OpenBossBag(Player player)
        {
			IEntitySource source = player.GetSource_OpenItem(ModContent.ItemType<YetiBag>());
			/*if (Main.rand.Next(2) == 0)
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
			}*/
			switch (Main.rand.Next(4))
            {
				case 0:
					player.QuickSpawnItem(source, ModContent.ItemType<ArcticIncantation>());
					break;
				case 1:
					player.QuickSpawnItem(source, ModContent.ItemType<HuntersSpear>());
					break;
				case 2:
					player.QuickSpawnItem(source, ModContent.ItemType<IcicleCannon>());
					break;
				case 3:
					player.QuickSpawnItem(source, ModContent.ItemType<MammothineClub>());
					break;
			}
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<Items.Armor.YetiMask>());
			}
			if (Main.rand.Next(5) == 0)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<ChilledFootprint>());
			}
			player.QuickSpawnItem(source, ModContent.ItemType<AncientPelt>());
			player.QuickSpawnItem(source, ModContent.ItemType<Sets.PreHardmode.Frostleaf.Frostleaf>(), Main.rand.Next(25, 35));
		}
	}
}

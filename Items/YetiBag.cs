using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Emperia.Items.Weapons.Yeti;
using Emperia.Items.Accessories;
using Terraria.DataStructures;
using Emperia.Npcs.Yeti;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.ItemDropRules;

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
		}

		//public override int BossBagNPC => ModContent.NPCType<Yeti>();
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
		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			//itemLoot.Add(EmperiaDropRule.OneFromOptionsCycleThroughPerRoll("Yeti", 1, ItemType<ArcticIncantation>(), ItemType<HuntersSpear>(), ItemType<IcicleCannon>(), ItemType<MammothineClub>()));
			itemLoot.Add(ItemDropRule.Common(ItemType<AncientPelt>()));
			itemLoot.Add(ItemDropRule.Common(ItemType<Sets.PreHardmode.Frostleaf.Frostleaf>(), 1, 25, 35));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<Armor.YetiMask>(), 7));
			itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<ChilledFootprint>(), 5));
			itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemType<ArcticIncantation>(), ItemType<HuntersSpear>(), ItemType<IcicleCannon>(), ItemType<MammothineClub>()));
		}
	}
}

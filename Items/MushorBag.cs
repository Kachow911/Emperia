using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

using Emperia.Npcs.Mushor;
using Emperia.Items.Weapons.Mushor;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;

namespace Emperia.Items
{
	public class MushorBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag (Mushor)");
			Tooltip.SetDefault("Right Click to open");
			ItemID.Sets.BossBag[Type] = true;
			ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
		}


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
			itemLoot.Add(ItemDropRule.Common(ItemType<Accessories.MycelialShield>()));
			//itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<MushorMask>(), 7));
			//itemLoot.Add(ItemDropRule.NotScalingWithLuck(ItemType<MushorTrophy>(), 10));
			itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemType<Shroomer>(), ItemType<Mushdisc>(), ItemType<Shroomflask>(), ItemType<Items.Weapons.Mushor.Shroomerang>()));
		}
	}
}

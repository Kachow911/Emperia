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

		public override int BossBagNPC => NPCType<Mushor>();
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag (Mushor)");
			Tooltip.SetDefault("Right Click to open");
			ItemID.Sets.BossBag[Type] = true;
			ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
			//CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
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

        /*public override void OpenBossBag(Player player)
		{
			//if (Main.rand.Next(6) == 0)
			//{
			//	player.QuickSpawnItem(ModContent.ItemType<MushorMask>());
			//}
			IEntitySource source = player.GetSource_OpenItem(ModContent.ItemType<MushorBag>());
			switch (Main.rand.Next(4))
			{
				case 0:
					player.QuickSpawnItem(source, ItemType<Shroomer>());
					break;
				case 1:
					player.QuickSpawnItem(source, ItemType<Mushdisc>());
					break;
				case 2:
					player.QuickSpawnItem(source, ItemType<Shroomflask>());
					break;
				case 3:
					player.QuickSpawnItem(source, ItemType<Shroomerang>());
					break;
			}
			player.QuickSpawnItem(source, ItemType<Items.Accessories.MycelialShield>());
			
		}*/
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
			/*LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
			{
				notExpertRule.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemType<Shroomer>(), ItemType<Mushdisc>(), ItemType<Shroomflask>(), ItemType<Items.Weapons.Mushor.Shroomerang>()));
				//notExpertRule.OnSuccess(ItemDropRule.Common(ItemType<MushorMask>(), 7));
				//notExpertRule.OnSuccess(ItemDropRule.Common(ItemType<MushorTrophy>(), 10));
				itemLoot.Add(notExpertRule);
			}*/
			//itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, ItemType<Shroomer>(), ItemType<Mushdisc>(), ItemType<Shroomflask>(), ItemType<Items.Weapons.Mushor.Shroomerang>()));

			
			itemLoot.Add(EmperiaDropRule.OneFromOptionsCycleThroughPerRoll("mushor", 1, ItemType<Shroomer>(), ItemType<Mushdisc>(), ItemType<Shroomflask>(), ItemType<Items.Weapons.Mushor.Shroomerang>()));
		}
	}
}

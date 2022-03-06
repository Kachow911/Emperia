using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using Emperia.Npcs.Mushor;
using Emperia.Items.Weapons.Mushor;
using Terraria.DataStructures;

namespace Emperia.Items
{
	public class MushorBag : ModItem
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
		//public override int BossBagNPC => NPCType<<Mushor>();
		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			//if (Main.rand.Next(6) == 0)
			//{
			//	player.QuickSpawnItem(ModContent.ItemType<MushorMask>());
			//}
			IEntitySource source = player.GetItemSource_OpenItem(ModContent.ItemType<MushorBag>());
			if (Main.rand.Next(3) != 0)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<Shroomer>());
			}
			if (Main.rand.Next(3) != 0)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<Mushdisc>());
			}
			if (Main.rand.Next(3) != 0)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<Shroomflask>());
			}
			if (Main.rand.Next(3) != 0)
			{
				player.QuickSpawnItem(source, ModContent.ItemType<Shroomerang>());
			}
			player.QuickSpawnItem(source, ModContent.ItemType<Items.Accessories.MycelialShield>());
			
		}
	}
}

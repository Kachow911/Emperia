using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using Emperia.Npcs.Mushor;

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
			item.width = 20;
			item.height = 20;
			item.rare = -2;

			item.maxStack = 30;

			item.expert = true;
		}
		//public override int BossBagNPC => NPCType<Mushor>();
		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			if (Main.rand.Next(6) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("MushorMask"));
			}
			if (Main.rand.Next(3) != 0)
			{
				player.QuickSpawnItem(mod.ItemType("Shroomer"));
			}
			if (Main.rand.Next(3) != 0)
			{
				player.QuickSpawnItem(mod.ItemType("Mushdisc"));
			}
			if (Main.rand.Next(3) != 0)
			{
				player.QuickSpawnItem(mod.ItemType("Shroomflask"));
			}
			if (Main.rand.Next(3) != 0)
			{
				player.QuickSpawnItem(mod.ItemType("Shroomerang"));
			}
			player.QuickSpawnItem(mod.ItemType("MycelialShield"));
			
		}
	}
}

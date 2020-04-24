using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Events;

namespace Emperia.Items
{
	public class SetStone : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Set's Stone");
			Tooltip.SetDefault("Brings forth a sandstorm");
		}


		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.rare = 3;
			item.maxStack = 1;
			item.noUseGraphic = true;
			item.useStyle = 4;
			item.value = Item.sellPrice(0, 0, 1, 0);
			item.useTime = item.useAnimation = 20;
			item.consumable = true;
			item.autoReuse = true;

		}
		public override bool CanUseItem(Player player)
		{
			if (Sandstorm.Happening)
			{
				return false;
			}
			return true;
		}
		bool sandText = false;
		public override bool UseItem(Player player)
		{
			if (!sandText)
			{
				Main.NewText("A Raging Tyrant brings terror to the Desert, slay him", 204, 153, 51);
				sandText = true;
			}
			Sandstorm.Happening = true;
			Sandstorm.TimeLeft = 21600;
			Sandstorm.Severity = 1f;
			return true;
		}

	}
}
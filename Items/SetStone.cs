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
			// DisplayName.SetDefault("Set's Stone");
			// Tooltip.SetDefault("Brings forth a sandstorm");
		}


		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.rare = ItemRarityID.Orange;
			Item.noUseGraphic = true;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.value = Item.sellPrice(0, 0, 1, 0);
			Item.useTime = Item.useAnimation = 20;
			Item.consumable = true;
			Item.autoReuse = true;

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
		public override bool? UseItem(Player player)
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
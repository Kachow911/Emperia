using System;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class MoonPedestalItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moon Pedestal");
			Tooltip.SetDefault("Placing the pedestal will cause you to respawn with max health and mana");
		}



		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 14;

			item.maxStack = 1;

			item.useStyle = 1;
			item.useTime = 10;
			item.useAnimation = 15;

			item.useTurn = true;
			item.autoReuse = true;
			item.consumable = true;
			item.rare = 3;
			item.createTile = mod.TileType("MoonPedestal");
		}
	}
}

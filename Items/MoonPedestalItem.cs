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
			// DisplayName.SetDefault("Moon Pedestal");
			// Tooltip.SetDefault("Players will respawn with max life and mana when placed nearby");
		}



		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 14;

			Item.maxStack = 1;

			Item.useStyle = 1;
			Item.useTime = 10;
			Item.useAnimation = 15;

			Item.useTurn = true;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.rare = 3;
			Item.createTile = ModContent.TileType<Tiles.MoonPedestal>();
		}
	}
}

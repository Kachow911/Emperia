using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items
{
	public class BloodCandle : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Blood Candle");
			Tooltip.SetDefault("Greatly increases enemy spawn rates and brings good fortune\nKindling the flame will exact a blood sacrifice");
		}

		public override void SetDefaults() {
			item.width = 10;
			item.height = 10;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = TileType<Tiles.BloodCandle>();
			item.flame = true;
			item.value = 120000;
		}
	}
}
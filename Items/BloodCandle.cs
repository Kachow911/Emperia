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
			// DisplayName.SetDefault("Blood Candle");
			// Tooltip.SetDefault("Greatly increases enemy spawn rates\nKindling the flame will exact a blood sacrifice that is said to earn fate's favor");
		}

		public override void SetDefaults() {
			Item.width = 10;
			Item.height = 10;
						Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = 1;
			Item.consumable = true;
			Item.createTile = TileType<Tiles.BloodCandleTile>();
			Item.rare = 4;
			Item.value = 120000;
		}
	}
}
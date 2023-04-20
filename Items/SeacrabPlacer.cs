using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class SeacrabPlacer : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Grotto Dirt");
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
						Item.value = 1000;
			Item.rare = 1;
			Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            //Item.createTile = ModContent.TileType<Tiles.Seacrabtile>();
		}
	}
}
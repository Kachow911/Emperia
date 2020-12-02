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
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 1000;
			item.rare = 1;
			item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("Seacrabtile");
		}
	}
}
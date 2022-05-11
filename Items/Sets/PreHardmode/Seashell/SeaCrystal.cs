using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Seashell
{
	public class SeaCrystal : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sea Crystal");
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 20;
			Item.maxStack = 999;
			Item.value = 20000;
			Item.rare = 1;
			Item.createTile = ModContent.TileType<Tiles.SeaCrystalTile>();
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
		}
	}
}
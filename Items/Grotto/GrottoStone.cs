using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Grotto
{
	public class GrottoStone : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Duskstone Block");
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.maxStack = 999;
			Item.rare = 0;
			Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.TwilightStone>();
		}
	}
}
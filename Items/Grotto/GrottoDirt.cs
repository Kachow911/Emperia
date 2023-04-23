using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items.Grotto
{
	public class GrottoDirt : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Loam Block");
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.rare = ItemRarityID.White;
			Item.maxStack = Terraria.Item.CommonMaxStack;
			Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
			Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.TwilightDirt>();
		}
	}
}
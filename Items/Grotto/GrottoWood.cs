using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Grotto
{
	public class GrottoWood : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mistwood");
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
            Item.createTile = ModContent.TileType<Tiles.TFWood>();
		}
	}
}
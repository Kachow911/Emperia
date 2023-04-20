using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Placeable
{
	public class LCDScreen : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("LCD Screen");
			// Tooltip.SetDefault("Can be configured with an LCD wrench to display up to 4 colors at once");
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
            Item.createTile = ModContent.TileType<Tiles.LCDScreenTile>();
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(40);
			recipe.AddIngredient(ItemID.Glass, 40);
			recipe.AddIngredient(ItemID.Wire, 10);
			recipe.AddIngredient(ItemID.PinkGel, 3);
			recipe.AddIngredient(ItemID.Ruby);
			recipe.AddIngredient(ItemID.Emerald);
			recipe.AddIngredient(ItemID.Sapphire);

			recipe.AddTile(TileID.Anvils);

			recipe.Register();
		}
	}
}
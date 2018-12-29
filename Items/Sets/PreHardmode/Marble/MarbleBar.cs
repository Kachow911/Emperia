using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Marble
{
	public class MarbleBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marble Bar");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 1000;
			item.rare = 1;
		}

	public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 1);
			recipe.AddIngredient(ItemID.Marble, 10);
			recipe.AddTile(TileID.Furnaces);  
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LeadBar, 1);
			recipe.AddIngredient(ItemID.Marble, 10);
			recipe.AddTile(TileID.Furnaces);  
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
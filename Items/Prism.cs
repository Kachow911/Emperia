using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class Prism : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prism");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 1000;
			item.rare = 3;
		}

	public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Glass, 8);
			recipe.AddIngredient(ItemID.Diamond, 1);
			recipe.AddIngredient(ItemID.HellstoneBar, 1);
			recipe.AddTile(TileID.Furnaces);  
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
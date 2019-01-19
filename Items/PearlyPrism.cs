using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class PearlyPrism : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pearly Prism");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 1000;
			item.rare = 4;
		}

	public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "Prism", 1);
			recipe.AddIngredient(ItemID.CrystalShard, 20);
			recipe.AddIngredient(ItemID.SoulofLight, 10);
			recipe.AddTile(TileID.CrystalBall);  
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
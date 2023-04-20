using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class Prism : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Prism");
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 999;
			Item.value = 14000;
			Item.rare = 3;
		}

	public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Glass, 8);
			recipe.AddIngredient(ItemID.HellstoneBar, 2);
			recipe.AddIngredient(ItemID.Diamond, 1);
			recipe.AddTile(TileID.Furnaces);  
			recipe.Register();
			
		}
	}
}

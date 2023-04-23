using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class AetherBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aetherium Bar");
			// Tooltip.SetDefault("''Surprisingly light'");
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.value = 1000;
			Item.maxStack = Terraria.Item.CommonMaxStack;
			Item.rare = ItemRarityID.Green;
		}

	public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Aetherium", 3);
			recipe.AddTile(TileID.SkyMill);  
			recipe.Register();

			/*CreateRecipe(ItemID.LuckyHorseshoe);
			recipe.AddIngredient(null, "AetherBar", 5);
			recipe.AddIngredient(ItemID.Feather, 3);
			recipe.AddIngredient(ItemID.Cloud, 15);
			recipe.AddTile(TileID.SkyMill);  
			//recipe.SetResult(ItemID.LuckyHorseshoe);
			recipe.Register();*/
			
			/*recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.WorkBench, 1);
			recipe.AddIngredient(null, "Prism", 2);
			recipe.AddIngredient(ItemID.BottledWater, 1);
			recipe.AddTile(TileID.WorkBenches);  
			recipe.SetResult(ItemID.AlchemyTable);*/
			
		}
	}
}
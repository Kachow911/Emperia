using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class PearlyPrism : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pearly Prism");
		}
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 22;
						Item.value = 52000;
			Item.rare = ItemRarityID.LightRed;
		}

	public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Prism", 1);
			recipe.AddIngredient(ItemID.CrystalShard, 20);
			recipe.AddIngredient(ItemID.SoulofLight, 8);
			recipe.AddIngredient(ItemID.HallowedBar, 2);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();
			
		}
	}
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items
{
	public class WaxwingPotion : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Waxwing Potion");
            Tooltip.SetDefault("Increases wing speed by 25%, but decreases flight time by 10%");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = 1;
            item.value = 2250;
            item.buffType = (mod.BuffType("Waxwing"));
            item.buffTime = 18000;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "Icarusfish");
			recipe.AddIngredient(ItemID.Shiverthorn); 
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}

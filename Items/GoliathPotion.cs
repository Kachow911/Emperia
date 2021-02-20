using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items
{
	public class GoliathPotion : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goliath Potion");
            Tooltip.SetDefault("20% increased sword size");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 32;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = 1;
            item.value = 4000;
            item.buffType = (mod.BuffType("Goliath"));
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

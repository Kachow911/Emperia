using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items
{
	public class MushroomPlatter : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom Platter");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 18;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.UseSound = SoundID.Item2;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = 1;
            item.value = 1750;
        	item.healLife = 110;
            item.potion = true;
        }

		public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
			healValue = 110;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Mushroom);
			recipe.AddIngredient(ItemID.VileMushroom);
			recipe.AddIngredient(ItemID.GlowingMushroom); 
			recipe.AddTile(TileID.CookingPots);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
    public class MushroomPlatterCrim : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom Platter");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(mod.ItemType("MushroomPlatter"));
        }

		public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
			healValue = 110;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Mushroom);
			recipe.AddIngredient(ItemID.ViciousMushroom);
			recipe.AddIngredient(ItemID.GlowingMushroom); 
			recipe.AddTile(TileID.CookingPots);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}

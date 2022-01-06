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
            Item.width = 30;
            Item.height = 18;
            Item.useStyle = 2;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item2;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = 1;
            Item.value = 1750;
        	Item.healLife = 110;
            Item.potion = true;
        }

		public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
			healValue = 110;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Mushroom);
			recipe.AddIngredient(ItemID.VileMushroom);
			recipe.AddIngredient(ItemID.GlowingMushroom); 
			recipe.AddTile(TileID.CookingPots);
			recipe.Register();
			
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
            Item.CloneDefaults(ModContent.ItemType<MushroomPlatter>());
        }

		public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
			healValue = 110;
		}

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Mushroom);
			recipe.AddIngredient(ItemID.ViciousMushroom);
			recipe.AddIngredient(ItemID.GlowingMushroom); 
			recipe.AddTile(TileID.CookingPots);
			recipe.Register();
			
		}
    }
}

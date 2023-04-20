using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;


namespace Emperia.Items
{
	public class WaxwingPotion : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Waxwing Potion");
            // Tooltip.SetDefault("Increases wing speed by 25%, but decreases flight time by 10%");
            ItemID.Sets.DrinkParticleColors[Item.type] = new Color[2] { new Color(115, 222, 242), new Color(58, 114, 201) };
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.useStyle = 9;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
                        Item.consumable = true;
            Item.rare = 1;
            Item.value = 1000;
            Item.buffType = (ModContent.BuffType<Waxwing>());
            Item.buffTime = 21600;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(null, "Icarusfish");
			recipe.AddIngredient(ItemID.Shiverthorn); 
			recipe.AddTile(TileID.Bottles);
			recipe.Register();
			
		}
    }
}

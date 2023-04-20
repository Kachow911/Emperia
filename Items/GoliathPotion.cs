using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Emperia.Buffs;
using Microsoft.Xna.Framework;

namespace Emperia.Items
{
	public class GoliathPotion : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Goliath Potion");
            // Tooltip.SetDefault("Increases sword size by 20% and sword damage by 10%");
            ItemID.Sets.DrinkParticleColors[Item.type] = new Color[2] { new Color(255, 76, 45), new Color(169, 0, 36) };
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 32;
            Item.useStyle = 9;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = 1;
            Item.value = 1000;
            Item.buffType = (ModContent.BuffType<Goliath>());
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

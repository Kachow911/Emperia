using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items
{
	public class CactusJuice : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Juice");
            Tooltip.SetDefault("Gives swiftness\n1 minute duration");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = 2;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = 1;
            Item.value = 1750;
            Item.healLife = 50;
            Item.potion = true;

        }
        
		public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
			healValue = 50;
		}

        public override bool? UseItem(Player player)
        {
            player.AddBuff(BuffID.Swiftness, 3600);
            return true;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(ItemID.Cactus, 3);
			recipe.AddIngredient(ItemID.Mushroom); 
			recipe.AddTile(TileID.Bottles);
			recipe.Register();
			
		}
    }
}

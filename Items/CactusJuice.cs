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
            item.width = 20;
            item.height = 26;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = 1;
            item.value = 1750;
            item.healLife = 50;
            item.potion = true;

        }
        
		public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
			healValue = 50;
		}

        public override bool UseItem(Player player)
        {
            player.AddBuff(BuffID.Swiftness, 3600);
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(ItemID.Cactus, 3);
			recipe.AddIngredient(ItemID.Mushroom); 
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}

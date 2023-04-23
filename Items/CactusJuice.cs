using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace Emperia.Items
{
	public class CactusJuice : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cactus Juice");
            // Tooltip.SetDefault("Gives swiftness\n1 minute duration");
            ItemID.Sets.DrinkParticleColors[Item.type] = new Color[2] { new Color(159, 206, 29), new Color(104, 82, 61) };
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
                        Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = 750;
            Item.maxStack = Terraria.Item.CommonMaxStack;
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

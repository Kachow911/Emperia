using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items
{
	public class VileVial : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vile Vial");
            Tooltip.SetDefault("Inflicts poison briefly before healing\n10 second duration");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = 1;
            item.value = 1750;
        	item.healLife = 130;
            item.potion = true;
        }
        int healDisplayFix = 130;
		public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
			healValue = healDisplayFix;
            healDisplayFix = 130;
		}

        public override bool UseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (Main.expertMode) {
				player.AddBuff(BuffID.Poisoned, 300);
            }
            else {
				player.AddBuff(BuffID.Poisoned, 600);
            } //vanilla expert mode code automatically doubles poison time
            modPlayer.vileTimer = 600;
            healDisplayFix = 0;
            return false;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LesserHealingPotion, 2);
			recipe.AddIngredient(ItemID.VileMushroom);
			recipe.AddIngredient(ItemID.RottenChunk); 
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this, 2);
			recipe.AddRecipe();
		}
    }
}

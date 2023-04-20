using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace Emperia.Items
{
	public class VileVial : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vile Vial");
            // Tooltip.SetDefault("Inflicts poison briefly before healing\n10 second duration");
            ItemID.Sets.DrinkParticleColors[Item.type] = new Color[2] { new Color(151, 105, 214), new Color(87, 40, 152) };
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.useStyle = 9;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
                        Item.consumable = true;
            Item.rare = 1;
            Item.value = 1250;
        	Item.healLife = 130;
            Item.potion = true;
        }
        int healDisplayFix = 130;
		public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
			healValue = healDisplayFix;
            healDisplayFix = 130;
		}

        public override bool? UseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (Main.masterMode)
            {
                player.AddBuff(BuffID.Poisoned, 240);
            }
            else if (Main.expertMode) {
				player.AddBuff(BuffID.Poisoned, 300);
            }
            else {
				player.AddBuff(BuffID.Poisoned, 600);
            } //vanilla expert mode code automatically doubles poison time, master increases by 2.5x
            modPlayer.vileTimer = 600;
            healDisplayFix = 0;
            return true;
        }

        public override void AddRecipes()
		{
            CreateRecipe(2)
			    .AddIngredient(ItemID.LesserHealingPotion, 2)
                //.AddIngredient(ItemID.VileMushroom)
                .AddRecipeGroup("Emperia:EvilMushroom")
                .AddRecipeGroup("Emperia:EvilChunk")
                //.AddIngredient(ItemID.RottenChunk)
			    .AddTile(TileID.Bottles)
			    .Register();
			//Recipe recipe = CreateRecipe();
			//recipe.AddIngredient(ItemID.LesserHealingPotion, 2);
			//recipe.AddIngredient(ItemID.VileMushroom);
			//recipe.AddIngredient(ItemID.RottenChunk); 
			//recipe.AddTile(TileID.Bottles);
			//recipe.SetResult(this, 2);
			
		}
    }
}

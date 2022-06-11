using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class EnchantedGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Gauntlet");
			Tooltip.SetDefault("Sword damage increased by up to 12% at close range, 25% on large foes\nSword strikes may summon an enchanted blade to slash nearby enemies\nAllows you to swing swords while walking backwards");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 2;
            Item.value = 32000;
            Item.accessory = true;
            Item.GetGlobalItem<GItem>().gauntletPower = 0.25f;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().magicGauntlet = Item;
            player.GetModPlayer<MyPlayer>().wristBrace = true;
        }
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(null, "WristBrace", 1);
			recipe.AddIngredient(null, "AetherBar", 8);
			recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
    }
}

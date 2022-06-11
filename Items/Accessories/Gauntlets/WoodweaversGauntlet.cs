using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class WoodweaversGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Woodweaver's Gauntlet");
			Tooltip.SetDefault("Sword damage increased by up to 10% at close range, 20% on large foes\nSword strikes may fire a tiny splinter of wood at foes");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 1;
            Item.value = 1500;
            Item.accessory = true;
            Item.GetGlobalItem<GItem>().gauntletPower = 0.20f;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
            player.GetModPlayer<MyPlayer>().woodGauntlet = Item;
        }
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 20);
			recipe.AddRecipeGroup("IronBar", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
    }
}

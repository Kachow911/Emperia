using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class WristBrace : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wrist Brace");
			Tooltip.SetDefault("Allows you to swing swords while walking backwards\n'Also prevents carpal tunnel!'");
		}
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = 1;
            Item.value = 1750;
            Item.accessory = true;
            Item.defense = 1;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
            player.GetModPlayer<MyPlayer>().wristBrace = true;
        }
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Shackle);
            recipe.AddRecipeGroup("IronBar", 6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
    }
}

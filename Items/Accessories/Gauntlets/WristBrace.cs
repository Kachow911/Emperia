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
            item.width = 20;
            item.height = 20;
            item.rare = 1;
            item.value = 1750;
            item.accessory = true;
            item.defense = 1;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>().wristBrace = true;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Shackle);
            recipe.AddRecipeGroup("IronBar", 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

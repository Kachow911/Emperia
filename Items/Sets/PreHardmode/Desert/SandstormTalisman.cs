using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Desert
{
    public class SandstormTalisman : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Talisman");
			Tooltip.SetDefault("6% Increased critical strike chance");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 50, 0);
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.meleeCrit += 6;
			player.magicCrit += 6;
			player.rangedCrit += 6;
			player.thrownCrit += 6;
		
        }
		public override void AddRecipes()
		{
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "PolishedSandstone", 6);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
		}
    }
}

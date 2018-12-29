using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
{
    public class CharmOfLegends : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Charm of Legends");
			Tooltip.SetDefault("Healing potions heal for 25 additional HP\nWhile under 20% HP you will recieve boosts of healing\nProvides life regeneration and reduces the cooldown of healing potions");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 28;
            item.rare = 5;
            item.value = Item.sellPrice(0, 2, 50, 0);
            item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<MyPlayer>(mod).vitalityCrystal = true;
			player.GetModPlayer<MyPlayer>(mod).forbiddenOath = true;
			player.pStone = true;
			player.lifeRegen += 2;
		
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CharmofMyths, 1);
			recipe.AddIngredient(null, "VitalityCrystal", 1);
			recipe.AddIngredient(null, "ForbiddenOath", 1);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
	
		}
		
    }
}

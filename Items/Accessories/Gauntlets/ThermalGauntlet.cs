using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class ThermalGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Thermal Gauntlet");
			// Tooltip.SetDefault("Enemies explode into seeking frost bolts and fire bolts.\nAttacks inflict On Fire! and Frostburn.\nEnemies have a chance to be frozen on hit.\nTaking damage summons meteors from the sky");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.defense = 0;
            Item.accessory = true;
            Item.GetGlobalItem<GItem>().gauntletPower = 0.30f; //temp
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().thermalGauntlet = true;
        }
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "FrostGauntlet", 1);
			recipe.AddIngredient(null, "MeteorGauntlet", 1);
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            
           
        }
    }
}

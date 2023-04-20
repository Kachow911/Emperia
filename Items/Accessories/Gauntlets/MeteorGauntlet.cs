using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class MeteorGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Meteor Gauntlet");
			// Tooltip.SetDefault("Attacks inflict on fire and have a chance to rain small meteors on enemies");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 2;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.defense = 0;
            Item.accessory = true;
            Item.GetGlobalItem<GItem>().gauntletPower = 0.25f; ///temp
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().meteorGauntlet = true;
        }
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 15);
			recipe.AddIngredient(ItemID.LavaCharm, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
           
        }
    }
}

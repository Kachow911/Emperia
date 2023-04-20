using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class SpeedGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gauntlet of Speed");
			// Tooltip.SetDefault("15% increased movement speed\nSlightly increased knockback");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.defense = 3;
            Item.accessory = true;
            Item.GetGlobalItem<GItem>().gauntletPower = 0.25f; //temp
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().slightKnockback = true;
            player.moveSpeed += 0.15f;
        }
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(ItemID.Aglet, 1);
			recipe.AddIngredient(ItemID.Feather, 7);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
    }
}

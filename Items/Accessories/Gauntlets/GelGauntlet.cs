using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class GelGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gelatinous Gauntlet");
			Tooltip.SetDefault("Sword damage increased by up to 12% at close range, 25% on large foes\nSword strikes on knockback-immune foes bounce you back safely\nAllows you to swing swords while walking backwards");
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 1;
            Item.value = 3750;
            Item.accessory = true;
            //Item.alpha = 50;
            Item.GetGlobalItem<GItem>().isGauntlet = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
            if (player.GetModPlayer<MyPlayer>().gauntletBonus < 0.25f)
            {
                player.GetModPlayer<MyPlayer>().gauntletBonus = 0.25f;
                Item.GetGlobalItem<GItem>().inactiveGauntlet = false;
            }
            else Item.GetGlobalItem<GItem>().inactiveGauntlet = true;
            player.GetModPlayer<MyPlayer>().gelGauntlet = 0.9f;
            player.GetModPlayer<MyPlayer>().wristBrace = true;
        }
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(null, "WristBrace", 1);
            recipe.AddIngredient(ItemID.Gel, 40);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
    }
}

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
            item.width = 30;
            item.height = 28;
            item.rare = 2;
            item.value = 32000;
            item.accessory = true;
            item.GetGlobalItem<GItem>().isGauntlet = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MyPlayer>().gauntletBonus = 0.25f;
			player.GetModPlayer<MyPlayer>().magicGauntlet = true;
            player.GetModPlayer<MyPlayer>().wristBrace = true;
        }
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(null, "WristBrace", 1);
			recipe.AddIngredient(null, "AetherBar", 8);
			recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Emperia;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories.Gauntlets
{
    public class GraveGauntlet : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grave Gauntlet");
			Tooltip.SetDefault("Sword damage increased by up to 12% at close range, 25% on large foes\nSword strikes may summon a skull that halves the next contact damage taken\nAllows you to swing swords while walking backwards"); //allows you to briefly become invulnerable in close combat
		}
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.rare = 1;
            Item.value = 56000;
            Item.accessory = true;
            Item.GetGlobalItem<GItem>().gauntletPower = 0.25f;
        }
        public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
        {
			player.GetModPlayer<MyPlayer>().boneGauntlet = Item;
            player.GetModPlayer<MyPlayer>().wristBrace = true;
        }
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "WoodweaversGauntlet", 1);
            recipe.AddIngredient(null, "WristBrace", 1);
            recipe.AddIngredient(ItemID.Bone, 200);
			recipe.AddIngredient(ItemID.GoldenKey);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
    }
}

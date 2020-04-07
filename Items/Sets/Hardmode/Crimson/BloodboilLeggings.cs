using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.Hardmode.Crimson {
	[AutoloadEquip(EquipType.Legs)]
public class BloodboilLeggings : ModItem
{
    
    public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Rotfire Leggings");
			Tooltip.SetDefault("20% increased movement speed");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 75000;
        item.rare = 4;
        item.defense = 8;
    }

    public override void UpdateEquip(Player player)
    {
            player.moveSpeed += 0.20f;
    }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RottenChunk, 5);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
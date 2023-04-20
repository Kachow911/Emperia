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
			// Tooltip.SetDefault("20% increased movement speed");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 75000;
        Item.rare = 4;
        Item.defense = 8;
    }

    public override void UpdateEquip(Player player)
    {
            player.moveSpeed += 0.20f;
    }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddIngredient(ItemID.Ichor, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            
        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.Hardmode.Corrupt {
	[AutoloadEquip(EquipType.Body)]
public class RotfireChestplate : ModItem
{
    
	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rotfire Chestplate");
			// Tooltip.SetDefault("7% increased throwing damage");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 65000;
        Item.rare = 4;
        Item.defense = 9; //15
    }

    public override void UpdateEquip(Player player)
    {
            //player.thrownDamage *= 1.07f;
     }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RottenChunk, 5);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            
        }
    }
}
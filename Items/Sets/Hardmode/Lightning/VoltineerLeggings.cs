using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.Hardmode.Lightning {
	[AutoloadEquip(EquipType.Legs)]
public class VoltineerLeggings : ModItem
{
    
    public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Voltineer Leggings");
			// Tooltip.SetDefault("15% increased movement speed");
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
            player.moveSpeed += 0.15f;
        }

        /*public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AridScale", 4);
            recipe.AddIngredient(null, "DesertEye", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }*/
    }
}
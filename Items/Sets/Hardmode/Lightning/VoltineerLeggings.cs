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
			DisplayName.SetDefault("Voltineer Leggings");
			Tooltip.SetDefault("15% increased movement speed");
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
            player.moveSpeed += 0.15f;
        }

        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AridScale", 4);
            recipe.AddIngredient(null, "DesertEye", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }*/
    }
}
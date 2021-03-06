﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Frostleaf
{
	[AutoloadEquip(EquipType.Legs)]
public class FrostleafLegs : ModItem
{
    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frostleaf Stumps");
			Tooltip.SetDefault("6% increased movement speed");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 14;
        item.value = 63250;
        item.rare = 1;
        item.defense = 4;
    }

    public override void UpdateEquip(Player player)
    {
      player.moveSpeed += 0.06f;
    }

    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Frostleaf", 11); 
            recipe.AddIngredient(ItemID.BorealWood, 25); 			
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
    }
}}
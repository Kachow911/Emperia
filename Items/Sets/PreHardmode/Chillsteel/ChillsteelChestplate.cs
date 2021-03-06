﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Chillsteel {
	[AutoloadEquip(EquipType.Body)]
public class ChillsteelChestplate : ModItem
{
    
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chillsteel Chestplate");
			Tooltip.SetDefault("Increases armor penetration by 4");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 65000;
        item.rare = 2;
        item.defense = 7; //15
    }

    public override void UpdateEquip(Player player)
    {
            player.armorPenetration += 6;
        }

    public override void AddRecipes()
    {
        /*ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Seashell, 4);
            recipe.AddIngredient(ItemID.FishingSeaweed, 3); 			
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();*/
    }
}}
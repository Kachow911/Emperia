using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.FrousForest {
	[AutoloadEquip(EquipType.Body)]
public class TrueleafChest : ModItem
{
    
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Trueleaf Chestplate");
			Tooltip.SetDefault("4% increased endurance");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 65000;
        item.rare = 2;
        item.defense = 6; //15
    }

    public override void UpdateEquip(Player player)
    {
            player.endurance += 0.04f;
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
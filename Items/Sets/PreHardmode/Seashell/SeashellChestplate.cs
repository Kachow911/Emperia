using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Seashell {
	[AutoloadEquip(EquipType.Body)]
public class SeashellChestplate : ModItem
{
    
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seashell Breastplate");
			Tooltip.SetDefault("+3 increased defense when in liquid \n +1 Defense when not in liquid");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 65000;
        item.rare = 1;
        item.defense = 4; //15
    }

    public override void UpdateEquip(Player player)
    {
       if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
    	{
			player.statDefense += 3;
		}
		else{
			player.statDefense += 1;
		}
    }

    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Seashell, 4);
            recipe.AddIngredient(ItemID.FishingSeaweed, 3); 			
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
    }
}}
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Seashell {
	[AutoloadEquip(EquipType.Legs)]
public class SeashellLeggings : ModItem
{
    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seashell Greaves");
			Tooltip.SetDefault("6% increased movement speed when in liquid \n 2% increased movement speed otherwise");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 57500;
        item.rare = 1;
        item.defense = 2; //15
    }

    public override void UpdateEquip(Player player)
    {
		if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
    	{
        player.moveSpeed += 0.06f;
		}
		else
		{
			 player.moveSpeed += 0.02f;
		}
    }

    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Seashell, 3);
            recipe.AddIngredient(ItemID.FishingSeaweed, 2); 			
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
    }
}}
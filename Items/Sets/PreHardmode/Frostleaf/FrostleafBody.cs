using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Frostleaf {
	[AutoloadEquip(EquipType.Body)]
public class FrostleafBody : ModItem
{
    
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frostleaf Barkmail");
			Tooltip.SetDefault("Grants immunity to Chilled");
		}
    public override void SetDefaults()
    {
        item.width = 32;
        item.height = 20;
        item.value = 65000;
        item.rare = 1;
        item.defense = 5;
    }

    public override void UpdateEquip(Player player)
    {
    //player.handWarmer = true;
    player.moveSpeed += 0.06f;
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
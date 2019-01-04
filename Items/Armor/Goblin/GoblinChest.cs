using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Armor.Goblin {
	[AutoloadEquip(EquipType.Body)]
public class GoblinChest : ModItem
{
    
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Elder Goblin's Chestpiece");
			Tooltip.SetDefault("3% increased melee and move speed");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 65000;
        item.rare = 3;
        item.defense = 7; //15
    }

    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.03f;
		player.meleeSpeed += 0.03f;
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
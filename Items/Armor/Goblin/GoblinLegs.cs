using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Armor.Goblin {
	[AutoloadEquip(EquipType.Legs)]
public class GoblinLegs : ModItem
{
    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Elder Goblin's Brogans");
			Tooltip.SetDefault("3% increased damage");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 57500;
        item.rare = 3;
        item.defense = 5; //15
    }

    public override void UpdateEquip(Player player)
    {
		player.thrownDamage += 0.03f;
        player.meleeDamage += 0.03f;
        player.minionDamage += 0.03f;
        player.magicDamage += 0.03f;
        player.rangedDamage += 0.03f;
    }

    public override void AddRecipes()
    {
       /* ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Seashell, 3);
            recipe.AddIngredient(ItemID.FishingSeaweed, 2); 			
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();*/
    }
}}
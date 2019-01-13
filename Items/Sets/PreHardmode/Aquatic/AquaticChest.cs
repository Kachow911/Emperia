using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Aquatic {
	[AutoloadEquip(EquipType.Body)]
public class AquaticChest : ModItem
{
    
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Torrential Chestpiece");
			Tooltip.SetDefault("4% increased endurance and damage");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 65000;
        item.rare = 3;
        item.defense = 9; //15
    }

    public override void UpdateEquip(Player player)
    {
            player.meleeDamage *= 1.04f;
            player.thrownDamage *= 1.04f;
            player.rangedDamage *= 1.04f;
            player.magicDamage *= 1.04f;
            player.minionDamage *= 1.04f;
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
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Aquatic
{
	[AutoloadEquip(EquipType.Legs)]
public class AquaticLegs : ModItem
{
    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Torrential Greaves");
			Tooltip.SetDefault("6% increased movement speed and 3% increased endurance");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 57500;
        item.rare = 3;
        item.defense = 7; //15
    }

    public override void UpdateEquip(Player player)
    {
            player.moveSpeed += 0.06f;
            player.endurance += 0.03f;
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
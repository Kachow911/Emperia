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
			// DisplayName.SetDefault("Trueleaf Chestplate");
			// Tooltip.SetDefault("4% increased endurance");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 65000;
        Item.rare = ItemRarityID.Green;
        Item.defense = 6; //15
    }

    public override void UpdateEquip(Player player)
    {
            player.endurance += 0.04f;
        }

    public override void AddRecipes()
    {
        /*Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Seashell, 4);
            recipe.AddIngredient(ItemID.FishingSeaweed, 3); 			
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.Register();
            */
    }
}}
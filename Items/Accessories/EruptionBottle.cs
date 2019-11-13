using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
 {
public class EruptionBottle : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eruption in a Bottle");
			Tooltip.SetDefault("Gives the player an improved double jump");
		}
	public override void SetDefaults()
	{
		item.melee = true;
		item.width = 38;
		item.height = 44;
		item.value = 180000;
		item.expert = true;
		item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
	
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.eruptionBottle = true;
		
	}
	 public override void AddRecipes()  //How to craft this sword
     {
		ModRecipe recipe = new ModRecipe(mod);
		recipe.AddIngredient(ItemID.CloudinaBottle, 1);
		 recipe.AddIngredient(null, "MoltenChunk", 5); 
        recipe.AddTile(TileID.Anvils); 			
        recipe.SetResult(this);
        recipe.AddRecipe(); 

     }

}}

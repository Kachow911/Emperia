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
			// DisplayName.SetDefault("Eruption in a Bottle");
			// Tooltip.SetDefault("Gives the player an improved double jump");
		}
	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Melee;
		Item.width = 38;
		Item.height = 44;
		Item.value = 180000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
	{
	
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.eruptionBottle = true;
		
	}
	 public override void AddRecipes()  //How to craft this sword
     {
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.CloudinaBottle, 1);
		 recipe.AddIngredient(null, "MoltenChunk", 5); 
        recipe.AddTile(TileID.Anvils); 			
        recipe.Register();
         

     }

}}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
 {
public class RetinalBane : ModItem
{
	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Retinal Bane");
			// Tooltip.SetDefault("Allows the player to dash into enemies, inflicting cursed flames\nDouble Tap a direction to dash");
		}
	public override void SetDefaults()
	{
		Item.damage = 60;
		Item.DamageType = DamageClass.Melee;
		Item.defense = 4;
		Item.width = 30;
		Item.height = 38;
		Item.value = 180000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
	{
	
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.cursedDash = true;
		
	}
	 public override void AddRecipes()  //How to craft this sword
     {
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.EoCShield, 1);
		recipe.AddIngredient(ItemID.MechanicalWheelPiece, 1);
        recipe.AddTile(TileID.MythrilAnvil); 			
        recipe.Register();
         

     }

}}

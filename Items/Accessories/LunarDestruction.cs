using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Accessories
 {
public class LunarDestruction : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Destruction");
			Tooltip.SetDefault("Allows the player to dash into enemies, inflicting extreme\nDouble Tap a direction to dash");
		}
	public override void SetDefaults()
	{
		item.damage = 60;
		item.melee = true;
		item.defense = 10;
		item.width = 38;
		item.height = 44;
		item.value = 1800000;
		item.expert = true;
		item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
	
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(mod);
		modPlayer.lunarDash = true;
		
	}
	 public override void AddRecipes()  //How to craft this sword
     {
		ModRecipe recipe = new ModRecipe(mod);
		recipe.AddIngredient(null, "RetinalBane", 1);
		recipe.AddIngredient(ItemID.FragmentSolar, 5);
		recipe.AddIngredient(ItemID.FragmentVortex, 5);
		recipe.AddIngredient(ItemID.FragmentNebula, 5);
		recipe.AddIngredient(ItemID.FragmentStardust, 5);
		recipe.AddIngredient(ItemID.LunarBar, 10);
        recipe.AddTile(TileID.LunarCraftingStation); 			
        recipe.SetResult(this);
        recipe.AddRecipe(); 

     }

}}
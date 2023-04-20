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
			// DisplayName.SetDefault("Lunar Destruction");
			// Tooltip.SetDefault("Allows the player to dash into enemies, inflicting extreme\nDouble Tap a direction to dash");
		}
	public override void SetDefaults()
	{
		Item.damage = 60;
		Item.DamageType = DamageClass.Melee;
		Item.defense = 10;
		Item.width = 38;
		Item.height = 44;
		Item.value = 1800000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisibleAccessory)
	{
	
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.lunarDash = true;
		
	}
	 public override void AddRecipes()  //How to craft this sword
     {
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "RetinalBane", 1);
		recipe.AddIngredient(ItemID.FragmentSolar, 5);
		recipe.AddIngredient(ItemID.FragmentVortex, 5);
		recipe.AddIngredient(ItemID.FragmentNebula, 5);
		recipe.AddIngredient(ItemID.FragmentStardust, 5);
		recipe.AddIngredient(ItemID.LunarBar, 10);
        recipe.AddTile(TileID.LunarCraftingStation); 			
        recipe.Register();
         

     }

}}

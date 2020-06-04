using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Frostleaf{
	[AutoloadEquip(EquipType.Head)]
public class FrostleafHead : ModItem
{
	 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frostleaf Hood");
			Tooltip.SetDefault("4% increased damage");
		}
    public override void SetDefaults()
    {
        item.width = 24;
        item.height = 22;
        item.value = 51750;
        item.rare = 1;
        item.defense = 3;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == mod.ItemType("FrostleafBody") && legs.type == mod.ItemType("FrostleafLegs");
    }
    
  

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Eating mushrooms briefly increases damage and movement speed";
		player.GetModPlayer<MyPlayer>().frostleafSet = true;
    }
    

    public override void UpdateEquip(Player player)
    {
        player.allDamage += 0.04f;
    }
    
    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Frostleaf", 9); 
            recipe.AddIngredient(ItemID.BorealWood, 20); 			
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
    }
}}

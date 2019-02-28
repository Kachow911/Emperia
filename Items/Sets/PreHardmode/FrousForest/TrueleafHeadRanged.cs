using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.FrousForest {
	[AutoloadEquip(EquipType.Head)]
public class TrueleafHeadRanged : ModItem
{
	 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Trueleaf Faceguard");
			Tooltip.SetDefault("Increases ranged damage by 3%");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 50000;
        item.rare = 2;
        item.defense = 4; //15
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == mod.ItemType("TrueleafChest") && legs.type == mod.ItemType("TrueleafStumps");
    }
    
  

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Ranged attacks sometimes send out additional damaging leaves";
		player.GetModPlayer<MyPlayer>(mod).forestSetRanged = true;
    }
    
    public override void UpdateEquip(Player player)
    {
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
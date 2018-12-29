using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Seashell {
	[AutoloadEquip(EquipType.Head)]
public class SeashellVisor : ModItem
{
	 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seashell Visor");
			Tooltip.SetDefault("4% increased damage when in liquid \n 2% increased damage otherwise");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 50000;
        item.rare = 1;
        item.defense = 3; //15
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == mod.ItemType("SeashellChestplate") && legs.type == mod.ItemType("SeashellLeggings");
    }
    
  

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Stats greatly increased when in water: +75 life, +5% movement speed, + 5 defense and +5% damage reduction";
		if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
    	{
        player.endurance += 0.05f;
		player.moveSpeed += 0.06f;
		player.statLifeMax2 += 75;
		player.statDefense += 5;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
		if(player.wet == true || player.honeyWet == true || player.lavaWet == true)
    	{
			player.meleeDamage *= 1.04f;
			player.thrownDamage *= 1.04f;
			player.rangedDamage *= 1.04f;
			player.magicDamage *= 1.04f;
			player.minionDamage *= 1.04f;
		}
		else
		{
			player.meleeDamage *= 1.02f;
			player.thrownDamage *= 1.02f;
			player.rangedDamage *= 1.02f;
			player.magicDamage *= 1.02f;
			player.minionDamage *= 1.02f;
		}
    }
    
    public override void AddRecipes()
    {
       ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Seashell, 3);
            recipe.AddIngredient(ItemID.FishingSeaweed, 2); 			
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
    }
}}
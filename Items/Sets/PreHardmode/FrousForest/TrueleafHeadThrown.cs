using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.FrousForest {
	[AutoloadEquip(EquipType.Head)]
public class TrueleafHeadThrown : ModItem
{
	 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Trueleaf Crown");
			Tooltip.SetDefault("Increases throwing damage by 3%");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 50000;
        Item.rare = 2;
        Item.defense = 4; //15
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<TrueleafChest>() && legs.type == ModContent.ItemType<TrueleafStumps>();
    }
    
  

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "33% chance to not consume throwing items\nThrowing weapons have a chance to ignore enemy defense";
		player.GetModPlayer<MyPlayer>().forestSetThrown = true;
    }
    
    public override void UpdateEquip(Player player)
    {
        //player.thrownDamage += 0.03f;
			
    }
    
    public override void AddRecipes()
    {
      /* Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Seashell, 3);
            recipe.AddIngredient(ItemID.FishingSeaweed, 2); 			
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.Register();
            */
    }
}}

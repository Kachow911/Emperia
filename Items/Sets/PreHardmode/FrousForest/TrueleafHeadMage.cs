using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.FrousForest {
	[AutoloadEquip(EquipType.Head)]
public class TrueleafHeadMage : ModItem
{
	 public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Trueleaf Vizor");
			// Tooltip.SetDefault("Increases magic damage by 3%");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 50000;
        Item.rare = ItemRarityID.Green;
        Item.defense = 4; //15
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<TrueleafChest>() && legs.type == ModContent.ItemType<TrueleafStumps>();
    }
    
  

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Hitiing an enemy with a magic weapon has a chance to send you into a primal rage\n Primal rage yields greatly increased mana regeneration";
		player.GetModPlayer<MyPlayer>().forestSetMage = true;
    }
    
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += 0.03f;
			
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

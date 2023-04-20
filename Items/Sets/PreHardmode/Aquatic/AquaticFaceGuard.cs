using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Aquatic {
	[AutoloadEquip(EquipType.Head)]
public class AquaticFaceGuard : ModItem
{
	 public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Torrential Faceguard");
			// Tooltip.SetDefault("Increases armor penetration by 6");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 50000;
        Item.rare = ItemRarityID.Orange;
        Item.defense = 7; //15
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<AquaticChest>() && legs.type == ModContent.ItemType<AquaticLegs>();
    }
    
  

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Aquatic Plants grow on tiles near you\nStanding next to these plants increases damage and life regen\nPlants explode after 20 seconds";
		 //player.GetModPlayer<MyPlayer>().aquaticSet = true;
         player.GetModPlayer<MyPlayer>().aquaticSet = Item;
    }
    
    public override void UpdateEquip(Player player)
    {
            player.GetArmorPenetration(DamageClass.Melee) += 6;
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

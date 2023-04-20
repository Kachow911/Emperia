using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Armor.Goblin {
	[AutoloadEquip(EquipType.Head)]
public class GoblinHelm : ModItem
{
	 public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Elder Goblin's Heaume");
			// Tooltip.SetDefault("4% Increased critical strike chance");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 50000;
        Item.rare = 3;
        Item.defense = 5; //15
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<GoblinChest>() && legs.type == ModContent.ItemType<GoblinLegs>();
    }
    
  

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Striking an enemy with a melee weapon will grant you the 'Goblins celerity' buff for a short time\nGoblins Celerity increases your movement and melee speed";
		 player.GetModPlayer<MyPlayer>().goblinSet = true;
    }
    
    public override void UpdateEquip(Player player)
    {
			player.GetCritChance(DamageClass.Generic) += 4;
    }
    
    public override void AddRecipes()
    {
         Recipe recipe = CreateRecipe();
         recipe.AddIngredient(null, "GiantPlating", 4);
         recipe.AddIngredient(ItemID.IronBar, 8); 			
         recipe.AddTile(TileID.Anvils); 			
         recipe.Register();
         
    }
}}

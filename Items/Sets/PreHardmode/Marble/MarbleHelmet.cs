using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Marble {
	[AutoloadEquip(EquipType.Head)]
public class MarbleHelmet : ModItem
{
 
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marble Helmet");
			Tooltip.SetDefault("+2% Ranged and Magic damage");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 50000;
        item.rare = 2;
        item.defense = 5; //15
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == mod.ItemType("MarbleBreastplate") && legs.type == mod.ItemType("MarbleGrieves");
    }
    
  

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "6% Critical Strike CHance";
        player.meleeCrit += 6;
		player.magicCrit += 6;
		player.rangedCrit += 6;
		player.thrownCrit += 6;
    }
    
    public override void UpdateEquip(Player player)
    {
        player.rangedDamage *= 1.02f;
        player.magicDamage *= 1.02f;
    }
    
    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "MarbleBar", 8);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
}}
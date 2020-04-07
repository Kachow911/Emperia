using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Chillsteel {
	[AutoloadEquip(EquipType.Head)]
public class ChillsteelHelmet : ModItem
{
    
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chillsteel Helmet");
			Tooltip.SetDefault("4% increased damage");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 140000;
        item.rare = 2;
        item.defense = 4;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == mod.ItemType("ChillsteelChestplate") && legs.type == mod.ItemType("ChillsteelGreaves");
    }
    
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Taking damage will cause ice bolts to damage enemies";
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.chillsteelSet = true;
		
    }
    
    public override void UpdateEquip(Player player)
    {
            player.meleeDamage *= 1.04f;
            player.thrownDamage *= 1.04f;
            player.rangedDamage *= 1.04f;
            player.magicDamage *= 1.04f;
            player.minionDamage *= 1.04f;
        }

        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AridScale", 4);
            recipe.AddIngredient(null, "DesertEye", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }*/
    }
}
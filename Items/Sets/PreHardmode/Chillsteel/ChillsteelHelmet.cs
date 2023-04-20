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
			// DisplayName.SetDefault("Chillsteel Helmet");
			// Tooltip.SetDefault("6% increased ranged damage");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 140000;
        Item.rare = 2;
        Item.defense = 4;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<ChillsteelChestplate>() && legs.type == ModContent.ItemType<ChillsteelGreaves>();
    }
    
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Ranged attacks inflict frostburn";
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.chillsteelSet = true;
		
    }
    
    public override void UpdateEquip(Player player)
    {
            player.GetDamage(DamageClass.Ranged) *= 1.04f;
    }

        /*public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AridScale", 4);
            recipe.AddIngredient(null, "DesertEye", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }*/
    }
}
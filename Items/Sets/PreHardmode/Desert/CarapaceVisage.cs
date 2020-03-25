using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Desert {
	[AutoloadEquip(EquipType.Head)]
public class CarapaceVisage : ModItem
{
    
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Carapace Visage");
			Tooltip.SetDefault("5% increased endurance");
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
        return body.type == mod.ItemType("CarapaceMantle") && legs.type == mod.ItemType("CarapaceBoots");
    }
    
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Allows you to ground pound, damaging nearby enemies";
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.carapaceSet = true;
		
    }
    
    public override void UpdateEquip(Player player)
    {
            player.endurance += 0.05f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AridScale", 4);
            recipe.AddIngredient(null, "DesertEye", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
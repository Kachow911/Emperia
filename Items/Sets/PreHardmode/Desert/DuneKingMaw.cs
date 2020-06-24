using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Desert {
	[AutoloadEquip(EquipType.Head)]
public class DuneKingMaw : ModItem
{
    
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dune King's Maw");
			Tooltip.SetDefault("4% increased endurance");
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
        return body.type == mod.ItemType("DuneKingMantle") && legs.type == mod.ItemType("DuneKingCuisses");
    }
    
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Allows you to ground pound, stirring up a sandstone spike to impale enemies";
		MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
		modPlayer.carapaceSet = true;
		ArmorIDs.Face.Sets.PreventHairDraw[face] = false;
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
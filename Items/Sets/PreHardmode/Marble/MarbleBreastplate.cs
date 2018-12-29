using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Marble {
	[AutoloadEquip(EquipType.Body)]
public class MarbleBreastplate : ModItem
{
  
	 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marble Breastplate");
			Tooltip.SetDefault("+3% damage reduction");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 65000;
        item.rare = 2;
        item.defense = 6; //15
    }

    public override void UpdateEquip(Player player)
    {
        player.endurance *= 1.03f;
    }

    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "MarbleBar", 12);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
}}
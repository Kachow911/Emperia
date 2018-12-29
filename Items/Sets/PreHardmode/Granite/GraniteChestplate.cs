using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Granite {
	[AutoloadEquip(EquipType.Body)]
public class GraniteChestplate : ModItem
{
   
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Breastplate");
			Tooltip.SetDefault("+3% Critical Strike Chance");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 65000;
        item.rare = 2;
        item.defense = 7; //15
    }

    public override void UpdateEquip(Player player)
    {
        player.meleeCrit += 3;
			player.magicCrit += 3;
			player.rangedCrit += 3;
			player.thrownCrit += 3;
    }

    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 12);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
}}
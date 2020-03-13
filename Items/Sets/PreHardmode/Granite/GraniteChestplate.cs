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
			Tooltip.SetDefault("Increases your max number of minions\n2% increased critical strike chance");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 210000;
        item.rare = 2;
        item.defense = 8;
    }

    public override void UpdateEquip(Player player)
    {
        player.maxMinions += 1;
        player.meleeCrit += 2;
		player.magicCrit += 2;
		player.rangedCrit += 2;
		player.thrownCrit += 2;
    }

    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 12);
        recipe.AddRecipeGroup("Emperia:EvilHide", 9);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
}}
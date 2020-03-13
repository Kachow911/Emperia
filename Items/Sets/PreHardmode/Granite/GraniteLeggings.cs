using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Granite {
	[AutoloadEquip(EquipType.Legs)]
public class GraniteLeggings : ModItem
{
    
    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Greaves");
			Tooltip.SetDefault("Grants immunity to knockback");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 175000;
        item.rare = 2;
        item.defense = 7;
    }

    public override void UpdateEquip(Player player)
    {
        player.noKnockback = true;
    }

    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 10);
        recipe.AddRecipeGroup("Emperia:EvilHide", 7);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
}}
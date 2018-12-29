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
			Tooltip.SetDefault("+6% movement speed");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 57500;
        item.rare = 3;
        item.defense = 5; //15
    }

    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.06f;
    }

    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 10);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
}}
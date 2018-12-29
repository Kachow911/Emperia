using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Marble {
	[AutoloadEquip(EquipType.Legs)]
public class MarbleGrieves : ModItem
{

    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marble Greaves");
			Tooltip.SetDefault("6% increased movement speed");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 57500;
        item.rare = 3;
        item.defense = 4; //15
    }

    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.06f;
    }

    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "MarbleBar", 7);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
}}
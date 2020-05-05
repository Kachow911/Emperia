using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Desert {
	[AutoloadEquip(EquipType.Legs)]
public class DuneKingCuisses : ModItem
{
    
    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dune King's Cuisses");
			Tooltip.SetDefault("5% increased endurance");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 75000;
        item.rare = 2;
        item.defense = 6;
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
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
			// DisplayName.SetDefault("Dune King's Cuisses");
			// Tooltip.SetDefault("5% increased endurance");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 75000;
        Item.rare = ItemRarityID.Green;
        Item.defense = 6;
    }

    public override void UpdateEquip(Player player)
    {
            player.endurance += 0.05f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AridScale", 4);
            recipe.AddIngredient(null, "DesertEye", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
    }
}
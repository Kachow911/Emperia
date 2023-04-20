using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Desert {
	[AutoloadEquip(EquipType.Body)]
public class DuneKingMantle : ModItem
{
   
	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dune King's Plating");
			// Tooltip.SetDefault("4% increased damage");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 210000;
        Item.rare = 2;
        Item.defense = 6;
    }

    public override void UpdateEquip(Player player)
    {
            player.GetDamage(DamageClass.Melee) *= 1.04f;
            //player.thrownDamage *= 1.04f;
            player.GetDamage(DamageClass.Ranged) *= 1.04f;
            player.GetDamage(DamageClass.Magic) *= 1.04f;
            player.GetDamage(DamageClass.Summon) *= 1.04f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AridScale", 5);
            recipe.AddIngredient(null, "DesertEye", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
    }
}
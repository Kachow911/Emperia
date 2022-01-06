using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Chillsteel {
	[AutoloadEquip(EquipType.Legs)]
public class ChillsteelGreaves : ModItem
{
    
    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chillsteel Greaves");
			Tooltip.SetDefault("4% increased ranged damage");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 75000;
        Item.rare = 2;
        Item.defense = 5;
    }

    public override void UpdateEquip(Player player)
    {
            player.GetDamage(DamageClass.Ranged) *= 1.04f;
    }

        /*public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AridScale", 4);
            recipe.AddIngredient(null, "DesertEye", 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }*/
    }
}
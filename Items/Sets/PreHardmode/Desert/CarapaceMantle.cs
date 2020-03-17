using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Desert {
	[AutoloadEquip(EquipType.Body)]
public class CarapaceMantle : ModItem
{
   
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Carapace Mantle");
			Tooltip.SetDefault("4% increased damage");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 210000;
        item.rare = 2;
        item.defense = 6;
    }

    public override void UpdateEquip(Player player)
    {
            player.meleeDamage *= 1.04f;
            player.thrownDamage *= 1.04f;
            player.rangedDamage *= 1.04f;
            player.magicDamage *= 1.04f;
            player.minionDamage *= 1.04f;
        }

    public override void AddRecipes()
    {
       /* ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 12);
        recipe.AddRecipeGroup("Emperia:EvilHide", 9);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();*/
    }
}}
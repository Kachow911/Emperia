using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Frostleaf {
	[AutoloadEquip(EquipType.Body)]
public class FrostleafBody : ModItem
{
    
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frostleaf Barkmail");
			Tooltip.SetDefault("Grants immunity to Chilled");
		}
    public override void SetDefaults()
    {
        item.width = 32;
        item.height = 20;
        item.value = 74750;
        item.rare = 1;
        item.defense = 4;
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[46] = true;
    }

    public override void AddRecipes()
    {
      ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Frostleaf", 13); 
            recipe.AddIngredient(ItemID.BorealWood, 30); 			
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
    }
}}
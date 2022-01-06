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
        Item.width = 32;
        Item.height = 20;
        Item.value = 74750;
        Item.rare = 1;
        Item.defense = 4;
    }

    public override void UpdateEquip(Player player)
    {
        player.buffImmune[46] = true;
    }

    public override void AddRecipes()
    {
      Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "Frostleaf", 13); 
            recipe.AddIngredient(ItemID.BorealWood, 30); 			
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
    }
}}
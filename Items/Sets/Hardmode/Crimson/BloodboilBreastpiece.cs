using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.Hardmode.Crimson {
	[AutoloadEquip(EquipType.Body)]
public class BloodboilBreastpiece : ModItem
{
    
	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bloodboil Breastpiece");
			// Tooltip.SetDefault("7% increased magic damage");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 65000;
        Item.rare = 4;
        Item.defense = 11; //15
    }

    public override void UpdateEquip(Player player)
    {
            player.GetDamage(DamageClass.Magic) *= 1.07f;
     }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddIngredient(ItemID.Ichor, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            
        }
    }
}
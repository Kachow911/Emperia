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
			DisplayName.SetDefault("Bloodboil Breastpiece");
			Tooltip.SetDefault("7% increased magic damage");
		}
    public override void SetDefaults()
    {
        item.width = 18;
        item.height = 18;
        item.value = 65000;
        item.rare = 4;
        item.defense = 11; //15
    }

    public override void UpdateEquip(Player player)
    {
            player.magicDamage *= 1.07f;
     }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RottenChunk, 5);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
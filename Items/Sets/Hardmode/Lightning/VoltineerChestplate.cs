using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.Hardmode.Lightning {
	[AutoloadEquip(EquipType.Body)]
public class VoltineerChestplate : ModItem
{
    
	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Voltineer Chestplate");
			// Tooltip.SetDefault("7% increased damage\n5% increased endurance");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 65000;
        Item.rare = 4;
        Item.defense = 15; //15
    }

    public override void UpdateEquip(Player player)
    {
            player.endurance += 0.05f;
            player.GetDamage(DamageClass.Melee) *= 1.07f;
            //player.thrownDamage *= 1.07f;
            player.GetDamage(DamageClass.Ranged) *= 1.07f;
            player.GetDamage(DamageClass.Magic) *= 1.07f;
            player.GetDamage(DamageClass.Summon) *= 1.07f;
        }

        /*public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RottenChunk, 5);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            
        }*/
    }
}
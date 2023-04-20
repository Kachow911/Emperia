using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Aquatic {
	[AutoloadEquip(EquipType.Body)]
public class AquaticChest : ModItem
{
    
	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Torrential Chestpiece");
			// Tooltip.SetDefault("4% increased endurance and damage");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 65000;
        Item.rare = ItemRarityID.Orange;
        Item.defense = 9; //15
    }

    public override void UpdateEquip(Player player)
    {
            player.GetDamage(DamageClass.Melee) *= 1.04f;
            //player.thrownDamage *= 1.04f;
            player.GetDamage(DamageClass.Ranged) *= 1.04f;
            player.GetDamage(DamageClass.Magic) *= 1.04f;
            player.GetDamage(DamageClass.Summon) *= 1.04f;
            player.endurance += 0.04f;
        }

    public override void AddRecipes()
    {
        /*Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Seashell, 4);
            recipe.AddIngredient(ItemID.FishingSeaweed, 3); 			
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.Register();
            */
    }
}}
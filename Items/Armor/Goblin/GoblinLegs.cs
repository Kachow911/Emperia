using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Armor.Goblin {
	[AutoloadEquip(EquipType.Legs)]
public class GoblinLegs : ModItem
{
    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Elder Goblin's Brogans");
			Tooltip.SetDefault("3% increased damage");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 57500;
        Item.rare = 3;
        Item.defense = 5; //15
    }

    public override void UpdateEquip(Player player)
    {
		//player.thrownDamage += 0.03f;
        player.GetDamage(DamageClass.Melee) += 0.03f;
        player.GetDamage(DamageClass.Summon) += 0.03f;
        player.GetDamage(DamageClass.Magic) += 0.03f;
        player.GetDamage(DamageClass.Ranged) += 0.03f;
    }

    public override void AddRecipes()
    {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "GiantPlating", 4);
            recipe.AddIngredient(ItemID.IronBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
}}
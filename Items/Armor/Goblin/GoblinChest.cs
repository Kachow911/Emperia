using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Armor.Goblin {
	[AutoloadEquip(EquipType.Body)]
public class GoblinChest : ModItem
{
    
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Elder Goblin's Chestpiece");
			Tooltip.SetDefault("3% increased melee and move speed");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 65000;
        Item.rare = 3;
        Item.defense = 7; //15
    }

    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.03f;
		player.meleeSpeed += 0.03f;
    }

    public override void AddRecipes()
    {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "GiantPlating", 6);
            recipe.AddIngredient(ItemID.IronBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
}}
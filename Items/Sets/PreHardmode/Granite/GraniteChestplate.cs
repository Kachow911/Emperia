using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Granite {
	[AutoloadEquip(EquipType.Body)]
public class GraniteChestplate : ModItem
{
   
	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Granite Breastplate");
			// Tooltip.SetDefault("Increases your max number of minions\n2% increased critical strike chance");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 210000;
        Item.rare = ItemRarityID.Green;
        Item.defense = 7;
    }

    public override void UpdateEquip(Player player)
    {
        player.maxMinions += 1;
		player.GetCritChance(DamageClass.Generic) += 2;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GraniteBar", 12);
        recipe.AddRecipeGroup("Emperia:EvilHide", 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        
    }
}}
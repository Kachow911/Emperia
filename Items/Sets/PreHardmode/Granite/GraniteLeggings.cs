using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items.Sets.PreHardmode.Granite {
	[AutoloadEquip(EquipType.Legs)]
public class GraniteLeggings : ModItem
{
    
    public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Granite Greaves");
			// Tooltip.SetDefault("Grants immunity to knockback");
		}
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 175000;
        Item.rare = 2;
        Item.defense = 6;
    }

    public override void UpdateEquip(Player player)
    {
        player.noKnockback = true;
        //player.npcTypeNoAggro[NPCID.GraniteFlyer] = true;
        //player.npcTypeNoAggro[NPCID.GraniteGolem] = true;
    }

        public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GraniteBar", 10);
        recipe.AddRecipeGroup("Emperia:EvilHide", 7);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        
    }
}}
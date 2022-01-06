using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Seashell {
public class SeashellHamaxe : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seashell Hamaxe");
		}
    public override void SetDefaults()
    {
        Item.damage = 7;
        Item.DamageType = DamageClass.Melee;
        Item.width = 46;
        Item.height = 46;
        Item.useTime = 26;
        Item.useAnimation = 26;
        Item.useTurn = true;
        Item.axe = 11;
		Item.hammer = 55;
        Item.useStyle = 1;
        Item.knockBack = 2f;
        Item.value = 1770;
        Item.rare = 2;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
       Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Seashell, 2);
            recipe.AddIngredient(ItemID.Coral, 2);
            recipe.AddIngredient(null, "SeaCrystal", 1);  			
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
    }
}}

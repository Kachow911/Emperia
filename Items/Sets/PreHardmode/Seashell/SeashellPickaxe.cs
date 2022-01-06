using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Seashell {
public class SeashellPickaxe : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seashell Pickaxe");
		}
    public override void SetDefaults()
    {
        Item.damage = 5;
        Item.DamageType = DamageClass.Melee;
        Item.width = 46;
        Item.height = 46;
        Item.useTime = 23;
        Item.useAnimation = 23;
        Item.useTurn = true;
        Item.pick = 45;
        Item.useStyle = 1;
        Item.knockBack = 2f;
        Item.value = 1770;
        Item.rare = 2;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

     public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Seashell, 2);
            recipe.AddIngredient(ItemID.Coral, 2);
            recipe.AddIngredient(null, "SeaCrystal", 1); 			
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
}}

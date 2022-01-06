using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Granite {
public class GranitePickaxe : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Pickaxe");
		}
    public override void SetDefaults()
    {
        Item.damage = 8;
        Item.DamageType = DamageClass.Melee;
        Item.width = 46;
        Item.height = 46;
        Item.useTime = 16;
        Item.useAnimation = 16;
        Item.useTurn = true;
        Item.pick = 59;
        Item.useStyle = 1;
        Item.knockBack = 2f;
        Item.value = 22500;
        Item.rare = 1;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GraniteBar", 6);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        
    }
}}

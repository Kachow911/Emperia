using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Granite {
public class GraniteHamaxe : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Hamaxe");
		}
    public override void SetDefaults()
    {
        item.damage = 9;
        item.melee = true;
        item.width = 46;
        item.height = 46;
        item.useTime = 26;
        item.useAnimation = 26;
        item.useTurn = true;
        item.axe = 13;
		item.hammer = 65;
        item.useStyle = 1;
        item.knockBack = 2f;
        item.value = 1770;
        item.rare = 2;
        item.UseSound = SoundID.Item1;
        item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 6);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
}}
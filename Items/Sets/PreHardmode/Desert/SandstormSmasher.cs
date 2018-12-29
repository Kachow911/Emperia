using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Desert {
public class SandstormSmasher : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Smasher");
		}
    public override void SetDefaults()
    {
        item.damage = 10;
        item.melee = true;
        item.width = 32;
        item.height = 32;
        item.useTime = 34;
        item.useAnimation = 34;
        item.useTurn = true;
		item.hammer = 7;
        item.useStyle = 1;
        item.knockBack = 3f;
        item.value = 2340;
        item.rare = 2;
        item.UseSound = SoundID.Item1;
        item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "PolishedSandstone", 7);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
}}
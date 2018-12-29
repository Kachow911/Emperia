using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Desert
{
public class SandstormExcavator : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Excavator");
		}
    public override void SetDefaults()
    {
        item.damage = 7;
        item.melee = true;
        item.width = 32;
        item.height = 32;
        item.useTime = 25;
        item.useAnimation = 25;
        item.useTurn = true;
        item.pick = 45;
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
        recipe.AddIngredient(null, "PolishedSandstone", 7);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
}}
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Jungle {
public class JunglePickaxe : ModItem
{
	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primal Pickaxe");
		}
    public override void SetDefaults()
    {
        item.damage = 47;
        item.melee = true;
        item.width = 46;
        item.height = 46;
        item.useTime = 19;
        item.useAnimation = 19;
        item.useTurn = true;
        item.pick = 190;
        item.useStyle = 1;
        item.knockBack = 5f;
        item.value = 17700;
        item.rare = 4;
        item.UseSound = SoundID.Item1;
        item.autoReuse = true;
    }

    public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "JungleMaterial", 4);
			recipe.AddIngredient(ItemID.AdamantiteBar, 2);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "JungleMaterial", 4);
			recipe.AddIngredient(ItemID.TitaniumBar, 2);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
}}
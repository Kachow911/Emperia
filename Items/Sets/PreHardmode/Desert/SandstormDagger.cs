using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Desert
{
	public class SandstormDagger : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 11;
			item.thrown = true;
			item.width = 20;
			item.height = 35;
			item.useStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.knockBack = 2f;
			item.useTime = 20;
			item.useAnimation = 20;
			item.value = 7500;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("SandstormDagger");
			item.shootSpeed = 10f;
			item.consumable = true;
			item.maxStack = 999;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Sandstorm Dagger");
    }
	public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "PolishedSandstone", 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 50);
			recipe.AddRecipe();
		}
	}
	
}

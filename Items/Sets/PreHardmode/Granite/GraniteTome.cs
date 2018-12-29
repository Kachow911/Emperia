using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Emperia.Items.Sets.PreHardmode.Granite
{
	public class GraniteTome : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 19;
			item.magic = true;
			item.width = 22;
			item.height = 24;
			item.useTime = 32;
			item.useAnimation = 32;
			item.useStyle = 5;
			item.knockBack = 3;
			item.value = 5000;
			item.rare = 2;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;

			item.shoot = mod.ProjectileType("GraniteEnergyRock");
			item.shootSpeed = 8f;
			item.mana = 8;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Granite Spellbook");
	  Tooltip.SetDefault("Shoots an explosive energy-filled chunk of granite");
    }
	public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 9);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
	}
}

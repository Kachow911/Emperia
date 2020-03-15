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

namespace Emperia.Items.Weapons
{
	public class AlluringBlossom : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 15;
			item.magic = true;
			item.width = 22;
			item.height = 18;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = 5;
			item.knockBack = 0;
			item.value = 22500;
			item.rare = 2;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("AlluringPulse");
			//item.shoot = mod.ProjectileType("FlameTendril");
			item.shootSpeed = 8f;
			item.mana = 7;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Alluring Blossom");
	  Tooltip.SetDefault("Shoots forth a pulse of pink energy that pulls enemies towards you");
	Item.staff[item.type] = true;
    }
	/*public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "MarbleBar", 9);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }*/
	}
}

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

namespace Emperia.Items.Weapons.Yeti
{
	public class ArcticIncantation : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 20;
			item.magic = true;
			item.width = 22;
			item.height = 24;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = 5;
			item.knockBack = 4;
			item.value = 5000;
			item.rare = 2;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("IceCrystal");
			item.shootSpeed = 5f;
			item.mana = 22;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Arctic Incantation");
	  Tooltip.SetDefault("Shoots forth a magic ice crystal that splits into shards");
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

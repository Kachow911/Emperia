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

namespace Emperia.Items.Sets.PreHardmode.Frostleaf
{
	public class Needlecaster : ModItem //maybe make the leaf variations more obvious or animate them
	{
		public override void SetDefaults()
		{

			item.damage = 12;
			item.magic = true;
			item.width = 22;
			item.height = 24;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 5;
			item.knockBack = 3;
            item.value = 24000;
			item.rare = 1;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Needle");
			item.shootSpeed = 6f;
			item.mana = 5;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Needlecaster");
	  Tooltip.SetDefault("Releases controllable bursts of needle-like leaves");
    }
    	public override void AddRecipes()
    	{
    		ModRecipe recipe = new ModRecipe(mod);      
    	    recipe.AddIngredient(null, "Frostleaf", 7); 
            recipe.AddIngredient(ItemID.BorealWood, 15); 			
    	    recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
	        recipe.AddRecipe();
    	}
	}
}

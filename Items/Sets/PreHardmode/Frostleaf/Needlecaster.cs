using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Emperia.Projectiles.Yeti;

namespace Emperia.Items.Sets.PreHardmode.Frostleaf
{
	public class Needlecaster : ModItem //maybe make the leaf variations more obvious or animate them
	{
		public override void SetDefaults()
		{

			Item.damage = 12;
			Item.DamageType = DamageClass.Magic;
			Item.width = 22;
			Item.height = 24;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 5;
			Item.knockBack = 3;
            Item.value = 24000;
			Item.rare = 1;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Needle>();
			Item.shootSpeed = 6f;
			Item.mana = 5;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Needlecaster");
	  Tooltip.SetDefault("Releases controllable bursts of needle-like leaves");
    }
    	public override void AddRecipes()
    	{
    		Recipe recipe = CreateRecipe();      
    	    recipe.AddIngredient(null, "Frostleaf", 7); 
            recipe.AddIngredient(ItemID.BorealWood, 15); 			
    	    recipe.AddTile(TileID.Anvils);
            recipe.Register();
	        
    	}
	}
}

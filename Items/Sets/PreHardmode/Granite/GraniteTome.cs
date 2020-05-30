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
		int count = 0;
		public override void SetDefaults()
		{
			item.damage = 31;
			item.magic = true;
			item.width = 22;
			item.height = 24;
			item.useTime = 44;
			item.useAnimation = 44;
			item.useStyle = 5;
			item.knockBack = 2.25f;
			item.value = 22500;
			item.noMelee = true;
			item.rare = 1;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("GraniteRock1");
			item.shootSpeed = 6f;
			item.mana = 12;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Granite Spellbook");
	  Tooltip.SetDefault("Cycles between firing 3 different granite chunks, each one more powerful than the last");
    }
	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (count == 0)
		{
			type = mod.ProjectileType("GraniteRock1");
			damage = (damage * 21) / 31; 
		}
		if (count == 1)
		{
			type = mod.ProjectileType("GraniteRock2");
			knockBack = 2.75f;
			speedX *= .75f;
			speedY *= .75f;
		}
		if (count == 2)
		{
			type = mod.ProjectileType("GraniteRock3");
			damage = (damage * 51) / 31; 
			knockBack = 3.5f;
			speedX *= .55f;
			speedY *= .55f;
		}
		count++;
		if (count > 2) count = 0;
		return true;
		
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(0, -2);
	}
	public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 8);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }
	}
}

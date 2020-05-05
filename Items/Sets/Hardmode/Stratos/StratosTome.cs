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

namespace Emperia.Items.Sets.Hardmode.Stratos
{
	
	public class StratosTome : ModItem
	{
		int count = 0;
		public override void SetDefaults()
		{
			item.damage = 56;
			item.magic = true;
			item.width = 22;
			item.height = 24;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 5;
			item.knockBack = 2.25f;
			item.value = 22500;
			item.noMelee = true;
			item.rare = 4;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("StratosMeteorite2");
			item.shootSpeed = 12f;
			item.mana = 12;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Stratos Spellbook");
	  Tooltip.SetDefault("Cycles between firing different stratos chunks, each one more powerful than the last");
    }
	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (count == 0)
		{
			type = mod.ProjectileType("StratosMeteorite2");
			damage = 56; 
		}
		if (count == 1)
		{
			type = mod.ProjectileType("StratosMeteor");
			knockBack = 4f;
			damage = 75;
		}
		if (count == 2)
		{
			type = mod.ProjectileType("StratosMeteorite2");
			damage = 56; 
			knockBack = 5f;
		}
		count++;
		if (count > 2) count = 0;
		Vector2 placePosition = new Vector2(player.Center.X + Main.rand.Next(-50, 50), player.Center.Y + Main.rand.Next(-50, 50));
		Vector2 direction = Main.MouseWorld - player.Center;
		direction.Normalize();
		Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * 8f, direction.Y * 8f, type, damage, knockBack, player.whoAmI);
		return false;
		
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(0, -2);
	}
	/*public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 8);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }*/
	}
}

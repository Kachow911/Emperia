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

namespace Emperia.Items.Sets.PreHardmode.Aquatic
{
	public class Downpour : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 17;
			item.magic = true;
			item.width = 22;
			item.height = 24;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 5;
			item.knockBack = 3;
			item.value = 5000;
			item.rare = 3;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;

			item.shoot = mod.ProjectileType("Rain");
			item.shootSpeed = 5f;
			item.mana = 5;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Downpour");
	  Tooltip.SetDefault("Rains ocean water onto the cursor");
    }
	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type1, ref int damage, ref float knockBack)
	{
		float speedFactor;
		float damageFactor;
		for (int i = 0; i <= Main.rand.Next(2); i++)
		{
			Vector2 placePosition = new Vector2(Main.MouseWorld.X + Main.rand.Next(-100, 100),player.Center.Y -400);
			Vector2 direction = Main.MouseWorld - placePosition;
			direction.Normalize();
			if (Main.rand.NextBool(2))
			{
				type1 = mod.ProjectileType("Rain");
				speedFactor = 11.5f;
				damageFactor = 1f;
			}
			else
			{
				type1 = mod.ProjectileType("Rain");
				speedFactor = 10.8f;
				damageFactor = 1.2f;
			}
				Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * speedFactor, direction.Y * speedFactor, type1, damage, 1, Main.myPlayer, 0, 0);
			}
			return false;
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

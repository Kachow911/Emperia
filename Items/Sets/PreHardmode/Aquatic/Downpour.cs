using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using System.Collections.Generic;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.PreHardmode.Aquatic
{
	public class Downpour : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 17;
			Item.DamageType = DamageClass.Magic;
			Item.width = 22;
			Item.height = 24;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = 5;
			Item.knockBack = 3;
			Item.value = 5000;
			Item.rare = 3;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;

			Item.shoot = ModContent.ProjectileType<Projectiles.Rain>();
			Item.shootSpeed = 5f;
			Item.mana = 5;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Downpour");
	  Tooltip.SetDefault("Rains ocean water onto the cursor");
    }
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
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
				type = ModContent.ProjectileType<Projectiles.Rain>();
				speedFactor = 11.5f;
				damageFactor = 1f;
			}
			else
			{
				type = ModContent.ProjectileType<Projectiles.Rain>();
				speedFactor = 10.8f;
				damageFactor = 1.2f;
			}
				Projectile.NewProjectile(source, placePosition.X, placePosition.Y, direction.X * speedFactor, direction.Y * speedFactor, type, damage, 1, Main.myPlayer, 0, 0);
			}
			return false;
	}
	/*public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MarbleBar", 9);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        
    }*/
	}
}

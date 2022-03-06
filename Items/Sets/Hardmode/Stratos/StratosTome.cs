using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using System.Collections.Generic;
using Emperia.Projectiles.Stratos;

namespace Emperia.Items.Sets.Hardmode.Stratos
{
	
	public class StratosTome : ModItem
	{
		int count = 0;
		public override void SetDefaults()
		{
			Item.damage = 56;
			Item.DamageType = DamageClass.Magic;
			Item.width = 22;
			Item.height = 24;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = 5;
			Item.knockBack = 2.25f;
			Item.value = 22500;
			Item.noMelee = true;
			Item.rare = 4;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<StratosMeteorite2>();
			Item.shootSpeed = 12f;
			Item.mana = 12;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Stratos Spellbook");
	  Tooltip.SetDefault("Cycles between firing different stratos chunks, each one more powerful than the last");
    }
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
	{
		if (count == 0)
		{
			type = ModContent.ProjectileType<StratosMeteorite2>();
			damage = 56; 
		}
		if (count == 1)
		{
			type = ModContent.ProjectileType<StratosMeteor>();
			knockBack = 4f;
			damage = 75;
		}
		if (count == 2)
		{
			type = ModContent.ProjectileType<StratosMeteorite2>();
			damage = 56; 
			knockBack = 5f;
		}
		count++;
		if (count > 2) count = 0;
		Vector2 placePosition = new Vector2(player.Center.X + Main.rand.Next(-50, 50), player.Center.Y + Main.rand.Next(-50, 50));
		Vector2 direction = Main.MouseWorld - player.Center;
		direction.Normalize();
		Projectile.NewProjectile(source, placePosition.X, placePosition.Y, direction.X * 8f, direction.Y * 8f, type, damage, knockBack, player.whoAmI);
		Projectile.NewProjectile(source, placePosition.X, placePosition.Y, 0, 0, ModContent.ProjectileType<Projectiles.StratosPortalAnim>(), 0, 0, player.whoAmI);
		return false;
		
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(0, -2);
	}
	/*public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GraniteBar", 8);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        
    }*/
	}
}

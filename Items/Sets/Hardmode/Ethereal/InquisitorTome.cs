using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using System.Collections.Generic;
using Emperia.Projectiles.Ethereal;

namespace Emperia.Items.Sets.Hardmode.Ethereal
{
	
	public class InquisitorTome : ModItem
	{
		int count = 0;
		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Magic;
			Item.width = 22;
			Item.height = 24;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useStyle = 5;
			Item.knockBack = 7f;
			Item.value = 22500;
			Item.noMelee = true;
			Item.rare = 5;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<EtherealBoltTome>();
			Item.shootSpeed = 12f;
			Item.mana = 4;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Inquisitor Spellbook");
	  Tooltip.SetDefault("Summons ethereal bolts to attack your foes");
    }
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
	{
		Vector2 placePosition = Main.MouseWorld + new Vector2(0, 150).RotatedByRandom(MathHelper.ToRadians(360));
		Vector2 direction = Main.MouseWorld - placePosition;
		direction.Normalize();
		Projectile.NewProjectile(source, placePosition.X, placePosition.Y, direction.X * 8f, direction.Y * 8f, type, damage, knockBack, player.whoAmI);
		//rojectile.NewProjectile(placePosition.X, placePosition.Y, 0, 0, ModContent.ProjectileType<StratosPortalAnim>(), 0, 0, player.whoAmI);
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

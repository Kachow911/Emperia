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

namespace Emperia.Items.Sets.Hardmode.Ethereal
{
	
	public class InquisitorTome : ModItem
	{
		int count = 0;
		public override void SetDefaults()
		{
			item.damage = 60;
			item.magic = true;
			item.width = 22;
			item.height = 24;
			item.useTime = 16;
			item.useAnimation = 16;
			item.useStyle = 5;
			item.knockBack = 7f;
			item.value = 22500;
			item.noMelee = true;
			item.rare = 5;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("EtherealBoltTome");
			item.shootSpeed = 12f;
			item.mana = 4;
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Inquisitor Spellbook");
	  Tooltip.SetDefault("Summons ethereal bolts to attack your foes");
    }
	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		Vector2 placePosition = Main.MouseWorld + new Vector2(0, 150).RotatedByRandom(MathHelper.ToRadians(360));
		Vector2 direction = Main.MouseWorld - placePosition;
		direction.Normalize();
		Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * 8f, direction.Y * 8f, type, damage, knockBack, player.whoAmI);
		//rojectile.NewProjectile(placePosition.X, placePosition.Y, 0, 0, mod.ProjectileType("StratosPortalAnim"), 0, 0, player.whoAmI);
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

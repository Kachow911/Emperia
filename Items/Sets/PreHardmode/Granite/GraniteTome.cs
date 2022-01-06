using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using System.Collections.Generic;
using Emperia.Projectiles.Granite;

namespace Emperia.Items.Sets.PreHardmode.Granite
{

	public class GraniteTome : ModItem
	{
		int count = 0;
		public override void SetDefaults()
		{
			Item.damage = 31;
			Item.DamageType = DamageClass.Magic;
			Item.width = 22;
			Item.height = 24;
			Item.useTime = 44;
			Item.useAnimation = 44;
			Item.useStyle = 5;
			Item.knockBack = 2.25f;
			Item.value = 27000;
			Item.noMelee = true;
			Item.rare = 1;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<GraniteRock1>();
			Item.shootSpeed = 6f;
			Item.mana = 12;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Spellbook");
			Tooltip.SetDefault("Cycles between firing 3 different granite chunks, each one more powerful than the last");
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack)
		{
			if (count == 0)
			{
				type = ModContent.ProjectileType<GraniteRock1>();
				damage = (damage * 21) / 31;
			}
			if (count == 1)
			{
				type = ModContent.ProjectileType<GraniteRock2>();
				knockBack = 2.75f;
				velocity *= .75f;
			}
			if (count == 2)
			{
				type = ModContent.ProjectileType<GraniteRock3>();
				damage = (damage * 51) / 31;
				knockBack = 3.5f;
				velocity *= .55f;
			}
		}
	
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
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
		    Recipe recipe = CreateRecipe();
		    recipe.AddIngredient(null, "GraniteBar", 8);
		    recipe.AddTile(TileID.Anvils);
		    recipe.Register();
		    
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Stratos;

namespace Emperia.Items.Sets.Hardmode.Stratos
{
	public class StratosStaff : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 59;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 20;
			Item.width = 25;
			Item.height = 26;
			Item.useTime = 15;
			Item.UseSound = SoundID.Item43;
			Item.useAnimation = 15;
			Item.reuseDelay = 14;
			Item.useStyle = 5;
			Item.staff[Item.type] = true;
			Item.noMelee = true;
			Item.knockBack = 2.5f;
			Item.value = 6500;
			Item.rare = 4;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<StratosEnergy>();
			Item.shootSpeed = 12f;
		}
		

		public override void SetStaticDefaults()
		{
		  //DisplayName.SetDefault("Tesla Coil Rod");
		  Tooltip.SetDefault("Fires a blast of stratos energy and rock chunks");
		}
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			int num1 = 2;
			for (int index = 0; index < num1; ++index)
			{
				float num2 = (velocity.X / 3) + (float)(Main.rand.Next(-20, 21) * 0.05f);
				float num3 = (velocity.Y / 3) + (float)(Main.rand.Next(-20, 21) * 0.05f);
				Projectile.NewProjectile(source, position.X, position.Y, num2, num3, ModContent.ProjectileType<StratosRockMini>(), (int)((double)damage * 0.5), knockBack, (int)((Entity)player).whoAmI, 0.0f, 0.0f);
			}
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockBack, (int)((Entity)player).whoAmI, 0.0f, 0.0f);
			return false;

		}
		public override void AddRecipes()
		{
			/*
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.RottenChunk, 2);
			recipe.AddIngredient(ItemID.CursedFlame, 10);
			recipe.AddIngredient(ItemID.SoulofNight, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			
			*/
		}
	}
}

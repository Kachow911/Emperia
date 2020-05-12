using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Emperia.Items.Sets.Hardmode.Stratos
{
	public class StratosStaff : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 59;
			item.magic = true;
			item.mana = 20;
			item.width = 25;
			item.height = 26;
			item.useTime = 15;
			item.UseSound = SoundID.Item43;
			item.useAnimation = 15;
			item.reuseDelay = 14;
			item.useStyle = 5;
			Item.staff[item.type] = true;
			item.noMelee = true;
			item.knockBack = 2.5f;
			item.value = 6500;
			item.rare = 4;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("StratosEnergy");
			item.shootSpeed = 12f;
		}
		

		public override void SetStaticDefaults()
		{
		  //DisplayName.SetDefault("Tesla Coil Rod");
		  Tooltip.SetDefault("Fires a blast of stratos energy and rock chunks");
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int num1 = 2;
			for (int index = 0; index < num1; ++index)
			{
				float num2 = (speedX / 3) + (float)(Main.rand.Next(-20, 21) * 0.05f);
				float num3 = (speedY / 3) + (float)(Main.rand.Next(-20, 21) * 0.05f);
				Projectile.NewProjectile(position.X, position.Y, num2, num3, mod.ProjectileType("StratosRockMini"), (int)((double)damage * 0.5), knockBack, (int)((Entity)player).whoAmI, 0.0f, 0.0f);
			}
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, (int)((Entity)player).whoAmI, 0.0f, 0.0f);
			return false;

		}
		public override void AddRecipes()
		{
			/*
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RottenChunk, 2);
			recipe.AddIngredient(ItemID.CursedFlame, 10);
			recipe.AddIngredient(ItemID.SoulofNight, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
			*/
		}
	}
}

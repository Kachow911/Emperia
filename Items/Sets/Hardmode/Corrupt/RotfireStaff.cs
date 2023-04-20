using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Corrupt;

namespace Emperia.Items.Sets.Hardmode.Corrupt
{
	public class RotfireStaff : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 53;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 15;
			Item.width = 25;
			Item.height = 26;
			Item.useTime = 12;
			Item.UseSound = SoundID.Item43;
			Item.useAnimation = 36;
			Item.reuseDelay = 14;
			Item.useStyle = 5;
			Item.staff[Item.type] = true;
			Item.noMelee = true;
			Item.knockBack = 2.5f;
			Item.value = 6500;
			Item.rare = 4;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<CursedFlame1>();
			Item.shootSpeed = 12f;
		}
		

		public override void SetStaticDefaults()
		{
		  //DisplayName.SetDefault("Tesla Coil Rod");
		  // Tooltip.SetDefault("Fires a burst of cursed flames");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.RottenChunk, 2);
			recipe.AddIngredient(ItemID.CursedFlame, 10);
			recipe.AddIngredient(ItemID.SoulofNight, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			
		}
	}
}

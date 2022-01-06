using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Crimson;

namespace Emperia.Items.Sets.Hardmode.Crimson
{
	public class BloodboilStaff : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 48;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 5;
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
			Item.shoot = ModContent.ProjectileType<BigBubble>();
			Item.shootSpeed = 12f;
		}
		

		public override void SetStaticDefaults()
		{
		  //DisplayName.SetDefault("Tesla Coil Rod");
		  Tooltip.SetDefault("Fires splitting Ichor bubbles");
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

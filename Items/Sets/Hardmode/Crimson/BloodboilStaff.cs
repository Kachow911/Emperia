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

namespace Emperia.Items.Sets.Hardmode.Crimson
{
	public class BloodboilStaff : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 48;
			item.magic = true;
			item.mana = 5;
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
			item.shoot = mod.ProjectileType("BigBubble");
			item.shootSpeed = 12f;
		}
		

		public override void SetStaticDefaults()
		{
		  //DisplayName.SetDefault("Tesla Coil Rod");
		  Tooltip.SetDefault("Fires splitting Ichor bubbles");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RottenChunk, 2);
			recipe.AddIngredient(ItemID.CursedFlame, 10);
			recipe.AddIngredient(ItemID.SoulofNight, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

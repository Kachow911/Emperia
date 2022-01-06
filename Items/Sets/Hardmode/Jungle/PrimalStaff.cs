using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.Hardmode.Jungle
{
	public class PrimalStaff : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 39;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 12;
			Item.width = 25;
			Item.height = 26;
			Item.useTime = 28;
			Item.UseSound = SoundID.Item43;
			Item.useAnimation = 28;
			Item.useStyle = 5;
			Item.staff[Item.type] = true;
			Item.noMelee = true;
			Item.knockBack = 2.5f;
			Item.value = 6500;
			Item.rare = 4;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<PrimalStaffProj>();
			Item.shootSpeed = 20f;
		}
		

		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Primal Staff");
		  Tooltip.SetDefault("Fires a spread of slow spheres");
		}
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			float numberProjectiles = 3;
			float rotation = MathHelper.ToRadians(15);
			position += Vector2.Normalize(velocity) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 Projectile.
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "JungleMaterial", 5);
			recipe.AddIngredient(ItemID.AdamantiteBar, 2);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			
			recipe = CreateRecipe();
			recipe.AddIngredient(null, "JungleMaterial", 5);
			recipe.AddIngredient(ItemID.TitaniumBar, 2);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			
		}
	}
}

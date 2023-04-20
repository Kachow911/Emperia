using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.Hardmode.Jungle {
public class PrimalPike : ModItem
{

	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Primal Pike");
		}
	public override void SetDefaults()
	{
		Item.width = 56;  //The width of the .png file in pixels divided by 2.
		Item.damage = 50;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 25;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useTime = 25;
		Item.knockBack = 5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 56;  //The height of the .png file in pixels divided by 2.
				Item.value = 45000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.LightRed;  //Ranges from 1 to 11.
		Item.shoot = ModContent.ProjectileType<Projectiles.PrimalPike>();
		Item.shootSpeed = 8f;
		
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
}}
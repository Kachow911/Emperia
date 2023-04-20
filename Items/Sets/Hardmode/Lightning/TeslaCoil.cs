using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Lightning;

namespace Emperia.Items.Sets.Hardmode.Lightning
{
	public class TeslaCoil : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 45;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 18;
			Item.width = 25;
			Item.height = 26;
			Item.useTime = 28;
			Item.UseSound = SoundID.Item43;
			Item.useAnimation = 28;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.staff[Item.type] = true;
			Item.noMelee = true;
			Item.knockBack = 2.5f;
			Item.value = 6500;
			Item.rare = ItemRarityID.LightRed;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<LightningBolt1>();
			Item.shootSpeed = 16f;
		}
		

		public override void SetStaticDefaults()
		{
		  // DisplayName.SetDefault("Tesla Coil Rod");
		  // Tooltip.SetDefault("Fires a bolt of lightning");
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.lightningSet)
				Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ModContent.ProjectileType<LightningSetEffect>(), 25, knockBack, player.whoAmI);
			return true;
		}
		/*public override void AddRecipes()
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
			
		}*/
	}
}

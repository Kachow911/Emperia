using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Emperia.Items.Sets.Hardmode.Jungle
{
	public class PrimalRepeater : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots multiple slightly inaccurate bullets \n10% chance to not consume ammo");
		}

		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 20;
			Item.useTime = 23;
			Item.useAnimation = 23;
			Item.useStyle = 5;
			Item.noMelee = true; //so the Item's animation doesn't do damage
			Item.knockBack = 3;
			Item.value = 10000;
			Item.rare = 4;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = 10; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Bullet;
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

		 
		public override bool CanConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .1f;
		}


		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			int numberProjectiles = 3;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5)); // 30 degree spread.
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false; // return false because we don't want tmodloader to shoot Projectile
		}


		// Help, my gun isn't being held at the handle! Adjust these 2 numbers until it looks right.
		/*public override Vector2? HoldoutOffset()
		{
			return new Vector2(10, 0);
		}*/

		
	}
}

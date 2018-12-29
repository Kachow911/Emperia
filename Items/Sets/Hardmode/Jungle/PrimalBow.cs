using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Jungle
{
    public class PrimalBow : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primal Bow");
			Tooltip.SetDefault("Shoots 4 arrows\nOnly consumes ammo on the first one");
		}
        public override void SetDefaults()
        {
            item.damage = 23;
            item.noMelee = true;
            item.ranged = true;
            item.width = 69;
            item.height = 40;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = ItemID.WoodenArrow;
            item.knockBack = 1;
            item.value = 1000;
            item.rare = 4;
            item.autoReuse = true;
            item.shootSpeed = 10f;
			item.UseSound = SoundID.Item5; 
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 4; 
			float rotation = MathHelper.ToRadians(20);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 8f, perturbedSpeed.Y * 8f, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "JungleMaterial", 5);
			recipe.AddIngredient(ItemID.AdamantiteBar, 2);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "JungleMaterial", 5);
			recipe.AddIngredient(ItemID.TitaniumBar, 2);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
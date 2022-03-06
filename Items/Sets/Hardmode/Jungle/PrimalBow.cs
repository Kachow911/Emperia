using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;

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
            Item.damage = 23;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 69;
            Item.height = 40;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 5;
            Item.shoot = 3;
            Item.useAmmo = ItemID.WoodenArrow;
            Item.knockBack = 1;
            Item.value = 1000;
            Item.rare = 4;
            Item.autoReuse = true;
            Item.shootSpeed = 10f;
			Item.UseSound = SoundID.Item5; 
        }
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			float numberProjectiles = 4; 
			float rotation = MathHelper.ToRadians(20);
			position += Vector2.Normalize(velocity) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 Projectile.
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * 8f, perturbedSpeed.Y * 8f, type, damage, knockBack, player.whoAmI);
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
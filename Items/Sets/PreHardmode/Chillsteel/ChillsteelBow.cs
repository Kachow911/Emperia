using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Ice;

namespace Emperia.Items.Sets.PreHardmode.Chillsteel
{
    public class ChillsteelBow : ModItem
    {
		//int counter = 0;
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Granite Bow");
			Tooltip.SetDefault("Shoots out ice crystals occasionally along with the arrow");
		}
        public override void SetDefaults()
        {
            Item.damage = 41;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 40;
            Item.useTime = 28;
            Item.useAnimation = 29;
            Item.useStyle = 5;
            Item.shoot = 3;
            Item.useAmmo = ItemID.WoodenArrow;
            Item.knockBack = 1;
            Item.value = 22500;
            Item.rare = 4;
            Item.autoReuse = false;
            Item.shootSpeed = 12f;
			Item.UseSound = SoundID.Item5; 
        }

		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			if (Main.rand.NextBool(3))
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-20, 20)));
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X / 2, perturbedSpeed.Y / 2, ModContent.ProjectileType<IceBomb>(), damage, knockBack, player.whoAmI);
			}
			return true;

		}


		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}

		/*public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.RottenChunk, 2);
			recipe.AddIngredient(ItemID.CursedFlame, 10);
			recipe.AddIngredient(ItemID.SoulofNight, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			
		}*/
	}
}

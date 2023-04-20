using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Desert;


namespace Emperia.Items.Sets.PreHardmode.Desert
{
    public class CarapaceCruise : ModItem
    {
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Wave");
			// Tooltip.SetDefault("Summons worms from the sky to plague your foes");
		}


        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 9;
            Item.width = 52;
            Item.height = 60;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<DesertWorm>();
            Item.shootSpeed = 8f;
        }
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			float speedFactor;
			float damageFactor;
			for (int i = 0; i <= 3; i++)
			{
				Vector2 placePosition = new Vector2(Main.MouseWorld.X + Main.rand.Next(-400, 400), player.Center.Y - 800);
				Vector2 direction = Main.MouseWorld - placePosition;
				direction.Normalize();
					speedFactor = 15.5f;
					damageFactor = 1f;

				Projectile.NewProjectile(source, placePosition.X, placePosition.Y, direction.X * speedFactor, direction.Y * speedFactor, type, damage, 1, Main.myPlayer, 0, 0);
			}
			return false;
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DesertEye", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
    }
}

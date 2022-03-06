using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Granite;

namespace Emperia.Items.Sets.PreHardmode.Granite
{
    public class GraniteBow : ModItem
    {
		int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Bow");
			Tooltip.SetDefault("Fires a granite energy arrow every 3 shots");
		}
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 40;
            Item.useTime = 29;
            Item.useAnimation = 29;
            Item.useStyle = 5;
            Item.shoot = 3;
            Item.useAmmo = ItemID.WoodenArrow;
            Item.knockBack = 1;
            Item.value = 27000;
            Item.rare = 1;
            Item.autoReuse = false;
            Item.shootSpeed = 6f;
			Item.UseSound = SoundID.Item5; 
        }

		/*public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (counter == 3)
			{
				damage = (int)(damage * 1.2);
			}
		}*/
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			if (counter == 3)
			{
                damage = (int) (damage * 1.2);
				Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<GraniteArrow>(), damage, knockBack, player.whoAmI);
				counter = 0;
				return false;
			}
			counter++;
			return true;
		}
		
		public override bool CanConsumeAmmo(Player player)
		{
			return !(counter == 3);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(6, 0);
		}
		
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "GraniteBar", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			
		}
    }
}

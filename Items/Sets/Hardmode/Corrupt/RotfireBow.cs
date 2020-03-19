using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Corrupt
{
    public class RotfireBow : ModItem
    {
		//int counter = 0;
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Granite Bow");
			Tooltip.SetDefault("Shoots out cursed flames along with the arrow");
		}
        public override void SetDefaults()
        {
            item.damage = 38;
            item.noMelee = true;
            item.ranged = true;
            item.width = 30;
            item.height = 40;
            item.useTime = 28;
            item.useAnimation = 29;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = ItemID.WoodenArrow;
            item.knockBack = 1;
            item.value = 22500;
            item.rare = 4;
            item.autoReuse = false;
            item.shootSpeed = 12f;
			item.UseSound = SoundID.Item5; 
        }

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-20, 20)));
			Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X / 2, perturbedSpeed.Y / 2, mod.ProjectileType("FireBallCursed2"), damage, knockBack, player.whoAmI);
			return true;

		}


		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
        public override void AddRecipes()
		{
			/*ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "GraniteBar", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();*/
		}
    }
}

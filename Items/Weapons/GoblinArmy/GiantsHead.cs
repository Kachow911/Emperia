using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Emperia.Projectiles;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;

namespace Emperia.Items.Weapons.GoblinArmy
{
	public class GiantsHead : ModItem
	{
		public override void SetDefaults()
		{

			Item.damage = 37;
			Item.DamageType = DamageClass.Magic;
			Item.width = 46;
			Item.height = 42;
			Item.useTime = 31;
			Item.useAnimation = 31;
			Item.useStyle = 5;
			Item.knockBack = 4;
			Item.value = 5000;
			Item.rare = 3;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<FlameTendril>();
			Item.noUseGraphic = true;
			Item.shootSpeed = 7f;
			Item.mana = 23;
		}
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			Vector2 placePosition = Main.MouseWorld - player.Center;
			placePosition.Normalize();
			int p = Projectile.NewProjectile(source, player.Center.X + placePosition.X * 20f, player.Center.Y + placePosition.Y * 20f, 0, 0, ModContent.ProjectileType<GiantsHeadAnim>(), 0, 1, Main.myPlayer, 0, 0);
			
			if (placePosition.X > 0)
			{
				Main.projectile[p].rotation = (float)Math.Atan2((double)placePosition.Y, (double)placePosition.X);
				Main.projectile[p].spriteDirection = 1;
			}
			else if (placePosition.X < 0)
			{
				Main.projectile[p].rotation = (float)Math.Atan2((double)placePosition.Y, (double)placePosition.X) + 3.14f;
				Main.projectile[p].spriteDirection = -1;
				Main.projectile[p].position.X -= 7;
			}
			return true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Brobdingnagian Head");
			Tooltip.SetDefault("Shoots forth tendrils of flame");
		
		}
		/*public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "MarbleBar", 9);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			
		}*/
	}
}

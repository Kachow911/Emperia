using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Emperia.Items.Weapons.GoblinArmy
{
	public class GiantsHead : ModItem
	{
		public override void SetDefaults()
		{

			item.damage = 37;
			item.magic = true;
			item.width = 46;
			item.height = 42;
			item.useTime = 31;
			item.useAnimation = 31;
			item.useStyle = 5;
			item.knockBack = 4;
			item.value = 5000;
			item.rare = 3;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("FlameTendril");
			item.noUseGraphic = true;
			item.shootSpeed = 7f;
			item.mana = 23;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type1, ref int damage, ref float knockBack)
		{
			Vector2 placePosition = Main.MouseWorld - player.Center;
			placePosition.Normalize();
			int p = Projectile.NewProjectile(player.Center.X + placePosition.X * 20f, player.Center.Y + placePosition.Y * 20f, 0, 0, mod.ProjectileType("GiantsHeadAnim"), 0, 1, Main.myPlayer, 0, 0);
			
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
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "MarbleBar", 9);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}*/
	}
}

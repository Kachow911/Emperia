using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Yeti
{
    public class BigGameHunter : ModItem
    {
		private int bulletLoadeds = 1;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Big Game Rifle");
			Tooltip.SetDefault("You must reload after shots\nRight Click to load a bullet");
		}
        public override void SetDefaults()
        {
            item.damage = 50;
            item.noMelee = true;
            item.ranged = true;
            item.width = 69;
            item.height = 40;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.knockBack = 1;
            item.value = 1000;
            item.rare = 3;
			item.scale = 0.7f;
            item.autoReuse = false;
            item.shootSpeed = 10f;
			item.crit = 10;
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			
			if (!(player.altFunctionUse == 2))
			{
				if (bulletLoadeds > 0)
				{
					Color rgb = new Color(252, 207, 83);
					for (int i = - 10; i < 10; i++)
					{
						int index2 = Dust.NewDust(player.position + new Vector2(i, 0), player.width, player.height, 76, player.velocity.X / 5, (float) player.velocity.Y, 0, rgb, 0.9f);
					}
					Main.PlaySound(SoundID.Item, player.Center, 14);
					bulletLoadeds--;
					return true;
				}
			}
			return false;  
		}
		public override bool AltFunctionUse(Player player)
		{
			Color rgb = new Color(139,69,19);
			int index2 = Dust.NewDust(player.position, player.width, player.height, 76, (float) player.velocity.X, (float) player.velocity.Y, 0, rgb, 0.9f);
			if (bulletLoadeds < 1)
				 bulletLoadeds++;
			return true;
		}
		public override bool UseItem(Player player)
		{
			return bulletLoadeds > 0;
		}
        /*public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 8);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }*/
    }
}
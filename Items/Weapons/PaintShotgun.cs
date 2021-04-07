using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Emperia.Items.Weapons        
{
    public class PaintShotgun : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Colorblast");
		}
        public override void SetDefaults()
        {  
            item.damage = 24;  
            item.ranged = true;    
            item.width = 42; 
            item.height = 16;    
            item.useAnimation = 20;
			item.useTime = 10;
            item.useStyle = 5;  
            item.noMelee = true; 
            item.knockBack = 1.3f; 
            item.value = 255000;
            item.rare = 4;   
            item.autoReuse = false;  
            item.shoot = mod.ProjectileType("PaintBall");   
            item.shootSpeed = 6f; 
			item.crit = 4;
        }
 
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 3; 
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10)); 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 2, perturbedSpeed.Y * 2, type, damage, knockBack, player.whoAmI);
			}
			Main.PlaySound(SoundID.Item98, player.position);
			Main.PlaySound(SoundID.Item5, player.position);
			return false; 
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PainterPaintballGun);
            recipe.AddIngredient(ItemID.Shotgun);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
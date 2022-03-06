using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;
using static Terraria.Audio.SoundEngine;
 
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
            Item.damage = 24;  
            Item.DamageType = DamageClass.Ranged;    
            Item.width = 42; 
            Item.height = 16;    
            Item.useAnimation = 20;
			Item.useTime = 10;
            Item.useStyle = 5;  
            Item.noMelee = true; 
            Item.knockBack = 1.3f; 
            Item.value = 255000;
            Item.rare = 4;   
            Item.autoReuse = false;  
            Item.shoot = ModContent.ProjectileType<PaintBall>();   
            Item.shootSpeed = 6f; 
			Item.crit = 4;
        }
 
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			int numberProjectiles = 3; 
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10)); 
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * 2, perturbedSpeed.Y * 2, type, damage, knockBack, player.whoAmI);
			}
			PlaySound(SoundID.Item98, player.position);
			PlaySound(SoundID.Item5, player.position);
			return false; 
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-2, 0);
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.PainterPaintballGun);
            recipe.AddIngredient(ItemID.Shotgun);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			
		}
    }
}
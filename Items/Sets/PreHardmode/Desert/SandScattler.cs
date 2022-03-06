using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Desert;
 
namespace Emperia.Items.Sets.PreHardmode.Desert     
{
    public class SandScattler : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shell Striker");
			Tooltip.SetDefault("Shoots a spread of sharp fragments that explode if they all hit the target");
		}
        public override void SetDefaults()
        {  
            Item.damage = 12;  
            Item.DamageType = DamageClass.Ranged;    
            Item.width = 66; 
            Item.height = 28;    
            Item.useAnimation = 38;
			Item.useTime = 38;
			//Item.reuseDelay = 14;
            Item.useStyle = 5;  
            Item.noMelee = true; 
            Item.knockBack = 1f; 
            Item.value = 27000;
            Item.rare = 1;   
            Item.autoReuse = false;  
            Item.shoot = ModContent.ProjectileType<ShellStrike>();   
            Item.shootSpeed = 11.5f; 
        }
 
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			int numberProjectiles = 4; 
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(((i == 3) ? 22 : 8))); 
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * 2, perturbedSpeed.Y * 2, type, damage, knockBack, player.whoAmI);
			}
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item11, player.position);
			return false; 
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 0);
		}
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AridScale", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
    }
}
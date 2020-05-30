using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
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
            item.damage = 12;  
            item.ranged = true;    
            item.width = 66; 
            item.height = 28;    
            item.useAnimation = 38;
			item.useTime = 38;
			//item.reuseDelay = 14;
            item.useStyle = 5;  
            item.noMelee = true; 
            item.knockBack = 1f; 
            item.value = 27000;
            item.rare = 1;   
            item.autoReuse = false;  
            item.shoot = mod.ProjectileType("ShellStrike");   
            item.shootSpeed = 11.5f; 
        }
 
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 4; 
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(((i == 3) ? 22 : 8))); 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 2, perturbedSpeed.Y * 2, type, damage, knockBack, player.whoAmI);
			}
			Main.PlaySound(SoundID.Item11, player.position);
			return false; 
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-8, 0);
		}
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AridScale", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
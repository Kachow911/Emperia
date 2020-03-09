using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Emperia.Items.Weapons.GoblinArmy        
{
    public class GiantsDevastator : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Giant's Devastator");
			Tooltip.SetDefault("Shoots powerful rockets that leave lasting fire on the ground\nHas a long reload time");
		}
        public override void SetDefaults()
        {  
            item.damage = 45;  
            item.ranged = true;    
            item.width = 42; 
            item.height = 16;    
            item.useAnimation = 25;
			item.useTime = 25;
			item.reuseDelay = 80;
            item.useStyle = 5;  
            item.noMelee = true; 
            item.knockBack = 4f; 
            item.value = 255000;
            item.rare = 3;   
            item.autoReuse = false;  
            item.shoot = mod.ProjectileType("GoblinRocket");   
            item.shootSpeed =9f; 
        }
 
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 1; 
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5)); 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 2, perturbedSpeed.Y * 2, type, damage, knockBack, player.whoAmI);
			}
			Main.PlaySound(SoundID.Item5, player.position);
			return false; 
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
    }
}
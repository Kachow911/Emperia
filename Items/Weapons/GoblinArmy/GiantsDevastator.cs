using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;
 
namespace Emperia.Items.Weapons.GoblinArmy        
{
    public class GiantsDevastator : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Giant's Devastator");
			// Tooltip.SetDefault("Shoots powerful rockets that leave lasting fire on the ground\nHas a long reload time");
		}
        public override void SetDefaults()
        {  
            Item.damage = 45;  
            Item.DamageType = DamageClass.Ranged;    
            Item.width = 42; 
            Item.height = 16;    
            Item.useAnimation = 25;
			Item.useTime = 25;
			Item.reuseDelay = 80;
            Item.useStyle = 5;  
            Item.noMelee = true; 
            Item.knockBack = 4f; 
            Item.value = 255000;
            Item.rare = 3;   
            Item.autoReuse = false;  
            Item.shoot = ModContent.ProjectileType<GoblinRocket>();   
            Item.shootSpeed =9f; 
        }
 
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			int numberProjectiles = 1; 
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5)); 
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * 2, perturbedSpeed.Y * 2, type, damage, knockBack, player.whoAmI);
			}
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item5, player.position);
			return false; 
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
    }
}
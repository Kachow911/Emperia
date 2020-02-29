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
			DisplayName.SetDefault("Sand Scattler");
			Tooltip.SetDefault("Shoots a volley of sand clumps with a chance to light enemies on fire");
		}
        public override void SetDefaults()
        {  
            item.damage = 18;  
            item.ranged = true;    
            item.width = 66; 
            item.height = 28;    
            item.useAnimation = 24;
			item.useTime = 24;
			item.reuseDelay = 14;
            item.useStyle = 5;  
            item.noMelee = true; 
            item.knockBack = 1.3f; 
            item.value = 255000;
            item.rare = 3;   
            item.autoReuse = false;  
            item.shoot = mod.ProjectileType("SandClump2");   
            item.shootSpeed = 6f; 
			item.crit = 12;
        }
 
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 3; 
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(12)); 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 2, perturbedSpeed.Y * 2, type, damage, knockBack, player.whoAmI);
			}
			Main.PlaySound(SoundID.Item10, player.position);
			return false; 
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
    }
}
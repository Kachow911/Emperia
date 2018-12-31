using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Emperia.Items.Weapons.Volcano       
{
    public class Hellraiser : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellraiser");
			Tooltip.SetDefault("Shoots a volley of flaming buckshots\nAttacks inflict 'On Fire'\nBuckshots leave fire behind on the ground");
		}
        public override void SetDefaults()
        {  
            item.damage = 19;  
            item.ranged = true;    
            item.width = 42; 
            item.height = 16;    
            item.useAnimation = 34;
			item.useTime = 34;
            item.useStyle = 5;  
            item.noMelee = true; 
            item.knockBack = 1.3f; 
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 3;   
            item.autoReuse = false;  
            item.shoot = mod.ProjectileType("MagmaShot");   
            item.shootSpeed = 7f; 
			item.crit = 4;
			item.useAmmo = AmmoID.Bullet;
        }
 
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Main.PlaySound(SoundID.Item14, player.position);
			for (int index = 0; index < 10; ++index)
				Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
			for (int index1 = 0; index1 < 10; ++index1)
            {
				int index2 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 6, 0.0f, 0.0f, 100, new Color(), 2.5f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 3f;
				int index3 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
				Main.dust[index3].velocity *= 2f;
            }
			int numberProjectiles = 3; 
			for (int i = 0; i < numberProjectiles; i++)
			{
				type = mod.ProjectileType("MagmaShot");  
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15)); 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false; 
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
    }
}
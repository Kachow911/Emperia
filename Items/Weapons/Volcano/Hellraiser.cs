using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;
 
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
            Item.damage = 19;  
            Item.DamageType = DamageClass.Ranged;    
            Item.width = 42; 
            Item.height = 16;    
            Item.useAnimation = 34;
			Item.useTime = 34;
            Item.useStyle = 5;  
            Item.noMelee = true; 
            Item.knockBack = 1.3f; 
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 3;   
            Item.autoReuse = false;  
            Item.shoot = ModContent.ProjectileType<MagmaShot>();   
            Item.shootSpeed = 7f; 
			Item.crit = 4;
			Item.useAmmo = AmmoID.Bullet;
        }
 
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, player.position);
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
				type = ModContent.ProjectileType<MagmaShot>();  
				Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15)); 
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false; 
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
    }
}
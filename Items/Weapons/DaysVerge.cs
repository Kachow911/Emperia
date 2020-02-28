using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Weapons
{
    public class DaysVerge : ModItem
    {
		private Vector2 SpawnPoint;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Day's Verge");
			Tooltip.SetDefault("Calls divine swords from the heavens\nStriking a foe with the blade will summon an additional sword to smite them");
		}
        public override void SetDefaults()
        {
            item.damage = 26;
            item.melee = true;
            item.width = 36;
            item.height = 36;
            item.useTime = 38;
            item.useAnimation = 38;     
            item.useStyle = 1;
            item.knockBack = 7.5f;
            item.value = 204000;        
            item.rare = 3;
			item.scale = 1f;
			item.UseSound = SoundID.Item18;
			item.shoot = mod.ProjectileType("BlueSword");
			item.shootSpeed = 20f;
            item.useTurn = true;          
        }
		bool canSummon = true;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type1, ref int damage, ref float knockBack)
		{
			
			float speedFactor;
			int damageFactor;
		
			Vector2 placePosition = player.Center + new Vector2((Main.MouseWorld.X - player.Center.X) / 2 + Main.rand.Next(-100, 100), -580);
			Vector2 direction = Main.MouseWorld - placePosition;
			direction.Normalize();
			if (Main.rand.NextBool(2))
			{
				type1 = mod.ProjectileType("BlueSword");
				speedFactor = 24f;
				damageFactor = 2;
			}
			else
			{
				type1 = mod.ProjectileType("PinkSword");
				speedFactor = 9.5f;
				damageFactor = 2;
			}
			int p = Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * speedFactor, direction.Y * speedFactor, type1, damage * damageFactor, 1, Main.myPlayer, 0, 0);
			Main.projectile[p].usesLocalNPCImmunity = false;
			Main.PlaySound(SoundID.Item9, Main.projectile[p].position);
			canSummon = true;
			return false;
		  }
		public override void OnHitNPC (Player player, NPC target, int damage, float knockback, bool crit)
		{
			float speedFactor;
			int damageFactor;
			int type1;
			if (canSummon)
			{
				Vector2 placePosition = player.Center + new Vector2((Main.MouseWorld.X - player.Center.X) / 2 + Main.rand.Next(-100, 100), -580);
				Vector2 direction = Main.MouseWorld - placePosition;
				direction.Normalize();
				if (Main.rand.NextBool(2))
				{
					type1 = mod.ProjectileType("BlueSword");
					speedFactor = 24f;
					damageFactor = 2;
				}
				else
				{
					type1 = mod.ProjectileType("PinkSword");
					speedFactor = 9.5f;
					damageFactor = 2;
				}
				int p = Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * speedFactor, direction.Y * speedFactor, type1, damage * damageFactor, 1, Main.myPlayer, 0, 0);
				Main.projectile[p].usesLocalNPCImmunity = false;
				Main.PlaySound(SoundID.Item9, Main.projectile[p].position);
				canSummon = false;
				target.immune[item.owner] = 0;
			}	
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 234);
                Main.dust[dust].noGravity = true;
			}
		}
		public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "FireBlade", 1); 
			recipe.AddIngredient(ItemID.IceBlade, 1);
			recipe.AddIngredient(ItemID.Starfury, 1);
			recipe.AddIngredient(ItemID.EnchantedSword, 1);
            recipe.AddTile(TileID.Anvils); 			
            recipe.SetResult(this);
            recipe.AddRecipe(); 
			recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "FireBlade", 1); 
			recipe.AddIngredient(ItemID.IceBlade, 1);
			recipe.AddIngredient(ItemID.Starfury, 1);
			recipe.AddIngredient(ItemID.Arkhalis, 1);
            recipe.AddTile(TileID.Anvils); 			
            recipe.SetResult(this);
            recipe.AddRecipe(); 

        }
    }
}

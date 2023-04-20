using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Items.Weapons
{
    public class DaysVerge : ModItem
    {
		private Vector2 SpawnPoint;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Day's Verge");
			// Tooltip.SetDefault("Calls divine swords from the heavens\nStriking a foe with the blade will summon an additional sword to smite them");
		}
        public override void SetDefaults()
        {
            Item.damage = 26;
            Item.DamageType = DamageClass.Melee;
            Item.width = 36;
            Item.height = 36;
            Item.useTime = 38;
            Item.useAnimation = 38;     
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7.5f;
            Item.value = 204000;        
            Item.rare = ItemRarityID.Orange;
			Item.scale = 1f;
			Item.UseSound = SoundID.Item18;
			Item.shoot = ModContent.ProjectileType<BlueSword>();
			Item.shootSpeed = 20f;
            Item.useTurn = true;          
        }
		bool canSummon = true;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			
			float speedFactor;
			int damageFactor;
		
			Vector2 placePosition = player.Center + new Vector2((Main.MouseWorld.X - player.Center.X) / 2 + Main.rand.Next(-100, 100), -580);
			Vector2 direction = Main.MouseWorld - placePosition;
			direction.Normalize();
			if (Main.rand.NextBool(2))
			{
				type = ModContent.ProjectileType<BlueSword>();
				speedFactor = 24f;
				damageFactor = 2;
			}
			else
			{
				type = ModContent.ProjectileType<PinkSword>();
				speedFactor = 9.5f;
				damageFactor = 2;
			}
			int p = Projectile.NewProjectile(source, placePosition.X, placePosition.Y, direction.X * speedFactor, direction.Y * speedFactor, type, damage * damageFactor, 1, Main.myPlayer, 0, 0);
			Main.projectile[p].usesLocalNPCImmunity = false;
			PlaySound(SoundID.Item9, Main.projectile[p].position);
			canSummon = true;
			return false;
		  }
		public override void OnHitNPC (Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			float speedFactor;
			int damageFactor;
			int type;
			if (canSummon)
			{
				Vector2 placePosition = player.Center + new Vector2((Main.MouseWorld.X - player.Center.X) / 2 + Main.rand.Next(-100, 100), -580);
				Vector2 direction = Main.MouseWorld - placePosition;
				direction.Normalize();
				if (Main.rand.NextBool(2))
				{
					type = ModContent.ProjectileType<BlueSword>();
					speedFactor = 24f;
					damageFactor = 2;
				}
				else
				{
					type = ModContent.ProjectileType<PinkSword>();
					speedFactor = 9.5f;
					damageFactor = 2;
				}
				int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), placePosition.X, placePosition.Y, direction.X * speedFactor, direction.Y * speedFactor, type, hit.SourceDamage * damageFactor, 1, Main.myPlayer, 0, 0);
				if (type == ModContent.ProjectileType<PinkSword>()) Main.projectile[p].CritChance += 4;
				Main.projectile[p].usesLocalNPCImmunity = false;
				PlaySound(SoundID.Item9, Main.projectile[p].position);
				canSummon = false;
				target.immune[Item.playerIndexTheItemIsReservedFor] = 0;
			}	
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.BoneTorch);
                Main.dust[dust].noGravity = true;
			}
		}
		public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "FireBlade", 1); 
			recipe.AddIngredient(ItemID.IceBlade, 1);
			recipe.AddIngredient(ItemID.Starfury, 1);
			recipe.AddIngredient(ItemID.EnchantedSword, 1);
            recipe.AddTile(TileID.Anvils); 			
            recipe.Register();
             
			recipe = CreateRecipe();      
            recipe.AddIngredient(null, "FireBlade", 1); 
			recipe.AddIngredient(ItemID.IceBlade, 1);
			recipe.AddIngredient(ItemID.Starfury, 1);
			recipe.AddIngredient(ItemID.Arkhalis, 1);
            recipe.AddTile(TileID.Anvils); 			
            recipe.Register();
             

        }
    }
}

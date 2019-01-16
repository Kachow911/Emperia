using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Weapons //where is located
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
        {    //Sword name
            item.damage = 29;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 200;              //Sword width
            item.height = 200;             //Sword height
            item.useTime = 38;          //how fast 
            item.useAnimation = 38;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 7.5f;      //Sword knockback
            item.value = 204000;        
            item.rare = 3;
			item.scale = 1f;
			item.UseSound = SoundID.Item18;
			item.shoot = mod.ProjectileType("BlueSword");
			item.shootSpeed = 8f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed                 
        }
		 public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type1, ref int damage, ref float knockBack)
		{
			
			float speedFactor;
			float damageFactor;
			
			for (int i = 0; i <= Main.rand.Next(2); i++)
			{
				Vector2 placePosition = player.Center + new Vector2((Main.MouseWorld.X - player.Center.X) / 2 + Main.rand.Next(-100, 100), -500 - 50 * i);
				Vector2 direction = Main.MouseWorld - placePosition;
				direction.Normalize();
				if (Main.rand.NextBool(2))
				{
					type1 = mod.ProjectileType("BlueSword");
					speedFactor = 6.7f;
					damageFactor = 1f;
				}
				else
				{
					type1 = mod.ProjectileType("PinkSword");
					speedFactor = 5.5f;
					damageFactor = 1.2f;
				}
				int p = Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * speedFactor, direction.Y * speedFactor, type1, damage, 1, Main.myPlayer, 0, 0);
				Main.PlaySound(SoundID.Item9, Main.projectile[p].position);

			}
			return false;
		  }
		 public override void OnHitNPC (Player player, NPC target, int damage, float knockback, bool crit)
		{
			float speedFactor;
			float damageFactor;
			int type1;
				Vector2 placePosition = player.Center + new Vector2((Main.MouseWorld.X - player.Center.X) / 2 + Main.rand.Next(-100, 100), -500);
				Vector2 direction = Main.MouseWorld - placePosition;
				direction.Normalize();
				if (Main.rand.NextBool(2))
				{
					type1 = mod.ProjectileType("BlueSword");
					speedFactor = 5f;
					damageFactor = 1.1f;
				}
				else
				{
					type1 = mod.ProjectileType("PinkSword");
					speedFactor = 4.5f;
					damageFactor = 1.25f;
				}
				int p = Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * speedFactor, direction.Y * speedFactor, type1, damage, 1, Main.myPlayer, 0, 0);
				Main.PlaySound(SoundID.Item9, Main.projectile[p].position);
			
		}
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 234);
			}
		}
		public override void AddRecipes()  //How to craft this sword
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
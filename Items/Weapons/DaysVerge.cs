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
			Tooltip.SetDefault("Calls divine swords from the heavens");
		}
        public override void SetDefaults()
        {    //Sword name
            item.damage = 27;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 200;              //Sword width
            item.height = 200;             //Sword height
            item.useTime = 38;          //how fast 
            item.useAnimation = 38;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 7.5f;      //Sword knockback
            item.value = 100;        
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
				Vector2 placePosition = player.Center + new Vector2((Main.MouseWorld.X - player.Center.X) / 2, -470);
				Vector2 direction = Main.MouseWorld - placePosition;
				direction.Normalize();
				if (Main.rand.NextBool(2))
				{
					type1 = mod.ProjectileType("BlueSword");
					speedFactor = 5.5f;
					damageFactor = 1f;
				}
				else
				{
					type1 = mod.ProjectileType("PinkSword");
					speedFactor = 5f;
					damageFactor = 1.2f;
				}
				Projectile.NewProjectile(placePosition.X, player.Center.Y - 400, direction.X * speedFactor, direction.Y * speedFactor, type1, damage, 1, Main.myPlayer, 0, 0);
			}
			return false;
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

        }
    }
}
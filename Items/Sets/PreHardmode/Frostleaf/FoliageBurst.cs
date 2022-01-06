using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Items.Sets.PreHardmode.Frostleaf
{
    public class FoliageBurst : ModItem
    {
		private int notchedArrows = 1;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Foliage Burst");
			Tooltip.SetDefault("Right Click to notch up to 4 arrows");
		}
        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 69;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 5;
            Item.shoot = 3;
            Item.useAmmo = ItemID.WoodenArrow;
            Item.knockBack = 1;
            Item.value = 24000;
            Item.rare = 1;
            Item.autoReuse = false;
            Item.shootSpeed = 4.5f;
        }
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			if (!(player.altFunctionUse == 2))
			{
				PlaySound(SoundID.Item5, player.Center);
				int numberProjectiles = notchedArrows; 
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = Vector2.Zero;
					if (!(numberProjectiles == 1))
						perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians(-10 + i * 20 / numberProjectiles));
					else
						perturbedSpeed = velocity;
					Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * 2, perturbedSpeed.Y * 2, type, damage, knockBack, player.whoAmI);
				}
				notchedArrows = 1;
			}
			return false;  
		}
		public override bool AltFunctionUse(Player player)
		{
			
			if (notchedArrows < 5)
			{
				PlaySound(SoundID.Item5, player.Center);
				Color rgb = new Color(0, 255, 0);
				int index2 = Dust.NewDust(player.position, player.width, player.height, 155, (float) 0, (float) 0, 0, rgb, 0.8f);
			}
			else
			{
				PlaySound(SoundID.MaxMana, player.Center);
			}
			return true;
		}
		/*public override bool? UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				return true;
			}
			return true;
		}*/
		public override bool CanConsumeAmmo(Player player)
		{
			if (!(player.altFunctionUse == 2)) return true;
			else if (notchedArrows < 5) 
			{
				notchedArrows++;
				return true;
			}
			else return false;
		}
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, 0);
        }
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();      
		    recipe.AddIngredient(null, "Frostleaf", 7); 
	        recipe.AddIngredient(ItemID.BorealWood, 15); 			
	        recipe.AddTile(TileID.Anvils);
		    recipe.Register();
	        
		}
    }
}
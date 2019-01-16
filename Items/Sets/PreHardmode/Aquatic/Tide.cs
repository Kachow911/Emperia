using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Aquatic  //where is located
{
    public class Tide : ModItem
    {
		 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tide");
			Tooltip.SetDefault("Enemy hits send waves forward across the ground");
		}
        public override void SetDefaults()
        {   //Sword name
            item.damage = 27;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 16;              //Sword width
            item.height = 16;             //Sword height
            item.useTime = 27;          //how fast 
            item.useAnimation = 27;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3.5f;      //Sword knockback
            item.value = 5000;        
            item.rare = 3;
			item.scale = 1f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed
			item.UseSound = SoundID.Item1; 			
        }
		
        /*public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Seashell, 3);
            recipe.AddIngredient(ItemID.FishingSeaweed, 2); 			
            recipe.AddTile(TileID.WorkBenches); 			//you need 1 DirtBlock  //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
		public override void OnHitNPC (Player player, NPC target, int damage, float knockback, bool crit)
		{
			float velocity;
			if (target.Center.X > player.Center.X)
				velocity = 2;
			else
				velocity = -2;
			Projectile.NewProjectile(target.Center.X, target.Center.Y, velocity, 0, mod.ProjectileType("TideProjOne"), 0, 1, Main.myPlayer, 0, 0);
		}
    }
}
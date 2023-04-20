using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.PreHardmode.Aquatic  //where is located
{
    public class Tide : ModItem
    {
		 public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tide");
			// Tooltip.SetDefault("Enemy hits send water in orbit around you");
		}
        public override void SetDefaults()
        {   //Sword name
            Item.damage = 27;            //Sword damage
            Item.DamageType = DamageClass.Melee;            //if it's melee
            Item.width = 16;              //Sword width
            Item.height = 16;             //Sword height
            Item.useTime = 27;          //how fast 
            Item.useAnimation = 27;     
            Item.useStyle = ItemUseStyleID.Swing;        //Style is how this Item is used, 1 is the style of the sword
            Item.knockBack = 3.5f;      //Sword knockback
            Item.value = 5000;        
            Item.rare = ItemRarityID.Orange;
			Item.scale = 1f;
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.useTurn = true;             //Projectile speed
			Item.UseSound = SoundID.Item1; 			
        }
		
        /*public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Seashell, 3);
            recipe.AddIngredient(ItemID.FishingSeaweed, 2); 			
            recipe.AddTile(TileID.WorkBenches); 			//you need 1 DirtBlock  //at work bench
            recipe.Register();
            
        }*/
		public override void OnHitNPC (Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			float velocity;
			if (target.Center.X > player.Center.X)
				velocity = 2;
			else
				velocity = -2;
			Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X, target.Center.Y, velocity, 0, ModContent.ProjectileType<TideProj2>(), 0, 1, Main.myPlayer, 0, 0);
		}
    }
}
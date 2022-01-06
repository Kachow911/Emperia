using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.TrueDaysVerge;

namespace Emperia.Items.Weapons  //where is located
{
    public class TrueDaysVerge : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Day's Verge");
			Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {   //Sword name
            Item.damage = 89;            //Sword damage
            Item.DamageType = DamageClass.Melee;            //if it's melee
            Item.width = 32;              //Sword width
            Item.height = 32;             //Sword height
            Item.useTime = 33;          //how fast 
            Item.useAnimation = 33;     
            Item.useStyle = 1;        //Style is how this Item is used, 1 is the style of the sword
            Item.knockBack = 5.5f;  
			Item.crit = 4;			//Sword knockback
            Item.value = 100;        
            Item.rare = 5;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<TrueBlueSword>();
			Item.shootSpeed = 10f;
			Item.scale = 1f;
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.useTurn = true;             //Projectile speed                 
        }
		
        public override void AddRecipes()  //How to craft this sword
        {
			Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(null, "DaysVerge", 1); 
			recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
            recipe.AddTile(TileID.MythrilAnvil); 			
            recipe.Register();
             

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				int type = 0;
				if (Main.rand.Next(2) == 0)
					type = 52;
				else
					type = 176;
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, type);
			}
		}
		
        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			if (Main.rand.Next(2) == 0)
					type = ModContent.ProjectileType<TrueBlueSword>();
				else
					type = ModContent.ProjectileType<TruePinkSword>();
			return true;
		}
    }
}
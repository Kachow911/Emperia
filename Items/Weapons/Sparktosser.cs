using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Emperia.Items.Weapons        
{
    public class Sparktosser : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sparktosser");
		}
        public override void SetDefaults()
        {  
            Item.damage = 11;  
            Item.DamageType = DamageClass.Ranged;    
            Item.width = 42; 
            Item.height = 16;    
            Item.useTime = 6;   
            Item.useAnimation = 20;     
            Item.useStyle = 5;  
            Item.noMelee = true; 
            Item.knockBack = 3.25f; 
            Item.UseSound = SoundID.Item34; 
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.rare = 6;   
            Item.autoReuse = true;  
            Item.shoot = 85;   
            Item.shootSpeed = 4.5f; 
            Item.useAmmo = AmmoID.Gel;
			Item.reuseDelay = 20;
        }
 
		public override void AddRecipes()
        {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.IllegalGunParts, 1);
				recipe.AddIngredient(ItemID.Gel, 50);
                recipe.Register();
                
        }
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
    }
}
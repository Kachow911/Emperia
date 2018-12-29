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
			DisplayName.SetDefault("Sparktosser");
		}
        public override void SetDefaults()
        {  
            item.damage = 11;  
            item.ranged = true;    
            item.width = 42; 
            item.height = 16;    
            item.useTime = 6;   
            item.useAnimation = 20;     
            item.useStyle = 5;  
            item.noMelee = true; 
            item.knockBack = 3.25f; 
            item.UseSound = SoundID.Item34; 
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 6;   
            item.autoReuse = true;  
            item.shoot = 85;   
            item.shootSpeed = 4.5f; 
            item.useAmmo = AmmoID.Gel;
			item.reuseDelay = 20;
        }
 
		public override void AddRecipes()
        {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.IllegalGunParts, 1);
				recipe.AddIngredient(ItemID.Gel, 50);
                recipe.SetResult(this);
                recipe.AddRecipe();
        }
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
    }
}
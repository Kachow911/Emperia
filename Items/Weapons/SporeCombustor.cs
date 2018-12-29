using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Emperia.Items.Weapons        
{
    public class SporeCombustor : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spore Combustor");
			Tooltip.SetDefault("Uses gel for ammo\nInflicts Spore Storm, which steals a small amount of life from all afflicted enemies every 2 seconds\nIts spores are more potent at closer ranges");
		}
        public override void SetDefaults()
        {  
            item.damage = 34;  
            item.ranged = true;    
            item.width = 42; 
            item.height = 16;    
            item.useTime = 6;   
            item.useAnimation = 20;     
            item.useStyle = 5;  
            item.noMelee = true; 
            item.knockBack = 0.5f; 
            item.UseSound = SoundID.Item34; 
            item.value = 600000;
            item.rare = 8;   
            item.autoReuse = true;  
            item.shoot = mod.ProjectileType("SporeFlame");   
            item.shootSpeed = 7f; 
            item.useAmmo = AmmoID.Gel;
        }
 
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ShroomiteBar, 14);
			recipe.AddTile(TileID.MythrilAnvil);  
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
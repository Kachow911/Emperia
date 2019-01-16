using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

 
namespace Emperia.Items.Weapons        
{
    public class SporeCombustor : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spore Combustor");
			Tooltip.SetDefault("Uses gel for ammo\nInflicts Spore Storm for 10-30 seconds depending on range\nSteals a small amount of life from all afflicted enemies every 2 seconds");
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
		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.Next(100) > 66;
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
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Glowmasks/SporeCombustorGM"),
				new Vector2
				(
					item.position.X - Main.screenPosition.X + item.width * 0.5f,
					item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White,
				rotation,
				texture.Size() * 0.5f,
				scale, 
				SpriteEffects.None, 
				0f
			);
		}
    }
}
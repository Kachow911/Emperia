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

 
namespace Emperia.Items.Weapons.Twilight  
{
    public class FlowerBlaster : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bouquet Blaster");
		}
        public override void SetDefaults()
        {  
            item.damage = 16;  
            item.ranged = true;    
            item.width = 50; 
            item.height = 28;    
            item.useTime = 16;   
            item.useAnimation = 16;     
            item.useStyle = 5;  
            item.noMelee = true; 
            item.knockBack = 0.75f; 
            item.UseSound = SoundID.Item17; 
            item.value = 33000;
            item.rare = 3;   
            item.autoReuse = true;  
            item.shoot = mod.ProjectileType("PowPetal");   
            item.shootSpeed = 8f; 
        }
	
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
	
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Glowmasks/fbGlowmask"),
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
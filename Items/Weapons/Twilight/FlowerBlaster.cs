using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Emperia.Projectiles;

 
namespace Emperia.Items.Weapons.Twilight  
{
    public class FlowerBlaster : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bouquet Blaster");
		}
        public override void SetDefaults()
        {  
            Item.damage = 16;  
            Item.DamageType = DamageClass.Ranged;    
            Item.width = 50; 
            Item.height = 28;    
            Item.useTime = 16;   
            Item.useAnimation = 16;     
            Item.useStyle = ItemUseStyleID.Shoot;  
            Item.noMelee = true; 
            Item.knockBack = 0.75f; 
            Item.UseSound = SoundID.Item17; 
            Item.value = 33000;
            Item.rare = ItemRarityID.Orange;   
            Item.autoReuse = true;  
            Item.shoot = ModContent.ProjectileType<PowPetal>();   
            Item.shootSpeed = 8f; 
        }
	
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
	
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			//texture = GameContent.TextureAssets.Item[Item.type].Value;
			Main.EntitySpriteDraw
			(
				texture = Mod.Assets.Request<Texture2D>("Glowmasks/fbGlowmask").Value,
				new Vector2
				(
					Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
					Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White,
				rotation,
				texture.Size() * 0.5f,
				scale, 
				SpriteEffects.None, 
				0
			);
		}
    }
}
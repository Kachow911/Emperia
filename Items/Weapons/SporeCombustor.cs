using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Emperia.Projectiles;

 
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
            Item.damage = 34;  
            Item.DamageType = DamageClass.Ranged;    
            Item.width = 42; 
            Item.height = 16;    
            Item.useTime = 6;   
            Item.useAnimation = 20;     
            Item.useStyle = 5;  
            Item.noMelee = true; 
            Item.knockBack = 0.5f; 
            Item.UseSound = SoundID.Item34; 
            Item.value = 600000;
            Item.rare = 8;   
            Item.autoReuse = true;  
            Item.shoot = ModContent.ProjectileType<SporeFlame>();   
            Item.shootSpeed = 7f; 
            Item.useAmmo = AmmoID.Gel;
        }
		public override bool CanConsumeAmmo(Player player)
		{
			return Main.rand.Next(100) > 66;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ShroomiteBar, 14);
			recipe.AddTile(TileID.MythrilAnvil);  
			recipe.Register();
			
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			//texture = GameContent.TextureAssets.Item[Item.type].Value;
			Main.EntitySpriteDraw
			(
				texture = Mod.Assets.Request<Texture2D>("Glowmasks/SporeCombustorGM").Value,
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
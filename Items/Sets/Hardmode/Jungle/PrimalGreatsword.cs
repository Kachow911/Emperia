using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.Hardmode.Jungle  //where is located
{
    public class PrimalGreatsword : ModItem
    {
		 public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Primal Greatsword");
			// Tooltip.SetDefault("Swinging the Sword Generates Primal Bombs Around You");
		}
        public override void SetDefaults()
        {   //Sword name
            Item.damage = 53;            //Sword damage
            Item.DamageType = DamageClass.Melee;            //if it's melee
            Item.width = 16;              //Sword width
            Item.height = 16;             //Sword height
            Item.useTime = 27;          //how fast 
            Item.useAnimation = 27;     
            Item.useStyle = ItemUseStyleID.Swing;        //Style is how this Item is used, 1 is the style of the sword
            Item.knockBack = 3.5f;      //Sword knockback
            Item.value = 100;        
            Item.rare = ItemRarityID.LightRed;
			Item.scale = 1f;
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.useTurn = true;             //Projectile speed
			Item.UseSound = SoundID.Item1; 	
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.shootSpeed = 10f;
        }
		
       public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "JungleMaterial", 5);
			recipe.AddIngredient(ItemID.AdamantiteBar, 2);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			
			recipe = CreateRecipe();
			recipe.AddIngredient(null, "JungleMaterial", 5);
			recipe.AddIngredient(ItemID.TitaniumBar, 2);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
			for (int i = 0; i < 3; i++)
			{
				Projectile.NewProjectile(source, player.Center.X + Main.rand.Next(-300, 300), player.Center.Y + Main.rand.Next(-300, 300), 0, 0, ModContent.ProjectileType<Projectiles.PrimalBomb>(), 75, 1, Main.myPlayer, 60, 0);
			}
			
			return false;
		}
    }
}
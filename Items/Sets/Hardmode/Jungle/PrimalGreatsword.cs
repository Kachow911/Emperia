using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Jungle  //where is located
{
    public class PrimalGreatsword : ModItem
    {
		 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primal Greatsword");
			Tooltip.SetDefault("Swinging the Sword Generates Primal Bombs Around You");
		}
        public override void SetDefaults()
        {   //Sword name
            item.damage = 53;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 16;              //Sword width
            item.height = 16;             //Sword height
            item.useTime = 27;          //how fast 
            item.useAnimation = 27;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3.5f;      //Sword knockback
            item.value = 100;        
            item.rare = 4;
			item.scale = 1f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed
			item.UseSound = SoundID.Item1; 	
			item.shoot = 1;
			item.shootSpeed = 10f;
        }
		
       public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "JungleMaterial", 5);
			recipe.AddIngredient(ItemID.AdamantiteBar, 2);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "JungleMaterial", 5);
			recipe.AddIngredient(ItemID.TitaniumBar, 2);
			recipe.AddIngredient(ItemID.SoulofNight, 2);
			recipe.AddIngredient(ItemID.SoulofLight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			for (int i = 0; i < 3; i++)
			{
				Projectile.NewProjectile(player.Center.X + Main.rand.Next(-300, 300), player.Center.Y + Main.rand.Next(-300, 300), 0, 0, mod.ProjectileType("PrimalBomb"), 75, 1, Main.myPlayer, 60, 0);
			}
			
			return false;
		}
    }
}
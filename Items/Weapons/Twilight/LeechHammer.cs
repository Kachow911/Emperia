using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Twilight
{
    public class LeechHammer : ModItem
    {
		 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leech Hammer");
			Tooltip.SetDefault("Hitting enemies with the hammer generates leech eyes around you");
		}
        public override void SetDefaults()
        {   //Sword name
            item.damage = 24;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 16;              //Sword width
            item.height = 16;             //Sword height
            item.useTime = 27;          //how fast 
            item.useAnimation = 27;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3.5f;      //Sword knockback
            item.value = 100;        
            item.rare = 3;
			item.scale = 1f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed
			item.UseSound = SoundID.Item1; 	
			//item.shoot = 1;
			//item.shootSpeed = 10f;
        }

		/*public override void AddRecipes()
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
		 }*/
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			for (int i = 0; i < 1; i++)
			{
				Projectile.NewProjectile(player.Center.X + Main.rand.Next(-250, 15), player.Center.Y + Main.rand.Next(-300, 300), 0, 0, mod.ProjectileType("LeechEye"), 25, 1, Main.myPlayer, 60, 0);
			}
		}
    }
}
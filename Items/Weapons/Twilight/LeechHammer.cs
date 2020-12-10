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
			Tooltip.SetDefault("Critical hits with the hammer generates leech eyes around you");
		}
        public override void SetDefaults()
        {   //Sword name
            item.damage = 24;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 16;              //Sword width
            item.height = 16;             //Sword height
            item.useTime = 30;          //how fast 
            item.useAnimation = 27;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 6f;      //Sword knockback
            item.value = 100;        
            item.rare = 3;
			item.scale = 1f;
            item.useTurn = true;             //projectile speed
			item.UseSound = SoundID.Item18; 	
			//item.shoot = mod.ProjectileType("LeechEye");
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
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
	
			return false;
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (target.life == target.lifeMax - damage)
			{
				string text1 = target.life.ToString();
				string text2 = target.lifeMax.ToString();
				Projectile.NewProjectile(player.Center.X + Main.rand.Next(-250, 15), player.Center.Y + Main.rand.Next(-300, 300), 0, 0, mod.ProjectileType("LeechEye"), 25, 1, Main.myPlayer, 60, 0);
				Main.PlaySound(SoundID.NPCHit36, target.Center);
			}
			//if (Main.rand.Next(3) == 0)
			//	Projectile.NewProjectile(player.Center.X + Main.rand.Next(-250, 15), player.Center.Y + Main.rand.Next(-300, 300), 0, 0, mod.ProjectileType("LeechEye"), 25, 1, Main.myPlayer, 60, 0);
		}
    }
}
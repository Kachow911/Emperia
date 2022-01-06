using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Twilight;

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
            Item.damage = 24;            //Sword damage
            Item.DamageType = DamageClass.Melee;            //if it's melee
            Item.width = 16;              //Sword width
            Item.height = 16;             //Sword height
            Item.useTime = 30;          //how fast 
            Item.useAnimation = 27;     
            Item.useStyle = 1;        //Style is how this Item is used, 1 is the style of the sword
            Item.knockBack = 6f;      //Sword knockback
            Item.value = 100;        
            Item.rare = 3;
			Item.scale = 1f;
            Item.useTurn = true;             //Projectile speed
			Item.UseSound = SoundID.Item18; 	
			//Item.shoot = ModContent.ProjectileType<LeechEye>();
			//Item.shootSpeed = 10f;
        }

		/*public override void AddRecipes()
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
			 
		 }*/
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
	
			return false;
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (target.life == target.lifeMax - damage)
			{
				string text1 = target.life.ToString();
				string text2 = target.lifeMax.ToString();
				Projectile.NewProjectile(player.GetProjectileSource_Item(Item), player.Center.X + Main.rand.Next(-250, 15), player.Center.Y + Main.rand.Next(-300, 300), 0, 0, ModContent.ProjectileType<LeechEye>(), 25, 1, Main.myPlayer, 60, 0);
				Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCHit36, target.Center);
			}
			//if (Main.rand.Next(3) == 0)
			//	Projectile.NewProjectile(source, player.Center.X + Main.rand.Next(-250, 15), player.Center.Y + Main.rand.Next(-300, 300), 0, 0, ModContent.ProjectileType<LeechEye>(), 25, 1, Main.myPlayer, 60, 0);
		}
    }
}
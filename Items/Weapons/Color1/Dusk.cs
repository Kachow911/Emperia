using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Color1   //where is located
{
    public class Dusk : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dusk");
			Tooltip.SetDefault("Striking an enemy will give you the 'Indigo Inertia' Buff");
		}
        public override void SetDefaults()
        {   //Sword name
            item.damage = 142;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 32;              //Sword width
            item.height = 32;             //Sword height
            item.useTime = 33;          //how fast 
            item.useAnimation = 33;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 5.5f;  
			item.crit = 4;			//Sword knockback
            item.value = 100;        
            item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("DuskProj");
			item.shootSpeed = 16f;
			item.scale = 1f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed                 
        }
		
        public override void AddRecipes()  //How to craft this sword
        {
			ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "CeruleanClaymore", 1); 
			recipe.AddIngredient(null, "IndigoIaito", 1); 
			recipe.AddRecipeGroup("Emperia:PalBar", 4);
			recipe.AddRecipeGroup("Emperia:AdBar", 4);
			recipe.AddIngredient(ItemID.HallowedBar, 2); 
			recipe.AddIngredient(ItemID.SoulofLight, 4); 
			recipe.AddIngredient(ItemID.SoulofSight, 4); 
			recipe.AddIngredient(ItemID.CrystalShard, 20);
						
            recipe.AddTile(TileID.MythrilAnvil); 			
            recipe.SetResult(this);
            recipe.AddRecipe(); 

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 21);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			player.AddBuff(mod.BuffType("IndigoIntensity"), Main.rand.Next(420, 600));
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type1, ref int damage, ref float knockBack)
		{
			damage = damage / 2;
			return true;
		}
    }
}
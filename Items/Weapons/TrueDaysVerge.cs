using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons  //where is located
{
    public class TrueDaysVerge : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Day's Verge");
			Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {   //Sword name
            item.damage = 89;            //Sword damage
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
			item.shoot = mod.ProjectileType("TrueBlueSword");
			item.shootSpeed = 10f;
			item.scale = 1f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed                 
        }
		
        public override void AddRecipes()  //How to craft this sword
        {
			ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "DaysVerge", 1); 
			recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
            recipe.AddTile(TileID.MythrilAnvil); 			
            recipe.SetResult(this);
            recipe.AddRecipe(); 

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				int type = 0;
				if (Main.rand.Next(2) == 0)
					type = 52;
				else
					type = 176;
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, type);
			}
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type1, ref int damage, ref float knockBack)
		{
			if (Main.rand.Next(2) == 0)
					type1 = mod.ProjectileType("TrueBlueSword");
				else
					type1 = mod.ProjectileType("TruePinkSword");
			return true;
		}
    }
}
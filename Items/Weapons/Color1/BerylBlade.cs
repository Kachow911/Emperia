using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Color1   //where is located
{
    public class BerylBlade : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beryl Blade");
			Tooltip.SetDefault("Striking an enemy will increase melee speed for a short time");
		}
        public override void SetDefaults()
        {    //Sword name
            item.damage = 79;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 32;              //Sword width
            item.height = 32;             //Sword height
            item.useTime = 36;          //how fast 
            item.useAnimation = 36;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3.5f;  
            item.value = 100;        
            item.rare = 3;
			item.scale = 1f;
            item.autoReuse = true;
			item.UseSound = SoundID.Item1;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed                 
        }
		
        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Prism", 1); 
			recipe.AddIngredient(ItemID.Emerald, 8); 
			recipe.AddIngredient(ItemID.LimeKelp, 1); 
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
		 public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 61);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			player.AddBuff(mod.BuffType("BerylBrutalism"), Main.rand.Next(360, 600));
		}
    }
}
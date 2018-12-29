using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Color1  //where is located
{
    public class SaffronSaber : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Saffron Sabre");
			Tooltip.SetDefault("Striking an enemy will increase movement speed for a short time");
		}
        public override void SetDefaults()
        {     //Sword name
            item.damage = 41;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 32;              //Sword width
            item.height = 32;             //Sword height
            item.useTime = 17;          //how fast 
            item.useAnimation = 17;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 1.5f;  
			item.crit = 4;			//Sword knockback
            item.value = 100;        
            item.rare = 3;
			item.scale = 1f;
			item.UseSound = SoundID.Item1;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed                 
        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 64);
			}
		}
        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Prism", 1); 
			recipe.AddIngredient(ItemID.Topaz, 8); 
			recipe.AddIngredient(ItemID.YellowMarigold, 1); 
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			player.AddBuff(mod.BuffType("SaffronSadism"), Main.rand.Next(360, 600));
		}
    }
}
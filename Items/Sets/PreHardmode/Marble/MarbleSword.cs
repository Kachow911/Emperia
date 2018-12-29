using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Marble   //where is located
{
    public class MarbleSword : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marble Sword");
			Tooltip.SetDefault("Killing an enemy regenerates you 10 life");
		}
        public override void SetDefaults()
        {    //Sword name
            item.damage = 14;            
            item.melee = true;            
            item.width = 16;              
            item.height = 16;             
            item.useTime = 18;          //how fast 
            item.useAnimation = 18;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3;      //Sword knockback
            item.value = 100;        
            item.rare = 2;
			item.scale = 1.15f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true; 
			item.UseSound = SoundID.Item1;               
        }
		public override void OnHitNPC (Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (target.life <= 0)
			{
				int num622 = Dust.NewDust(new Vector2(target.position.X + target.width / 2, target.position.Y + target.height / 2), 1, 1, 15, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
				player.statLife += 10;
				player.HealEffect(10);
			}
		}
        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "MarbleBar", 8); 
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
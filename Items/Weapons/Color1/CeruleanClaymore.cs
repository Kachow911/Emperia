using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Color1   //where is located
{
    public class CeruleanClaymore : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cerulean Claymore");
			Tooltip.SetDefault("Striking an enemy will increase melee damage for a short time");
		}
        public override void SetDefaults()
        {     //Sword name
            item.damage = 76;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 32;              //Sword width
            item.height = 32;             //Sword height
            item.useTime = 28;          //how fast 
            item.useAnimation = 28;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 3.5f;  
			item.crit = 4;			//Sword knockback
            item.value = 100;        
            item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.scale = 1f;
            item.autoReuse = false;   //if it's capable of autoswing.
            item.useTurn = true;             //projectile speed                 
        }
		
        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Prism", 1); 
			recipe.AddIngredient(ItemID.Sapphire, 8); 
			recipe.AddIngredient(ItemID.BlueBerries, 1); 
            recipe.AddTile(TileID.Anvils); 			//you need 1 DirtBlock  //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 59);
			}
		}
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            if (target.boss)
                player.AddBuff(mod.BuffType("CeruleanCharge"), Main.rand.Next(360, 600) + 300);
            else
                 player.AddBuff(mod.BuffType("CeruleanCharge"), Main.rand.Next(360, 600));
		}
    }
}
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Items.Weapons.Twilight
{
    public class Toothslitter : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Toothslitter");
			Tooltip.SetDefault("Hitting 3 consecutive attacks with this blade makes your next attack ignore 2 extra immune frames of the enemy");
		}


        public override void SetDefaults()
        {
            item.damage = 24;
            item.useTime = 36;
            item.useAnimation = 36;
            item.melee = true;            
            item.width = 32;              
            item.height = 32;             
            item.useStyle = 1;        
            item.knockBack = 3.75f;
            item.value = 258000;
            item.crit = 6;
            item.rare = 3;
            item.UseSound = SoundID.Item1;   
            item.autoReuse = false;
            item.useTurn = true;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 5);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0f;

            }
        }
       
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Muramasa, 1);
            recipe.AddIngredient(ItemID.BladeofGrass, 1);
            recipe.AddIngredient(ItemID.FieryGreatsword, 1);
            recipe.AddIngredient(ItemID.BloodButcherer, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

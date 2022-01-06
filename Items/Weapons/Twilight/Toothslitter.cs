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
            Item.damage = 24;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.DamageType = DamageClass.Melee;            
            Item.width = 32;              
            Item.height = 32;             
            Item.useStyle = 1;        
            Item.knockBack = 3.75f;
            Item.value = 258000;
            Item.crit = 6;
            Item.rare = 3;
            Item.UseSound = SoundID.Item1;   
            Item.autoReuse = false;
            Item.useTurn = true;
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
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Muramasa, 1);
            recipe.AddIngredient(ItemID.BladeofGrass, 1);
            recipe.AddIngredient(ItemID.FieryGreatsword, 1);
            recipe.AddIngredient(ItemID.BloodButcherer, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
            
        }
    }
}

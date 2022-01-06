using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Items.Sets.Hardmode.Stratos
{
    public class StratosBroadsword : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stratos Broadsword");
			Tooltip.SetDefault("Hitting enemies sends rock shards flying");
		}


        public override void SetDefaults()
        {
            Item.damage = 54;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.DamageType = DamageClass.Melee;            
            Item.width = 60;              
            Item.height = 66;             
            Item.useStyle = 1;        
            Item.knockBack = 5f;
            Item.value = 258000;
            Item.crit = 6;
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;   
            Item.autoReuse = true;
            Item.useTurn = false;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {

        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 180);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0f;

            }
        }



        /*public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Muramasa, 1);
            recipe.AddIngredient(ItemID.BladeofGrass, 1);
            recipe.AddIngredient(ItemID.FieryGreatsword, 1);
            recipe.AddIngredient(ItemID.BloodButcherer, 1);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
            
        }*/
    }
}

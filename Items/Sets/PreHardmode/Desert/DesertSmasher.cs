using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Desert
{
    public class DesertSmasher : ModItem
    {
        int crits = 0;
		 public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Smasher");
			Tooltip.SetDefault("Every third crit causes desert spikes to rise foward and attack other enemies");
		}
        public override void SetDefaults()
        {    //Sword name
            item.damage = 20;            //Sword damage
            item.melee = true;            //if it's melee
            item.width = 56;              //Sword width
            item.height = 66;             //Sword height
            item.useTime = 30;          //how fast 
            item.useAnimation = 30;     
            item.useStyle = 1;        //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 8f;      //Sword knockback
            item.value = 1000;        
            item.rare = 2;
			item.scale = 1f;
            item.autoReuse = true;   //if it's capable of autoswing.
            item.useTurn = true;             //player speed
			item.UseSound = SoundID.Item1; 			
        }
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (crit)
                crits++;
			if (crits >= 3)
            {
                crits = 0;
                Vector2 perturbedSpeed;
                if (target.Center.X > player.Center.X)
                    perturbedSpeed = new Vector2(2, 0);
                else
                    perturbedSpeed = new Vector2(-2, 0);
                Projectile.NewProjectile(player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("CarapaceCrusherProj1"), damage, 1, Main.myPlayer, 0, 0);
            }
        
		}
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AridScale", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}

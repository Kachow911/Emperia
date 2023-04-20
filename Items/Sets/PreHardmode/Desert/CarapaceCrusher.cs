using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Desert;

namespace Emperia.Items.Sets.PreHardmode.Desert
{
    public class CarapaceCrusher : ModItem
    {
		 public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Carapace Crusher");
			// Tooltip.SetDefault("Killing enemies causes desert spikes to rise foward and attack other enemies");
		}
        public override void SetDefaults()
        {    //Sword name
            Item.damage = 19;            //Sword damage
            Item.DamageType = DamageClass.Melee;            //if it's melee
            Item.width = 56;              //Sword width
            Item.height = 66;             //Sword height
            Item.useTime = 26;          //how fast 
            Item.useAnimation = 26;     
            Item.useStyle = 1;        //Style is how this Item is used, 1 is the style of the sword
            Item.knockBack = 2f;      //Sword knockback
            Item.value = 1000;        
            Item.rare = 2;
			Item.scale = 1f;
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.useTurn = true;             //player speed
			Item.UseSound = SoundID.Item1; 			
        }
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
			
			if (target.life <= 0)
            {
                Vector2 perturbedSpeed;
                if (target.Center.X > player.Center.X)
                    perturbedSpeed = new Vector2(2, 0);
                else
                    perturbedSpeed = new Vector2(-2, 0);
                //Projectile.NewProjectile(source, player.Center.X, player.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<CarapaceCrusherProj1>(), damage, 1, Main.myPlayer, 0, 0);
            }
        
		}
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AridScale", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Frostleaf
{
    public class Coniferocious : ModItem //make grenade actually do splash damage
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coniferocious");
			Tooltip.SetDefault("Occasionally throws out a pinecone grenade");
		}
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
			item.damage = 14;
			item.melee = true;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.noMelee = true;
			item.useTime = 18;
            item.useAnimation = 18;
            item.useTurn = true;
            item.autoReuse = false;
			item.knockBack = 6f;
            item.value = 24000;        
            item.rare = 1;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("ConiferociousProj");
            item.shootSpeed = 9f;
            item.UseSound = SoundID.Item1;
        }
        int pineconeCharge = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            pineconeCharge++;
            if(pineconeCharge >= 4)
            {
                Vector2 perturbedSpeed = new Vector2(speedX * 1.4f, speedY * 1.4f);
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("PineconeGrenade"), damage * 2, knockBack + 1f, player.whoAmI);
                pineconeCharge = 0;
            }
            return true;
        }
		public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 250; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "Frostleaf", 7); 
            recipe.AddIngredient(ItemID.BorealWood, 15); 			
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}
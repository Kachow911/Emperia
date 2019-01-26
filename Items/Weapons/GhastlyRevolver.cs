using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons
{
    public class GhastlyRevolver : ModItem
    {
		float lastCount = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ghastly Revolver");
			Tooltip.SetDefault("Generates haunted pistols around you as you shoot");
		}


        public override void SetDefaults()
        {
            item.damage = 62;  
            item.ranged = true;
            item.width = 42;     
            item.height = 22;    
            item.useTime = 31;
            item.useAnimation = 31;
            item.useStyle = 5;    
            item.noMelee = true; 
            item.knockBack = 4;
            item.useTurn = false;
            item.value = Terraria.Item.sellPrice(0, 2, 0, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item36;
            item.autoReuse = true;
            item.shoot = 10; 
            item.shootSpeed = 9.5f;
            item.useAmmo = AmmoID.Bullet;
        }
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (Main.rand.Next(4) == 0)
			{
			int count= 0;
            for (int i = 0; i < 255; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == mod.ProjectileType("HauntedRevolver"))
                {
                    count++;
					
                }
            }
			if (lastCount + 1 > 4)
				lastCount = 0;
			if (count < 4)
			{
				Projectile.NewProjectile(player.Center.X - 32, player.Center.Y - 32, 0, 0, mod.ProjectileType("HauntedRevolver"), 0, 0, player.whoAmI, ai1: lastCount + 1);
				lastCount++;
			}
			}
			return true;
        }
		
		/*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Material", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }*/
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}
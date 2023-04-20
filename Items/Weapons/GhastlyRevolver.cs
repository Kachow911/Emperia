using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Emperia.Projectiles;
using Terraria.DataStructures;

namespace Emperia.Items.Weapons
{
    public class GhastlyRevolver : ModItem
    {
		float lastCount = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ghastly Revolver");
			// Tooltip.SetDefault("Generates haunted pistols around you as you shoot");
		}


        public override void SetDefaults()
        {
            Item.damage = 62;  
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;     
            Item.height = 22;    
            Item.useTime = 31;
            Item.useAnimation = 31;
            Item.useStyle = 5;    
            Item.noMelee = true; 
            Item.knockBack = 4;
            Item.useTurn = false;
            Item.value = Terraria.Item.sellPrice(0, 2, 0, 0);
            Item.rare = 4;
            Item.UseSound = SoundID.Item36;
            Item.autoReuse = true;
            Item.shoot = 10; 
            Item.shootSpeed = 9.5f;
            Item.useAmmo = AmmoID.Bullet;
        }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
			if (Main.rand.Next(4) == 0)
			{
			int count= 0;
            for (int i = 0; i < 255; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == ModContent.ProjectileType<HauntedRevolver>())
                {
                    count++;
					
                }
            }
			if (lastCount + 1 > 4)
				lastCount = 0;
			if (count < 4)
			{
				Projectile.NewProjectile(source, player.Center.X - 32, player.Center.Y - 32, 0, 0, ModContent.ProjectileType<HauntedRevolver>(), 0, 0, player.whoAmI, ai1: lastCount + 1);
				lastCount++;
			}
			}
			return true;
        }
		
		/*public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Material", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 1);
            
        }*/
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}
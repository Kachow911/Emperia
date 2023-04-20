using Terraria;
using System;
using Terraria.ID;
using Terraria.DataStructures;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.PreHardmode.Seashell
{
    public class SeashellBow : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Seashell Bow");
            // Tooltip.SetDefault("Wooden arrows turn into a burst of coral");
		}
        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 69;
            Item.height = 40;
		 	Item.useAnimation = 22;
			Item.useTime = 22;
            Item.useStyle = 5;
            Item.shoot = 3;
            Item.useAmmo = ItemID.WoodenArrow;
            Item.knockBack = 1;
            Item.value = 22000;
            Item.rare = 1;
            Item.autoReuse = false;
            Item.shootSpeed = 11f;
			Item.UseSound = SoundID.Item5; 

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();      
            recipe.AddIngredient(ItemID.Seashell, 4);
            recipe.AddIngredient(ItemID.Coral, 4);
            recipe.AddIngredient(null, "SeaCrystal", 1); 		
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<CoralBurstMain>();
                damage = (int)(damage * 0.70f);
            }
        }
		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
            if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = ModContent.ProjectileType<CoralBurstMain>();
                damage = (int)(damage * 0.70f);
			}
			return true;  
		}*/
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 0);
		}
    }
}

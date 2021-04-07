using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Seashell
{
    public class SeashellBow : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seashell Bow");
            Tooltip.SetDefault("Wooden arrows turn into a burst of coral");
		}
        public override void SetDefaults()
        {
            item.damage = 13;
            item.noMelee = true;
            item.ranged = true;
            item.width = 69;
            item.height = 40;
		 	item.useAnimation = 22;
			item.useTime = 22;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = ItemID.WoodenArrow;
            item.knockBack = 1;
            item.value = 24000;
            item.rare = 1;
            item.autoReuse = false;
            item.shootSpeed = 11f;
			item.UseSound = SoundID.Item5; 

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(ItemID.Seashell, 4);
            recipe.AddIngredient(ItemID.Coral, 4);
            recipe.AddIngredient(null, "SeaCrystal", 1); 		
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = mod.ProjectileType("CoralBurstMain");
                damage = (int)(damage * 0.70f);
			}
			return true;  
		}
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, 0);
		}
    }
}

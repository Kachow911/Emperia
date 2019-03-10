using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Vulcan
{
    public class VulcanCrossbow : ModItem
    {
        private int bulletLoadeds = 1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vulcan Crossbow");
            Tooltip.SetDefault("After dealing 500 damage, you may left click to launch a barrage of rockets");
        }
        public override void SetDefaults()
        {
            item.damage = 50;
            item.noMelee = true;
            item.ranged = true;
            item.width = 69;
            item.height = 40;
            item.useTime = 40;
            item.useAnimation = 40;
            item.useStyle = 5;
            item.knockBack = 1;
            item.value = 1000;
            item.rare = 3;
            item.scale = 0.7f;
            item.autoReuse = false;
            item.shootSpeed = 10f;
            item.crit = 10;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 5;
                item.useAnimation = 15;
                item.useTime = 5;
                item.reuseDelay = 14;
                item.damage = 75;
                item.shoot = mod.ProjectileType("VulcanRocket");
            }
            else
            {
                item.useStyle = 5;
                item.useTime = 32;
                item.useAnimation = 32;
                item.damage = 44;
                item.shoot = 3;
                item.shootSpeed = 15f;
                item.useAmmo = ItemID.WoodenArrow;
            }
            return base.CanUseItem(player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            return bulletLoadeds > 0;
        }
        /*public override void AddRecipes()
    {
        ModRecipe recipe = new ModRecipe(mod);
        recipe.AddIngredient(null, "GraniteBar", 8);
        recipe.AddTile(TileID.Anvils);
        recipe.SetResult(this);
        recipe.AddRecipe();
    }*/
    }
}
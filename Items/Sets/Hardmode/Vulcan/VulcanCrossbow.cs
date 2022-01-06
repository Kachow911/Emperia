using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Emperia.Projectiles;

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
            Item.damage = 50;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 69;
            Item.height = 40;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = 5;
            Item.knockBack = 1;
            Item.value = 1000;
            Item.rare = 3;
            Item.scale = 0.7f;
            Item.autoReuse = false;
            Item.shootSpeed = 10f;
            Item.crit = 10;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = 5;
                Item.useAnimation = 15;
                Item.useTime = 5;
                Item.reuseDelay = 14;
                Item.damage = 75;
                Item.shoot = ModContent.ProjectileType<VulcanRocket>();
            }
            else
            {
                Item.useStyle = 5;
                Item.useTime = 32;
                Item.useAnimation = 32;
                Item.damage = 44;
                Item.shoot = 3;
                Item.shootSpeed = 15f;
                Item.useAmmo = ItemID.WoodenArrow;
            }
            return base.CanUseItem(player);
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            return bulletLoadeds > 0;
        }
        /*public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "GraniteBar", 8);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
        
    }*/
    }
}
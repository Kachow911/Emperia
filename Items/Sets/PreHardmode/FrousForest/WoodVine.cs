using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.FrousForest
{
    public class Woodvine : ModItem
    {
		int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Woodvine");
			Tooltip.SetDefault("Turns wooden arrows into piercing leaves");
		}
        public override void SetDefaults()
        {
            item.damage = 17;
            item.noMelee = true;
            item.ranged = true;
            item.width = 69;
            item.height = 40;
            item.useTime = 29;
            item.useAnimation = 29;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = ItemID.WoodenArrow;
            item.knockBack = 1;
            item.value = 22500;
            item.rare = 2;
            item.autoReuse = false;
            item.shootSpeed = 8f;
			item.UseSound = SoundID.Item5; 
        }
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = mod.ProjectileType("VineLeaf");
            }
            return true;
			
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
        
    }
}
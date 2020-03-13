using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Twilight
{
    public class LeechBow : ModItem
    {
		int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leech Bow");
			Tooltip.SetDefault("Turns wooden arrows into leech arrows that occasionally pierce through enemies, doubling their damage");
		}
        public override void SetDefaults()
        {
            item.damage = 23;
            item.noMelee = true;
            item.ranged = true;
            item.width = 69;
            item.height = 40;
            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = ItemID.WoodenArrow;
            item.knockBack = 1;
            item.value = 24000;
            item.rare = 3;
            item.autoReuse = false;
            item.shootSpeed = 8f;
			item.UseSound = SoundID.Item5; 
        }
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = mod.ProjectileType("LeechArrow");
            }
            return true;
			
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
        
    }
}
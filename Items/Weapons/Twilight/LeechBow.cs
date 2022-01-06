using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Twilight;

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
            Item.damage = 23;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 69;
            Item.height = 40;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = 5;
            Item.shoot = 3;
            Item.useAmmo = ItemID.WoodenArrow;
            Item.knockBack = 1;
            Item.value = 24000;
            Item.rare = 3;
            Item.autoReuse = false;
            Item.shootSpeed = 8f;
			Item.UseSound = SoundID.Item5; 
        }
		
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<LeechArrow>();
            }
            return true;
			
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
        
    }
}
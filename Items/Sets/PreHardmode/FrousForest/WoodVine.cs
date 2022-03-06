using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles;

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
            Item.damage = 17;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 69;
            Item.height = 40;
            Item.useTime = 29;
            Item.useAnimation = 29;
            Item.useStyle = 5;
            Item.shoot = 3;
            Item.useAmmo = ItemID.WoodenArrow;
            Item.knockBack = 1;
            Item.value = 22500;
            Item.rare = 2;
            Item.autoReuse = false;
            Item.shootSpeed = 8f;
			Item.UseSound = SoundID.Item5; 
        }
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<VineLeaf>();
            }
            return true;
			
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
        
    }
}
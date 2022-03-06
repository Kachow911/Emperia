using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Emperia.Items.Sets.Hardmode.Ethereal
{
    public class InquisitorBow : ModItem
    {
		//int counter = 0;
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Granite Bow");
			Tooltip.SetDefault("Turns wooden arrows into ethereal bolts");
		}
        public override void SetDefaults()
        {
            Item.damage = 38;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 40;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = 5;
            Item.shoot = 3;
            Item.useAmmo = ItemID.WoodenArrow;
            Item.knockBack = 1;
            Item.value = 22500;
            Item.rare = 4;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
			Item.UseSound = SoundID.Item5; 
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<Projectiles.Ethereal.EtherealArrow>();
            }
			Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-20, 20)));
			velocity = perturbedSpeed;
			return true;

		}


		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}

		public override void AddRecipes()
		{
			
		}
	}
}

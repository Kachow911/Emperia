using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

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
            item.damage = 38;
            item.noMelee = true;
            item.ranged = true;
            item.width = 30;
            item.height = 40;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = ItemID.WoodenArrow;
            item.knockBack = 1;
            item.value = 22500;
            item.rare = 4;
            item.autoReuse = true;
            item.shootSpeed = 12f;
			item.UseSound = SoundID.Item5; 
        }

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = mod.ProjectileType("EtherealArrow");
            }
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-20, 20)));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
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

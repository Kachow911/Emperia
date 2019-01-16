using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Skeletron
{
    public class Skelebow : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Skelebow");
			Tooltip.SetDefault("Has a 33% chance to fire a shadow bolt instead\n Shadow bows penetrate enemies and explode");
		}
        public override void SetDefaults()
        {
            item.damage = 25;
            item.noMelee = true;
            item.ranged = true;
            item.width = 69;
            item.height = 40;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = ItemID.WoodenArrow;
            item.knockBack = 1;
            item.value = 5000;
            item.rare = 2;
            item.autoReuse = true;
            item.shootSpeed = 15f;
			item.UseSound = SoundID.Item5; 
        }
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.rand.Next(3) == 2)
			{
				int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("ShadowBolt"), damage, knockBack, player.whoAmI);
				Main.projectile[p].penetrate = 2;
				return false;
			}
			return true;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
    }
}
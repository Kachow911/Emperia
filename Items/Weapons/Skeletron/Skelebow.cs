using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles;

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
            Item.damage = 25;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 69;
            Item.height = 40;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.shoot = 3;
            Item.useAmmo = ItemID.WoodenArrow;
            Item.knockBack = 1;
            Item.value = 5000;
            Item.rare = 2;
            Item.autoReuse = true;
            Item.shootSpeed = 15f;
			Item.UseSound = SoundID.Item5; 
        }
		
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			if (Main.rand.Next(3) == 2)
			{
				int p = Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<ShadowBolt>(), damage, knockBack, player.whoAmI);
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
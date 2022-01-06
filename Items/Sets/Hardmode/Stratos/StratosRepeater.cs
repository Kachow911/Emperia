using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Emperia.Projectiles.Stratos;
using Terraria.DataStructures;

namespace Emperia.Items.Sets.Hardmode.Stratos
{
    public class StratosRepeater : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stratos Repeater");
			Tooltip.SetDefault("Bullets fired using the gun inflict Crushing Freeze, no matter the type");
		}
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 24;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = 5;
            Item.shoot = 10;
            Item.useAmmo = AmmoID.Bullet;
            Item.knockBack = 1;
            Item.value = 22500;
            Item.rare = 2;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
			Item.UseSound = SoundID.Item5; 
        }
		
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
            Vector2 placePosition = player.Center + new Vector2(Main.rand.Next(-100, 100), -player.height - Main.rand.Next(50));
            Vector2 direction = Main.MouseWorld - placePosition;
            direction.Normalize();
            if (Main.rand.NextBool(5))
            {
                //type = ModContent.ProjectileType<StratosMeteorite>();
                Projectile.NewProjectile(source, placePosition.X, placePosition.Y, direction.X * 12f, direction.Y * 12f, ModContent.ProjectileType<StratosMeteorite>(), damage, knockBack, Main.myPlayer, 0, 0);
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

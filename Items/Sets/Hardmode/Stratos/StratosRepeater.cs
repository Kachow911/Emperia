using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

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
            item.damage = 18;
            item.noMelee = true;
            item.ranged = true;
            item.width = 42;
            item.height = 24;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.knockBack = 1;
            item.value = 22500;
            item.rare = 2;
            item.autoReuse = true;
            item.shootSpeed = 12f;
			item.UseSound = SoundID.Item5; 
        }
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            Vector2 placePosition = player.Center + new Vector2(Main.rand.Next(-100, 100), -player.height - Main.rand.Next(50));
            Vector2 direction = Main.MouseWorld - placePosition;
            direction.Normalize();
            if (Main.rand.NextBool(5))
            {
                //type = mod.ProjectileType("StratosMeteorite");
                Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * 12f, direction.Y * 12f, mod.ProjectileType("StratosMeteorite"), damage, knockBack, Main.myPlayer, 0, 0);
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

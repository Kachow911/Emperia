using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;
using Emperia.Projectiles;
namespace Emperia.Items.Weapons.Inquisitor
{
    public class PuppeteerPistol : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Puppeteer's Pistol");
			// Tooltip.SetDefault("Fires and explosive buckshot every three shots");
		}
        int count;

        public override void SetDefaults()
        {
            Item.damage = 42;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 32;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 2.5f;
            Item.useTurn = false;
            Item.value = Terraria.Item.sellPrice(0, 1, 32, 0);
            Item.rare = 4;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = 10;
            Item.shootSpeed = 8f;
            Item.useAmmo = AmmoID.Bullet;
        }
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            count++;
            if (count >= 3)
            {
                {
                    Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 2f, velocity.Y * 2f, ModContent.ProjectileType<PuppetShot>(), damage, knockBack, player.whoAmI, 0f, 0f);
                }
                count = 0;
				return false;
            }
            return true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
namespace Emperia.Items.Weapons.Inquisitor
{
    public class PuppeteerPistol : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Puppeteer's Pistol");
			Tooltip.SetDefault("Fires and explosive buckshot every three shots");
		}
        int count;

        public override void SetDefaults()
        {
            item.damage = 42;
            item.ranged = true;
            item.width = 50;
            item.height = 32;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2.5f;
            item.useTurn = false;
            item.value = Terraria.Item.sellPrice(0, 1, 32, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 8f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            count++;
            if (count >= 3)
            {
                {
                    Projectile.NewProjectile(position.X, position.Y, speedX * 2, speedY * 2, mod.ProjectileType("PuppetShot"), damage, knockBack, player.whoAmI, 0f, 0f);
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
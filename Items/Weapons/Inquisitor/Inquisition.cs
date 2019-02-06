using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
namespace Emperia.Items.Weapons.Inquisitor
{
    public class Inquisition : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Inquisition");
			Tooltip.SetDefault("Fires a spread of blades");
		}
        int count;

        public override void SetDefaults()
        {
            item.damage = 32;
            item.melee = true;
            item.width = 50;
            item.height = 32;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 2.5f;
            item.useTurn = true;
            item.value = Terraria.Item.sellPrice(0, 1, 32, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("InquisitionBlade");
            item.shootSpeed = 11f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(-6 + 12 * i));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        
    }
}
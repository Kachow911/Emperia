using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Scoria
{
    public class ScoriaBow : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scoria Bow");
			Tooltip.SetDefault("Arrowa fired using the bow explode, no matter the type");
		}
        public override void SetDefaults()
        {
            item.damage = 20;
            item.noMelee = true;
            item.ranged = true;
            item.width = 69;
            item.height = 40;
            item.useTime = 29;
            item.useAnimation = 29;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = ItemID.WoodenArrow;
            item.knockBack = 1;
            item.value = 22500;
            item.rare = 2;
            item.autoReuse = true;
            item.shootSpeed = 12f;
			item.UseSound = SoundID.Item5; 
        }
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			Main.projectile[p].GetGlobalProjectile<MyProjectile>(mod).scoriaExplosion = true;
			return false;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	
    }
}
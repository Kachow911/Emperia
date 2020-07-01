using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons.Twilight
{
    public class Anastasia : ModItem
    {
		int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Anastasia");
			Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {
			item.mana = 10;
			item.damage = 18;
			item.useStyle = 5;
			item.shootSpeed = 32f;
			item.shoot = mod.ProjectileType("AnastasiaP1");
			item.width = 26;
			item.height = 28;
			item.UseSound = SoundID.Item8;
			item.useAnimation = 25;
			item.useTime = 25;
			item.autoReuse = true;
			item.rare = 7;
			item.noMelee = true;
			item.knockBack = 1f;
			item.value = 200000;
			item.magic = true;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			//int num53 = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player, 0f, 0f);
			return true;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
        
    }
}
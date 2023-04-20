using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Twilight;

namespace Emperia.Items.Weapons.Twilight
{
    public class Anastasia : ModItem
    {
		int counter = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Anastasia");
			// Tooltip.SetDefault("");
		}
        public override void SetDefaults()
        {
			Item.mana = 10;
			Item.damage = 18;
			Item.useStyle = 5;
			Item.shootSpeed = 32f;
			Item.shoot = ModContent.ProjectileType<AnastasiaP1>();
			Item.width = 26;
			Item.height = 28;
			Item.UseSound = SoundID.Item8;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.autoReuse = true;
			Item.rare = 7;
			Item.noMelee = true;
			Item.knockBack = 1f;
			Item.value = 200000;
			Item.DamageType = DamageClass.Magic;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			//int num53 = Projectile.NewProjectile(source, position.X, position.Y, speedX, speedY, type, damage, knockBack, player, 0f, 0f);
			return true;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
        
    }
}
using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Emperia.Items.Sets.PreHardmode.Chillsteel
{
    public class ChillsteelRepeater : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chillsteel Repeater");
			// Tooltip.SetDefault("Bullets fired using the gun inflict Crushing Freeze, no matter the type");
		}
        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 24;
            Item.useTime = 29;
            Item.useAnimation = 29;
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
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			int p = Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockBack, player.whoAmI);
			Main.projectile[p].GetGlobalProjectile<GProj>().chillEffect = true;
			return false;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	
    }
}

using Terraria;
using System;
using Terraria.ID;
using Terraria.DataStructures;
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
			Tooltip.SetDefault("Arrows fired using the bow explode, no matter the type");
		}
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 69;
            Item.height = 40;
            Item.useTime = 29;
            Item.useAnimation = 29;
            Item.useStyle = 5;
            Item.shoot = 3;
            Item.useAmmo = ItemID.WoodenArrow;
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
			Main.projectile[p].GetGlobalProjectile<GProj>().scoriaExplosion = true;
			return false;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	
    }
}

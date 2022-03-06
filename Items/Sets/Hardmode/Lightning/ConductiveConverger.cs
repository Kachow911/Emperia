using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Lightning;

namespace Emperia.Items.Sets.Hardmode.Lightning
{
    public class ConductiveConverger : ModItem
    {
		//int counter = 0;
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Granite Bow");
			Tooltip.SetDefault("Replaces wooden arrows with lightning bolts\n33% chance not to consume ammo");
		}
        public override void SetDefaults()
        {
            Item.damage = 34;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 40;
            Item.useTime = 28;
            Item.useAnimation = 29;
            Item.useStyle = 5;
            Item.shoot = 3;
            Item.useAmmo = ItemID.WoodenArrow;
            Item.knockBack = 1;
            Item.value = 22500;
            Item.rare = 4;
            Item.autoReuse = false;
            Item.shootSpeed = 12f;
			Item.UseSound = SoundID.Item5; 
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = ModContent.ProjectileType<LightningArrow>();
			}

			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.lightningSet)
				Projectile.NewProjectile(source, player.Center.X, player.Center.Y, 0, 0, ModContent.ProjectileType<LightningSetEffect>(), 25, knockBack, player.whoAmI);
			return true;

		}

		public override bool CanConsumeAmmo(Player player)
		{
			return !(Main.rand.Next(3) == 0);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
        public override void AddRecipes()
		{
			/*Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "GraniteBar", 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
			*/
		}
    }
}

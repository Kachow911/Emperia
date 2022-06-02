using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Crimson;

namespace Emperia.Items.Sets.Hardmode.Crimson
{
    public class BloodboilBow : ModItem
    {
		//int counter = 0;
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Granite Bow");
			Tooltip.SetDefault("Replaces wooden arrows with columns of ichor bubbles\n33% chance not to consume ammo");
		}
        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 40;
            Item.useTime = 28;
            Item.useAnimation = 28;
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
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = ModContent.ProjectileType<BigBubble>();
				int numberProjectiles = 3;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = velocity;
					//float speedFact = (float)Main.rand.Next(2, 15) / 10;
					Projectile.NewProjectile(player.GetSource_ItemUse(Item), position.X - (velocity.X * i), position.Y - (velocity.Y * i), perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI); //should also be getsource_itemuse_withpotential ammo. Too bad!
				}
			}
			return;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			return true;

		}

		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return !(Main.rand.Next(3) == 0);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
		
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Vertebrae, 2);
			recipe.AddIngredient(ItemID.Ichor, 10);
			recipe.AddIngredient(ItemID.SoulofNight, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			
		}
    }
}

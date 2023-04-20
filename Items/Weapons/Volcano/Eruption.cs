using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Chat;
using Microsoft.Xna.Framework.Graphics;
using System;
using Emperia.Projectiles;

namespace Emperia.Items.Weapons.Volcano  
{
    public class Eruption : ModItem
    {
		public override void SetDefaults()
		{

			Item.damage = 17;
			Item.noMelee = true;
			Item.noUseGraphic = false;
			Item.DamageType = DamageClass.Magic;
			Item.scale = 1f;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3f;

			Item.staff[Item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(0, 0, 40, 0);
			Item.UseSound = SoundID.Item43;
			Item.autoReuse = true;
			Item.shootSpeed = 3f;
			Item.mana = 15;
			Item.shoot = ModContent.ProjectileType<MagmaBlob>();
		}

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eruption");
            // Tooltip.SetDefault("Shoots a volley of explosive magma blobs");
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			
			int numberProjectiles = 4; 
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15)); 
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * Main.rand.Next(1, 3), perturbedSpeed.Y * Main.rand.Next(1, 3), type, damage, knockBack, player.whoAmI);
			}
			return false; 
		}
    }
}

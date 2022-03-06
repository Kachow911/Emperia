using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles;

namespace Emperia.Items.Sets.PreHardmode.FrousForest
{
	public class LeafKunai : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leaf Kunai");
			Tooltip.SetDefault("Pierces enemies with a chance of poisioning them");
		}


        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.width = 16;
            Item.height = 16;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            //Item.thrown = true;
            Item.DamageType = DamageClass.Ranged;
            Item.channel = true;
            Item.noMelee = true;
            Item.consumable = true;
            Item.maxStack = 999;
            Item.shoot = ModContent.ProjectileType<LeafKunaiProj>();
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 8.0f;
            Item.damage = 16;
            Item.knockBack = 3.5f;
			Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.crit = 4;
            Item.rare = 2;
            Item.autoReuse = true;
            Item.maxStack = 999;
            Item.consumable = true;
        }
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
		{
			int numberProjectiles = 2; 
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians(-3 + 6 * i)); 
				Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false; 
		}
    }
}
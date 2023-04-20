using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;
using Emperia.Projectiles;
namespace Emperia.Items.Sets.PreHardmode.Aquatic
{
    public class Predator : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Predator");
			// Tooltip.SetDefault("Turns bullets into streams of water which pierce enemies");
		}


        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 60;
            Item.height = 32;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.useTurn = false;
            Item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item36;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<RainBlast>();
            Item.shootSpeed = 13f;
            Item.useAmmo = AmmoID.Bullet;
 
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            type = ModContent.ProjectileType<RainBlast>();
            return true;
        }
       /* public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Egg14", 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            
        }*/
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}
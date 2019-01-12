using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
namespace Emperia.Items.Sets.PreHardmode.Aquatic
{
    public class Predator : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Predator");
			Tooltip.SetDefault("Turns bullets into streams of water which pierce enemies");
		}


        public override void SetDefaults()
        {
            item.damage = 17;
            item.ranged = true;
            item.width = 60;
            item.height = 32;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.useTurn = false;
            item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item36;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("RainBlast");
            item.shootSpeed = 13f;
            item.useAmmo = AmmoID.Bullet;
 
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("RainBlast");
            return true;
        }
       /* public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Egg14", 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }*/
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}
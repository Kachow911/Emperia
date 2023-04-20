using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Ice;

namespace Emperia.Items.Sets.PreHardmode.Chillsteel
{
    public class ChillsteelCaster : ModItem
    {
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Wave");
			// Tooltip.SetDefault("Shoots powerful ice clusters, inflicting crushing freeze");
		}


        public override void SetDefaults()
        {
            Item.damage = 25;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 9;
            Item.width = 52;
            Item.height = 60;
            Item.useTime = 15;
            Item.useAnimation = 30;
            Item.reuseDelay = 10;
            Item.useStyle = 5;
            Item.staff[Item.type] = true;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            Item.rare = 3;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<IceBomb2>();
            Item.shootSpeed = 8f;
        }
       /* public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DesertEye", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }*/
    }
}

    using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Chillsteel
{
    public class ChillsteelCaster : ModItem
    {
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Wave");
			Tooltip.SetDefault("Shoots powerful ice clusters, inflicting crushing freeze");
		}


        public override void SetDefaults()
        {
            item.damage = 25;
            item.magic = true;
            item.mana = 9;
            item.width = 52;
            item.height = 60;
            item.useTime = 15;
            item.useAnimation = 30;
            item.reuseDelay = 10;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Terraria.Item.sellPrice(0, 0, 50, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item34;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("IceBomb2");
            item.shootSpeed = 8f;
        }
       /* public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEye", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }*/
    }
}

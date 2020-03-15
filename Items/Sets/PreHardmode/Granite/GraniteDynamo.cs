using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Items.Sets.PreHardmode.Granite
{
	public class GraniteDynamo : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Dynamo");
            Tooltip.SetDefault("Summons a granite elemental to fight for you\nFires like a projectile when first summoned");
			Item.staff[item.type] = true;
        }

		public override void SetDefaults()
		{
            item.width = 38;
            item.height = 36;
            item.value = 22500;
            item.rare = 2;
            item.damage = 16;
            item.knockBack = 0f;
            item.useStyle = 5;
            item.useTime = 55;
            item.useAnimation = 55;
            item.mana = 20; 
            item.summon = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("GraniteMinion");
            item.shootSpeed = 10f;
            item.buffType = mod.BuffType("GraniteMinionBuff");
            item.buffTime = 3600;
            item.UseSound = SoundID.Item44;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
            recipe.AddIngredient(null, "GraniteBar", 8); 
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}
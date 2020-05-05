using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Crimson
{ 
	public class BloodboilNeedle : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloodboil Needle");
			Tooltip.SetDefault("Injects ichor into enemies, weakening them");
		}


        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.width = 16;
            item.height = 16;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.thrown = true;
            item.channel = true;
            item.noMelee = true;
            item.consumable = true;
            item.maxStack = 999;
            item.shoot = mod.ProjectileType("BloodNeedleProj");
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 8.0f;
            item.damage = 58;
            item.knockBack = 4f;
			item.value = Item.sellPrice(0, 0, 1, 50);
            item.crit = 4;
            item.rare = 4;
            item.autoReuse = true;
            item.maxStack = 999;
            item.consumable = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RottenChunk, 2);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
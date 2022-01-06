using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Crimson;

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
            Item.shoot = ModContent.ProjectileType<BloodNeedleProj>();
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 8.0f;
            Item.damage = 58;
            Item.knockBack = 4f;
			Item.value = Item.sellPrice(0, 0, 1, 50);
            Item.crit = 4;
            Item.rare = 4;
            Item.autoReuse = true;
            Item.maxStack = 999;
            Item.consumable = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RottenChunk, 2);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            
        }
    }
}
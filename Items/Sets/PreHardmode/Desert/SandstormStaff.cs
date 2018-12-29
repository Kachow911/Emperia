using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Chat;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Emperia.Items.Sets.PreHardmode.Desert     
{
    public class SandstormStaff : ModItem
    {
		public override void SetDefaults()
		{

			item.damage = 14;
			item.noMelee = true;
			item.noUseGraphic = false;
			item.magic = true;
			item.scale = 1f;
			item.width = 40;
			item.height = 40;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 5;
			item.knockBack = 3f;

			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
			item.rare = 2;
			item.value = Item.sellPrice(0, 0, 40, 0);
			item.UseSound = SoundID.Item43;
			item.autoReuse = true;
			item.shootSpeed = 7f;
			item.mana = 7;
			item.shoot = mod.ProjectileType("SandBlast");
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandstorm Caster");
            Tooltip.SetDefault("Shoots a blast of sand");
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "PolishedSandstone", 12);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}

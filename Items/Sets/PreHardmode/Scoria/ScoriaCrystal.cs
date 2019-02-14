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

namespace Emperia.Items.Sets.PreHardmode.Scoria    
{
    public class ScoriaCrystal : ModItem
    {
		public override void SetDefaults()
		{

			item.damage = 26;
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
			item.rare = 3;
			item.value = Item.sellPrice(0, 0, 40, 0);
			item.UseSound = SoundID.Item43;
			item.autoReuse = true;
			item.shootSpeed = 7f;
			item.mana = 7;
			item.shoot = mod.ProjectileType("ScorchBlast");
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scoria Crystal");
            Tooltip.SetDefault("Shoots blasts of scorch");
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "PolishedSandstone", 12);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
           for (int i = 0; i < 2; i++)
           {
               Projectile.NewProjectile(position.X, position.Y, speedX * Main.rand.Next(3, 8), speedY * Main.rand.Next(3, 8), type, damage, knockBack, player.whoAmI);
           }
            return false;
        }
    }
}

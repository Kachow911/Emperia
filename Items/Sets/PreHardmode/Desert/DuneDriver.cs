using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Desert
{
    public class DuneDriver : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons a pack of illusory worms to plague your foes");
            Item.staff[item.type] = true;
		}

        public override void SetDefaults()
        {
            item.damage = 14;
            item.magic = true;
            item.mana = 14;
            item.width = 52;
            item.height = 60;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = 25000;
            item.rare = 3;
            item.UseSound = SoundID.Item8;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("DuneDriverProj");
            item.shootSpeed = 0f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type1, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(player.position.X + 65 * player.direction, player.position.Y, 0, 0, mod.ProjectileType("DuneDriverProj"), item.damage, 0, Main.myPlayer, 0, 0);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DesertEye", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}

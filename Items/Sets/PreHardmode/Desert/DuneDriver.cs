using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Desert;

namespace Emperia.Items.Sets.PreHardmode.Desert
{
    public class DuneDriver : ModItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons a pack of illusory worms to plague your foes");
            Item.staff[Item.type] = true;
		}

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 14;
            Item.width = 52;
            Item.height = 60;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = 27000;
            Item.rare = 1;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<DuneDriverProj>();
            Item.shootSpeed = 0f;
        }
		public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            Projectile.NewProjectile(source, player.position.X + 65 * player.direction, player.position.Y, 0, 0, ModContent.ProjectileType<DuneDriverProj>(), Item.damage, 0, Main.myPlayer, 0, 0);
            return false;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DesertEye", 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            

        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

using Emperia.Projectiles.Corrupt;
namespace Emperia.Items.Sets.Hardmode.Corrupt
{
    public class RotfireBlade : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rotfire Blade");
			Tooltip.SetDefault("Hitting enemies directly inflicts cursed inferno");
		}


        int charger;
        public override void SetDefaults()
        {
            Item.damage = 49;
            Item.DamageType = DamageClass.Melee;
            Item.width = 46;
            Item.height = 66;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = Terraria.Item.sellPrice(0, 8, 0, 0);
            Item.rare = 4;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CursedBolt>();
            Item.shootSpeed = 12;
        }
        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            int numberProjectiles = Main.rand.Next(1, 2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(12));
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * 2, perturbedSpeed.Y * 2, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            {
                target.AddBuff(BuffID.CursedInferno, 180);
            }
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
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Crimson
{
    public class BloodboilScimitar : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloodboil Scimitar");
			Tooltip.SetDefault("Occasionally launches clusters of ichor bubbles");
		}


        int charger;
        public override void SetDefaults()
        {
            item.damage = 38;
            item.melee = true;
            item.width = 46;
            item.height = 66;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = Terraria.Item.sellPrice(0, 8, 0, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("BigBubble");
            item.shootSpeed = 12;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.rand.NextBool(7))
            {
                int numberProjectiles = Main.rand.Next(5, 6);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(16));
                    float speedFact = (float) Main.rand.Next(2, 15) / 10;
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * speedFact, perturbedSpeed.Y * speedFact, type, damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            {
                target.AddBuff(BuffID.Ichor, 180);
            }
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
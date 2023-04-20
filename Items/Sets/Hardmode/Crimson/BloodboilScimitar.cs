using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Emperia.Projectiles.Crimson;

namespace Emperia.Items.Sets.Hardmode.Crimson
{
    public class BloodboilScimitar : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bloodboil Scimitar");
			// Tooltip.SetDefault("Occasionally launches clusters of ichor bubbles");
		}


        int charger;
        public override void SetDefaults()
        {
            Item.damage = 38;
            Item.DamageType = DamageClass.Melee;
            Item.width = 46;
            Item.height = 66;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Terraria.Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<BigBubble>();
            Item.shootSpeed = 12;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            if (Main.rand.NextBool(7))
            {
                int numberProjectiles = Main.rand.Next(5, 6);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(16));
                    float speedFact = (float) Main.rand.Next(2, 15) / 10;
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * speedFact, perturbedSpeed.Y * speedFact, type, damage, knockBack, player.whoAmI);
                }
            }
            return false;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            {
                target.AddBuff(BuffID.Ichor, 180);
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
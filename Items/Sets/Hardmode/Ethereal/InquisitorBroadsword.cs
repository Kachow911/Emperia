using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Ethereal;

namespace Emperia.Items.Sets.Hardmode.Ethereal
{
    public class InquisitorBroadsword : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Inquisitor Broadsword");
			// Tooltip.SetDefault("Shoots a damaging wave");
		}


        int charger;
        public override void SetDefaults()
        {
            Item.damage = 55;
            Item.DamageType = DamageClass.Melee;
            Item.width = 56;
            Item.height = 56;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = Terraria.Item.sellPrice(0, 8, 0, 0);
            Item.rare = 5;
            Item.UseSound = SoundID.DD2_SonicBoomBladeSlash;;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<EtherealWave>();
            Item.shootSpeed = 5;
        }
        /*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
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
        }*/
        public override void AddRecipes()
        {
            
        }

    }
}
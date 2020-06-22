using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Ethereal
{
    public class InquisitorBroadsword : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Inquisitor Broadsword");
			Tooltip.SetDefault("Shoots a damaging wave");
		}


        int charger;
        public override void SetDefaults()
        {
            item.damage = 55;
            item.melee = true;
            item.width = 56;
            item.height = 56;
            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = Terraria.Item.sellPrice(0, 8, 0, 0);
            item.rare = 5;
            item.UseSound = SoundID.DD2_SonicBoomBladeSlash;;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("EtherealWave");
            item.shootSpeed = 5;
        }
        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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
        }*/
        public override void AddRecipes()
        {
            
        }

    }
}
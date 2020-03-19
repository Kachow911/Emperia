using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

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
            item.damage = 41;
            item.melee = true;
            item.width = 46;
            item.height = 66;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = Terraria.Item.sellPrice(0, 8, 0, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("CursedBolt");
            item.shootSpeed = 12;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(1, 3);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(12));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 2, perturbedSpeed.Y * 2, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            {
                target.AddBuff(BuffID.CursedInferno, 180);
            }
        }

    }
}
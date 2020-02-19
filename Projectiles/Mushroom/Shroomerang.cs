using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{
    public class Shroomerang : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(106);
            aiType = 106;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomerang");
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 10;
			height = 10;
			return true;
		}

        public override void AI()
        {
            //
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (target.life <= 0)
			{
				for (int i = 0; i < 6; i++)
				{
				
					Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 60 * i));
					Projectile.NewProjectile(target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("EnchantedMushroom"), damage / 3, 1, Main.myPlayer, 0, 0);
				
				}
			}

		}
    }
}

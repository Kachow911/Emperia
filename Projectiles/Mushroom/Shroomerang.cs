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
            Projectile.CloneDefaults(106);
            AIType = 106;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shroomerang");
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			width = 10;
			height = 10;
			return true;
		}

        public override void AI()
        {
            //
        }
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.life > 0)
                return;

            for (int i = 0; i < 6; i++)
            {
                Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 60 * i));
                Projectile.NewProjectile(Projectile.InheritSource(Projectile), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<EnchantedMushroom>(), hit.SourceDamage / 3, 1, Main.myPlayer, 0, 0);
            }

        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Yeti
{
	public class HuntersSpear : ModProjectile
	{
		private bool hit = false;
		Vector2 distFromEnemy;
		NPC target1;
		float rotation;
	    public override void SetDefaults()
		{
			projectile.scale = 1.2f;
			projectile.width = 18;
			projectile.height = 52;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 2;
			projectile.thrown = true;
			projectile.ignoreWater = true;
			aiType = ProjectileID.JavelinFriendly;
		}

	    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hunter's Spear");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

	    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = 10;
			height = 10;
			return true;
		}
		public override void AI()
        {
			
			
		}
		 public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			 target.AddBuff(BuffID.Frostburn, 100);
			 for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-32, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                if (i % 8 == 0)
                {   //odd
                    Dust.NewDust(projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 51);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7),51, vec.X * 2, vec.Y * 2);
                }
            }
		}
	    public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for(int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
	}
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent;

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
			Projectile.scale = 1.2f;
			Projectile.width = 18;
			Projectile.height = 52;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.penetrate = 2;
			Projectile.DamageType = DamageClass.Ranged;
//was thrown pre 1.4
			Projectile.ignoreWater = true;
			AIType = ProjectileID.JavelinFriendly;
		}

	    public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hunter's Spear");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

	    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
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
                    Dust.NewDust(Projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 51);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(Projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7),51, vec.X * 2, vec.Y * 2);
                }
            }
		}
	    public override bool PreDraw(ref Color lightColor)
        {
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for(int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }
	}
}

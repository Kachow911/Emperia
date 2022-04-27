using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Emperia.Projectiles
{
	public class RedPixel : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Red Pixel"); //for testing locations lol
		}
		//Projectile.NewProjectile(Wiring.GetProjectileSource((int)closestTile.X, (int)closestTile.Y), closestNPC.Center, Vector2.Zero, ModContent.ProjectileType<RedPixel>(), 0, 0);
		public override void SetDefaults() {
			Projectile.width = 2;
			Projectile.height = 2;
			//Projectile.aiStyle = 1;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 600;
			//Projectile.alpha = 255;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.light = 1f;
		}
		public override bool PreDraw(ref Color lightColor) {
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}
	}
}

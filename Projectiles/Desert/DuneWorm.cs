using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Desert
{
    public class DuneWorm : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dune Worm");
			ProjectileID.Sets.DontAttachHideToAlpha[projectile.type] = true;
		}
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
			projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 80;
            projectile.ignoreWater = true;
			projectile.spriteDirection = projectile.direction;
			projectile.hide = true;
        }
        public override void AI()
        {
			projectile.rotation = projectile.velocity.ToRotation();
		}
		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI) {
			drawCacheProjsBehindNPCsAndTiles.Add(index);
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++) {
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 2; ++i)
			{
				int dust1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 246);
				Main.dust[dust1].velocity *= 0f;
			}
		}
    }
}
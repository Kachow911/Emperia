using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Emperia.Projectiles.Desert
{
    public class DuneWorm : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dune Worm");
			ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type] = true;
		}
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 80;
            Projectile.ignoreWater = true;
			Projectile.spriteDirection = Projectile.direction;
			Projectile.hide = true;
        }
        public override void AI()
        {
			Projectile.rotation = Projectile.velocity.ToRotation();
		}
		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI) {
			behindNPCs.Add(index);
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
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 2; ++i)
			{
				int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GoldCoin);
				Main.dust[dust1].velocity *= 0f;
			}
		}
    }
}
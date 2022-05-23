using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Emperia.Projectiles
{
	public class Escarbeam : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Escarbeam");
		}

		public override void SetDefaults() {
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 600;
			Projectile.alpha = 255;
			Projectile.light = 0.8f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;
			Projectile.scale = 1.8f;
			AIType = ProjectileID.GreenLaser;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.eschargo < 0 && crit && target.GetGlobalNPC<MyNPC>().IsNormalEnemy(target))
			{
						//CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), Color.HotPink, "Charged!", false, false);
				modPlayer.eschargo += 1;
                if (modPlayer.eschargo == 0) Terraria.Audio.SoundEngine.PlaySound(SoundID.Item4, player.Center);
			}
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

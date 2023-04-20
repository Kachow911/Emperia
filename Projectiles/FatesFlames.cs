using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Emperia.Projectiles;
using Emperia.Buffs;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Projectiles
{
	public class FatesFlames : ModProjectile
	{
		private int decelerate = Main.rand.Next(16);

		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Fate's Flames");
		}

		public override void SetDefaults() {
			Projectile.damage = 0;
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 480 + Main.rand.Next(120);
			Projectile.light = 0.4f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false; //maybe change so it doesn't break on tiles but stops on them, although it's already better on ground
			Main.projFrames[Projectile.type] = 4;
		}
		public override void AI()
        {              
			if (Projectile.timeLeft % 30 == 0)
			{
					int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 10), Projectile.width, Projectile.height, 191, 0f, 0f, 91, new Color(89, 249, 116), 1.5f);
					Main.dust[dust].velocity = new Vector2(0, -1);
					Main.dust[dust].noGravity = true;
			}

			if (decelerate > 0)
			{
				decelerate--;
			}
			else if (Projectile.timeLeft % 5 == 0 && decelerate == 0)
			{
				Projectile.velocity = Projectile.velocity / 2;
			}

			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 4;
			}
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (target.life <= 0 && !target.HasBuff(ModContent.BuffType<FatesDemise>()))
			{
				int damage1 = 0;
				if (target.lifeMax > 1500)
				{
					damage1 = 300;
				}
				else
				{
					damage1 = target.lifeMax / 5;
				}
				for (int i = 0; i < 6; i++)
				{
					Vector2 perturbedSpeed = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
					Projectile.NewProjectile(Projectile.InheritSource(Projectile), target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FatesFlames>(), damage1, 1, Main.myPlayer, 0, 0);
					PlaySound(SoundID.NPCDeath52, target.Center);
				}
			}
            else
            {
                target.AddBuff(ModContent.BuffType<FatesDemise>(), 720);
			}
		}

		public override void Kill(int timeLeft)
        {
			if (Projectile.penetrate == 0)
			{
				PlaySound(SoundID.Item14, Projectile.Center);
				for (int i = 0; i < 30; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, 191, 0f, 0f, 91, new Color(89, 249, 116), 1.5f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 2f;
				}
			}
			else
			{
				PlaySound(SoundID.Item20, Projectile.Center);
				for (int i = 0; i < 3; ++i)
				{
					int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 10), Projectile.width, Projectile.height, 191, 0f, 0f, 91, new Color(89, 249, 116), 1.5f);
					Main.dust[dust].noGravity = true;
				}
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

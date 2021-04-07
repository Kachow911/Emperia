using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
	public class FatesFlames : ModProjectile
	{
		private int decelerate = Main.rand.Next(16);

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Fate's Flames");
		}

		public override void SetDefaults() {
			projectile.damage = 0;
			projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = 1;
			projectile.timeLeft = 480 + Main.rand.Next(120);
			projectile.light = 0.4f;
			projectile.ignoreWater = true;
			projectile.tileCollide = false; //maybe change so it doesn't break on tiles but stops on them, although it's already better on ground
			Main.projFrames[projectile.type] = 4;
		}
		public override void AI()
        {              
			if (projectile.timeLeft % 30 == 0)
			{
					int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 10), projectile.width, projectile.height, 191, 0f, 0f, 91, new Color(89, 249, 116), 1.5f);
					Main.dust[dust].velocity = new Vector2(0, -1);
					Main.dust[dust].noGravity = true;
			}

			if (decelerate > 0)
			{
				decelerate--;
			}
			else if (projectile.timeLeft % 5 == 0 && decelerate == 0)
			{
				projectile.velocity = projectile.velocity / 2;
			}

			projectile.frameCounter++;
			if (projectile.frameCounter >= 6)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (target.life <= 0 && !target.HasBuff(mod.BuffType("FatesDemise")))
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
					Projectile.NewProjectile(target.Center.X, target.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FatesFlames"), damage1, 1, Main.myPlayer, 0, 0);
					Main.PlaySound(SoundID.NPCDeath52, target.Center);
				}
			}
			else {
				target.AddBuff(mod.BuffType("FatesDemise"), 720);
			}
		}

		public override void Kill(int timeLeft)
        {
			if (projectile.penetrate == 0)
			{
				Main.PlaySound(SoundID.Item14, projectile.Center);
				for (int i = 0; i < 30; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 191, 0f, 0f, 91, new Color(89, 249, 116), 1.5f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 2f;
				}
			}
			else
			{
				Main.PlaySound(SoundID.Item20, projectile.Center);
				for (int i = 0; i < 3; ++i)
				{
					int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 10), projectile.width, projectile.height, 191, 0f, 0f, 91, new Color(89, 249, 116), 1.5f);
					Main.dust[dust].noGravity = true;
				}
			}
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
	}
}

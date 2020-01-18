using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace Emperia.Projectiles
{
	public class GraniteArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 1000;
			projectile.tileCollide = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Arrow");
		}
		
		public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
			projectile.position.X -= (float) (projectile.width * 3);
			projectile.position.Y -= (float) (projectile.height * 3);
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.graniteSet && modPlayer.graniteTime >= 1800)
			{
				projectile.width *= 12;
				projectile.height *= 12;
				projectile.localAI[1] = -1f;
				projectile.maxPenetrate = 0;
				projectile.damage *= 2;
				projectile.Damage();

				for (int i = 0; i < 60; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 4.0f;
				}
				modPlayer.graniteTime = 0;
			}
			else
			{
				projectile.width *= 6;
				projectile.height *= 6;
			
				projectile.localAI[1] = -1f;
				projectile.maxPenetrate = 0;
				projectile.Damage();

				for (int i = 0; i < 30; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 2.7f;
				}
			}
			
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{	
			target.immune[projectile.owner] = 5;
		}
		
		public override void AI()
		{
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 0.8f);
			}
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture2D3 = Main.projectileTexture[projectile.type];
			int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int y3 = num156 * projectile.frame;
			Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, y3, texture2D3.Width, num156);
			Vector2 origin2 = rectangle.Size() / 2f;
			Main.spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.position + projectile.Size / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Color.White, projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
	}
}
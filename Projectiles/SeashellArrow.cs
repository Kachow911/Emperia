using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using System.Collections.Generic;
using System;

namespace Emperia.Projectiles
{
	public class SeashellArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 60;
			Projectile.tileCollide = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seaweed Arrow");
		}
		
		public override void Kill(int timeLeft)
		{
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			Projectile.position.X -= (float) (Projectile.width * 3);
			Projectile.position.Y -= (float) (Projectile.height * 3);
			Projectile.width *= 6;
			Projectile.height *= 6;
			
			Projectile.localAI[1] = -1f;
			Projectile.maxPenetrate = 0;
			Projectile.Damage();

			for (int i = 0; i < 30; ++i)
			{
			  int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 61, 0.0f, 0.0f, 15, Color.White, 2f);
			  Main.dust[index2].noGravity = true;
			  Main.dust[index2].velocity *= 2.7f;
			}
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{	
			target.immune[Projectile.owner] = 2;
		}
		
		public override void AI()
		{
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height,61, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 0.8f);
			}
		}
		
		public override bool PreDraw(ref Color lightColor)
		{
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			int num156 = texture.Height / Main.projFrames[Projectile.type];
			int y3 = num156 * Projectile.frame;
			Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, y3, texture.Width, num156);
			Vector2 origin2 = rectangle.Size() / 2f;
			Main.EntitySpriteDraw(texture, Projectile.position + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Color.White, Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
			return false;
		}
	}
}
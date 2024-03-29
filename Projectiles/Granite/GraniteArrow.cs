using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using System.Collections.Generic;
using System;

namespace Emperia.Projectiles.Granite
{
	public class GraniteArrow : ModProjectile
	{
		NPC hitNPC;
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 1000;
			Projectile.tileCollide = true;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Granite Arrow");
		}
		
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
			Player player = Main.player[Projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.graniteSet && modPlayer.graniteTime >= 900)
            {
				modifiers.SourceDamage *= 1.75f;
			}
		}
		public override void OnKill(int timeLeft)
		{
			Player player = Main.player[Projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			Projectile.localAI[1] = -1f;
			Projectile.maxPenetrate = 0;
			Projectile.Damage();
			if (modPlayer.graniteSet && modPlayer.graniteTime >= 900)
			{
				for (int i = 0; i < Main.npc.Length; i++)
            	{
                	if (Projectile.Distance(Main.npc[i].Center) < 90 && Main.npc[i] != hitNPC)
                    	Main.npc[i].SimpleStrikeNPC((int)(Projectile.damage * 1.75f), 0);
            	}
				for (int i = 0; i < 45; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.MagicMirror, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 3.25f;
				}
				modPlayer.graniteTime = 0;
				Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
			}
			else
			{
				for (int i = 0; i < Main.npc.Length; i++)
            	{
                	if (Projectile.Distance(Main.npc[i].Center) < 65 && Main.npc[i] != hitNPC)
                    	Main.npc[i].SimpleStrikeNPC(Projectile.damage, 0);
            	}
				for (int i = 0; i < 30; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.MagicMirror, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 2f;
				}
				Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
			}
			
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{	
			target.immune[Projectile.owner] = 5;
			hitNPC = target;
		}
		
		public override void AI()
		{
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.MagicMirror, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 0.8f);
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
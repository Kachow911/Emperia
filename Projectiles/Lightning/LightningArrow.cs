using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;
using Emperia.Buffs;

namespace Emperia.Projectiles.Lightning
{
	public class LightningArrow : ModProjectile
	{
		//NPC hitNPC;
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			//Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 1000;
			Projectile.tileCollide = true;
		}
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lightning Arrow");
		}
		
		
		public override void Kill(int timeLeft)
		{
			/*Terraria.Audio.SoundEngine.PlaySound(2, (int)Projectile.position.X, (int)Projectile.position.Y, 10);
			Player player = Main.player[Projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			Projectile.localAI[1] = -1f;
			Projectile.maxPenetrate = 0;
			Projectile.Damage();
			if (modPlayer.graniteSet && modPlayer.graniteTime >= 1800)
			{
				for (int i = 0; i < Main.npc.Length; i++)
            	{
                	if (Projectile.Distance(Main.npc[i].Center) < 90 && Main.npc[i] != hitNPC)
                    	Main.npc[i].StrikeNPC(Projectile.damage + Projectile.damage / 2, 0f, 0, false, false, false);
            	}
				for (int i = 0; i < 45; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 3.25f;
				}
				modPlayer.graniteTime = 0;
			}
			else
			{
				for (int i = 0; i < Main.npc.Length; i++)
            	{
                	if (Projectile.Distance(Main.npc[i].Center) < 60 && Main.npc[i] != hitNPC)
                    	Main.npc[i].StrikeNPC(Projectile.damage, 0f, 0, false, false, false);
            	}
				for (int i = 0; i < 30; ++i)
				{
					int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity *= 2f;
				}
			}*/
			
		}
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<ElecHostile>(), 120);
			Player player = Main.player[Projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.lightningSet)
				modPlayer.lightningDamage += damageDone;
		}
		
		public override void AI()
		{
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Electric, 0.0f, 0.0f, 15, Color.LightBlue, 0.8f);
			}
		}
	
	}
}
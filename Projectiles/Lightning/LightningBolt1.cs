using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Projectiles.Lightning
{
	
    public class LightningBolt1 : ModProjectile
    {
		private bool init = false;
		Vector2 initialVel = Vector2.Zero;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lightning Bolt");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 28;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
			Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 180;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
	
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<ElecHostile>(), 240);
			Player player = Main.player[Projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.lightningSet)
				modPlayer.lightningDamage += damageDone;
		}
		public override void AI()           //Projectile make that the Projectile will face the corect way
        {             
			if (!init)
			{
				initialVel = Projectile.velocity;
				init = true;
			}
			Projectile.velocity = Vector2.Zero;
			Projectile.Center = Projectile.Center + initialVel.RotatedByRandom(MathHelper.ToRadians(60));
			for (int i = -1; i < 1; i++)
			{
				for (int index1 = 0; index1 < 2; ++index1)
				{
					float num1 = Projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(Projectile.Center + new Vector2(i, 0), 0, 0, DustID.Electric, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity.X /= 2;
					Main.dust[index2].alpha = 255;
					Main.dust[index2].scale = 1.2f;
				}
            }
			for (int i = -1; i < 1; i++)
			{
				for (int index1 = 0; index1 < 2; ++index1)
				{
					float num1 = Projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(Projectile.Center + new Vector2(0, i), 0, 0, DustID.Electric, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity.Y /= 2;
					Main.dust[index2].alpha = 255;
					Main.dust[index2].scale = 1.2f;
				}
			}
			/*for (int i = -1; i < 1; i++)
			{
				for (int index1 = 0; index1 < 2; ++index1)
				{
					float num1 = Projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(Projectile.Center + new Vector2(i, 2), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity.X /= 2;
					Main.dust[index2].alpha = 255;
					Main.dust[index2].scale = 1.2f;
				}
			}
			for (int i = -1; i < 1; i++)
			{
				for (int index1 = 0; index1 < 2; ++index1)
				{
					float num1 = Projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(Projectile.Center + new Vector2(i, 4), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity.X /= 2;
					Main.dust[index2].alpha = 255;
					Main.dust[index2].scale = 1.2f;
				}
			}
			for (int i = -1; i < 1; i++)
			{
				for (int index1 = 0; index1 < 2; ++index1)
				{
					float num1 = Projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(Projectile.Center + new Vector2(i, -4), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity.X /= 2;
					Main.dust[index2].alpha = 255;
					Main.dust[index2].scale = 1.2f;
				}
			}*/
	
			
        }
        
    }
}
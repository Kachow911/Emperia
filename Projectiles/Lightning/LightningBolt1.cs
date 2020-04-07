using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Lightning
{
	
    public class LightningBolt1 : ModProjectile
    {
		private bool init = false;
		Vector2 initialVel = Vector2.Zero;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lightning Bolt");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 28;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
			projectile.hostile = false;
            projectile.magic = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 180;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
	
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(mod.BuffType("ElecHostile"), 240);
			Player player = Main.player[projectile.owner];
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.lightningSet)
				modPlayer.lightningDamage += damage;
		}
		public override void AI()           //projectile make that the projectile will face the corect way
        {             
			if (!init)
			{
				initialVel = projectile.velocity;
				init = true;
			}
			projectile.velocity = Vector2.Zero;
			projectile.Center = projectile.Center + initialVel.RotatedByRandom(MathHelper.ToRadians(60));
			for (int i = -1; i < 1; i++)
			{
				for (int index1 = 0; index1 < 2; ++index1)
				{
					float num1 = projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(projectile.Center + new Vector2(i, 0), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
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
					float num1 = projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(projectile.Center + new Vector2(0, i), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
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
					float num1 = projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(projectile.Center + new Vector2(i, 2), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
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
					float num1 = projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(projectile.Center + new Vector2(i, 4), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
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
					float num1 = projectile.rotation + (float) ((Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
					float num2 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
					Vector2 vector2;
					vector2 = new Vector2((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
					int index2 = Dust.NewDust(projectile.Center + new Vector2(i, -4), 0, 0, 226, (float) vector2.X, (float) vector2.Y, 0, Color.LightBlue, 1f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].velocity.X /= 2;
					Main.dust[index2].alpha = 255;
					Main.dust[index2].scale = 1.2f;
				}
			}*/
	
			
        }
        
    }
}
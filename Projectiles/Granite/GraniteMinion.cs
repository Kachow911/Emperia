using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles.Granite
{
	public class GraniteMinion : ModProjectile
	{
		private int npc;
		bool targetNPC = false;
		private int totalHits = 0;
		private int curHits = 0;
		private bool init = false;
		private int projDamage = 0;
		int hitTimer = 0;
		int retargetTimer = 60;
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Spazmamini);
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.netImportant = true;
			projectile.width = 18;
			projectile.height = 18;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1000;
			aiType = ProjectileID.Spazmamini;

		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{

			return false;
		}
		public override void AI()
		{
			retargetTimer--;
			hitTimer++;
			if (!init)
            {
				projDamage = projectile.damage;
				projectile.damage = 0;
				init = true;
            }
			bool flag64 = projectile.type == mod.ProjectileType("GraniteMinion");

			MyPlayer modPlayer1 = Main.player[projectile.owner].GetModPlayer<MyPlayer>();
			if (flag64)
			{
				if (Main.player[projectile.owner].dead)
					modPlayer1.graniteMinion = false;

				if (modPlayer1.graniteMinion)
					projectile.timeLeft = 2;

			}
			targetNPC = false;
			for (int npcFinder = 0; npcFinder < 200; ++npcFinder)
			{
				if (Main.npc[npcFinder].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[npcFinder].Center, 1, 1))
				{
					Vector2 num1 = Main.npc[npcFinder].Center;
					float num2 = Math.Abs(projectile.Center.X - num1.X) + Math.Abs(projectile.Center.Y - num1.Y);
					if (num2 < 200f)
					{
						targetNPC = true;
						npc = npcFinder;

					}

				}
			}
            if (targetNPC && retargetTimer < 0)
            {
				aiType = -1;
				float num4 = Main.rand.Next(30, 43);
				Vector2 vector35 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
				float num5 = Main.npc[npc].Center.X - vector35.X;
				float num6 = Main.npc[npc].Center.Y - vector35.Y;
				float num7 = (float)Math.Sqrt((double)(num5 * num5 + num6 * num6));
				float num8 = num4 / num7;
				num5 *= num8;
				num6 *= num8;
				projectile.velocity.X = (projectile.velocity.X * 20f + num5) / 30f;
				projectile.velocity.Y = (projectile.velocity.Y * 20f + num6) / 30f;
				if (num7 < 6f)
                {
					projectile.Center = Main.npc[npc].Center;
					if (hitTimer > 60)
                    {
						Main.npc[npc].StrikeNPC(projDamage, 0f, 0, false, false, false);
						hitTimer = 0;
						curHits++;
						totalHits++;
						if (totalHits >= 10)
                        {
							Main.PlaySound(SoundID.Item14, projectile.Center);
							Player player = Main.player[projectile.owner];
							MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
							if (modPlayer.graniteSet && modPlayer.graniteTime >= 1800)
							{
								for (int i = 0; i < Main.npc.Length; i++)
								{
									if (projectile.Distance(Main.npc[i].Center) < 90)
										Main.npc[i].StrikeNPC(projDamage * 3, 0f, 0, false, false, false);
								}
								for (int i = 0; i < 45; ++i)
								{
									int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
									Main.dust[index2].noGravity = true;
									Main.dust[index2].velocity *= 3.25f;
								}
								modPlayer.graniteTime = 0;
							}
							else
							{
								for (int i = 0; i < Main.npc.Length; i++)
								{
									if (projectile.Distance(Main.npc[i].Center) < 60)
										Main.npc[i].StrikeNPC(projDamage + projDamage / 2, 0f, 0, false, false, false);
								}
								for (int i = 0; i < 30; ++i)
								{
									int index2 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 1.5f);
									Main.dust[index2].noGravity = true;
									Main.dust[index2].velocity *= 2f;
								}
							}
							projectile.timeLeft = 0;
                        }
						if (curHits >= 3)
                        {
							Vector2 perturbedSpeed = new Vector2(0, 8).RotatedByRandom(MathHelper.ToRadians(360));
							projectile.velocity = perturbedSpeed;
							retargetTimer = 120;
							curHits = 0;
						}
					}

				}
			}
            else if (retargetTimer < 0)
            {
				aiType = ProjectileID.Spazmamini;
			}
			else
            {
				aiType = -1;
			}
		}
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 3; ++i)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 3.25f;
			}
		}
		
	}
	
}
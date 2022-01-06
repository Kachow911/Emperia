using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Projectiles.Lightning
{
	
    public class LightningSetEffect : ModProjectile
    {
		private bool init = false;
		Vector2 initialVel = Vector2.Zero;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lightning Bolt");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 28;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
			Projectile.hostile = false;
           // Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 1;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<ElecHostile>(), 240);
		}
		public override void AI()           //Projectile make that the Projectile will face the corect way
		{
			int count = 0;
			for (int npcFinder = 0; npcFinder < 200; ++npcFinder)
			{
				if (Main.npc[npcFinder].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[npcFinder].Center, 1, 1))
				{
					Vector2 num1 = Main.npc[npcFinder].Center;
					float num2 = Math.Abs(Projectile.Center.X - num1.X) + Math.Abs(Projectile.Center.Y - num1.Y);
					if (num2 < 500f)
					{
						Vector2 vector2 = new Vector2(Projectile.Center.X, Projectile.Center.Y);
						float num11 = Main.npc[npcFinder].Center.X - vector2.X;
						float num22 = Main.npc[npcFinder].Center.Y - vector2.Y;
						float rotation = (float)Math.Atan2((double)num22, (double)num11);
						if (count < 3)
						{
							Main.npc[npcFinder].StrikeNPC(Projectile.damage, 0f, 0, false, false, false);
							count++;
							bool flag = true;
							while (flag)
							{
								float f = (float)Math.Sqrt((double)num11 * (double)num11 + (double)num22 * (double)num22);
								if ((double)f < 25.0)
									flag = false;
								else if (float.IsNaN(f))
								{
									flag = false;
								}
								else
								{
									float num3 = 5f / f;
									float num4 = num11 * num3;
									float num5 = num22 * num3;
									vector2.X += num4;
									vector2.Y += num5;
									num11 = Main.npc[npcFinder].Center.X - vector2.X;
									num22 = Main.npc[npcFinder].Center.Y - vector2.Y;
									int num250 = Dust.NewDust(new Vector2(vector2.X, vector2.Y), 16, 16, 226, (float)(Projectile.direction * 2), 0f, 226, new Color(53f, 67f, 253f), 0.5f);
									Main.dust[num250].noGravity = true;

								}
								//Main.EntitySpriteDraw(Mod.Assets.Request<Texture2D>("Projectiles/Tether").Value, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 12, 6)), color, rotation, new Vector2((float)12 * 0.5f, (float)6 * 0.5f), 1f, SpriteEffects.None, 0);
							}
						}

					}
				}

			}
		}
        
    }
}
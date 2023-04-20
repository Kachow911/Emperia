using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Projectiles.Ice
{
    public class ChillDaggerProj : ModProjectile
    {
		bool init = false;
		Color rgb;
		int timer = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chillsteel Dagger");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 30;       //Projectile width
            Projectile.height = 30;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Ranged;
//was thrown pre 1.4         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 2;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the Projectile will face the corect way
        {                                                           // |
			if (Projectile.velocity.X > 0)
			{
				Projectile.spriteDirection = 1;
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + (float) (3.14 / 4);
			}
			else if (Projectile.velocity.X < 0)
			{
				Projectile.spriteDirection = -1;
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + (float)((3.14) - (3.14 / 4));
			}

            int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, 68, (float)(Projectile.velocity.X * 0.200000002980232), (float)(Projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.7f);
            Main.dust[index2].position = Projectile.Center;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = Projectile.velocity * 0.5f;
        }
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (!init)
			{
				target.GetGlobalNPC<MyNPC>().chillStacks += 1;
				target.AddBuff(ModContent.BuffType<CrushingFreeze>(), 300);
				init = true;
			}
		}
		public override void Kill(int timeLeft)
        {
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
			for (int i = 0; i < 360; i += 36)
				{
				Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 46, 0f, 0f, 158, new Color(53f, 67f, 253f), 1f);
				Main.dust[num622].velocity += (vec *2f);
				Main.dust[num622].noGravity = true;
				}

		}
		
    }
}
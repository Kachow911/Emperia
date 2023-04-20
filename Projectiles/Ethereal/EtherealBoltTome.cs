using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Ethereal
{
    public class EtherealBoltTome : ModProjectile
    {
        bool init = false;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ethereal Bolt");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 14;       //Projectile width
            Projectile.height = 14;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this Projectile has before disepire
            Projectile.light = 1f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the Projectile will face the corect way
        {
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            /*if (Main.rand.Next(8) == 2)
            {
                int num622 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 1, 1, 180, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
                Main.dust[num622].velocity += Projectile.velocity * 0.2f;
                Main.dust[num622].noGravity = true;
            }*/
            if (!init)
            {
                init = true;
                for (int i = 0; i < 360; i++)
                {
                    Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
                    if (i % 8 == 0)
                    {
                        int b = Dust.NewDust(Projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), DustID.Vortex);
                        Main.dust[b].noGravity = true;
                        Main.dust[b].velocity = vec;
                    }
                }
            }
        }
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.GetGlobalNPC<MyNPC>().etherealDamages.Add(damageDone/2);
            target.GetGlobalNPC<MyNPC>().etherealCounts.Add(2);
            target.GetGlobalNPC<MyNPC>().etherealSource = Projectile;
        }
        public override void Kill(int timeLeft)
        {
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int index1 = 4; index1 < 31; ++index1)
            {
              float num1 = (float) (Projectile.oldVelocity.X * (30.0 / (double) index1));
              float num2 = (float) (Projectile.oldVelocity.Y * (30.0 / (double) index1));
              int index2 = Dust.NewDust(new Vector2((float) Projectile.oldPosition.X - num1, (float) Projectile.oldPosition.Y - num2), 8, 8, DustID.Vortex, (float) Projectile.oldVelocity.X * 2, (float) Projectile.oldVelocity.Y * 2, 100, Color.LightBlue, 2f);
              Main.dust[index2].noGravity = true;
              Dust dust1 = Main.dust[index2];
              dust1.velocity = dust1.velocity * 0.5f;
              int index3 = Dust.NewDust(new Vector2((float) Projectile.oldPosition.X - num1, (float) Projectile.oldPosition.Y - num2), 8, 8, DustID.Vortex, (float) Projectile.oldVelocity.X, (float) Projectile.oldVelocity.Y, 100, Color.LightBlue, 1.6f);
              Main.dust[index3].noGravity = true;
              Dust dust2 = Main.dust[index3];
              dust2.velocity = dust2.velocity * 0.5f;
            }
        }
		
    }
}
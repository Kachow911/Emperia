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
			DisplayName.SetDefault("Ethereal Bolt");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 14;       //projectile width
            projectile.height = 14;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            projectile.light = 1f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            /*if (Main.rand.Next(8) == 2)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, 180, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
                Main.dust[num622].velocity += projectile.velocity * 0.2f;
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
                        int b = Dust.NewDust(projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 229);
                        Main.dust[b].noGravity = true;
                        Main.dust[b].velocity = vec;
                    }
                }
            }
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.GetGlobalNPC<MyNPC>().etherealDamages.Add(damage/2);
			target.GetGlobalNPC<MyNPC>().etherealCounts.Add(2);
			
		}
		public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Item10, projectile.position);
            for (int index1 = 4; index1 < 31; ++index1)
            {
              float num1 = (float) (projectile.oldVelocity.X * (30.0 / (double) index1));
              float num2 = (float) (projectile.oldVelocity.Y * (30.0 / (double) index1));
              int index2 = Dust.NewDust(new Vector2((float) projectile.oldPosition.X - num1, (float) projectile.oldPosition.Y - num2), 8, 8, 229, (float) projectile.oldVelocity.X * 2, (float) projectile.oldVelocity.Y * 2, 100, Color.LightBlue, 2f);
              Main.dust[index2].noGravity = true;
              Dust dust1 = Main.dust[index2];
              dust1.velocity = dust1.velocity * 0.5f;
              int index3 = Dust.NewDust(new Vector2((float) projectile.oldPosition.X - num1, (float) projectile.oldPosition.Y - num2), 8, 8, 229, (float) projectile.oldVelocity.X, (float) projectile.oldVelocity.Y, 100, Color.LightBlue, 1.6f);
              Main.dust[index3].noGravity = true;
              Dust dust2 = Main.dust[index3];
              dust2.velocity = dust2.velocity * 0.5f;
            }
        }
		
    }
}
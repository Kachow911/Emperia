using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Stratos
{
    public class StratosMeteorite2 : ModProjectile
    {
        bool init = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stratos Rock");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 14;       //projectile width
            projectile.height = 14;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            projectile.light = 0.1f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {
            if (Main.rand.Next(8) == 2)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, 180, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
                Main.dust[num622].velocity += projectile.velocity * 0.2f;
                Main.dust[num622].noGravity = true;
            }
            if (!init)
            {
                init = true;
                for (int i = 0; i < 360; i++)
                {
                    Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
                    if (i % 8 == 0)
                    {
                        int b = Dust.NewDust(projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 180);
                        Main.dust[b].noGravity = true;
                        Main.dust[b].velocity = vec;
                    }
                }
            }
        }
		
		public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Dig, projectile.Center);
        	for (int i = 0; i < 360; i += 18)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 180, 0f, 0f, 0, new Color(53f, 67f, 253f), 1f);
				Main.dust[num622].velocity += (vec *2f);
				Main.dust[num622].noGravity = true;
			}
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (projectile.Distance(Main.npc[i].Center) < 60  && !Main.npc[i].townNPC)
                    Main.npc[i].StrikeNPC(projectile.damage, 0f, 0, false, false, false);
            }
        }
		
    }
}
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Flasks
{
    public class GoblinFlask1 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Alchemical Flask");
		}
        public override void SetDefaults()
        {
            projectile.width = 25;
            projectile.height = 25;
            projectile.friendly = false;
			projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.aiStyle = 2;
            projectile.timeLeft = 180;
            aiType = 48;
        }
        
        public override void AI()
        {
        	projectile.damage = 15;
        }
        public override void Kill(int timeLeft)
        {
        	Main.PlaySound(SoundID.Item, projectile.Center, 107);  
			for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-32, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                if (i % 8 == 0)
                {   //odd
                    Dust.NewDust(projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 105);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 105, vec.X * 2, vec.Y * 2);
                }
            }
        }
 
    }
}
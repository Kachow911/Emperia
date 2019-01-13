using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Plants
{

    public class plant2 : ModProjectile
    {
		private int explodeRadius = 64;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("AquaticPlant");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 16;      
            projectile.height = 16;  
            projectile.friendly = false;      
            projectile.magic = true;         
            projectile.tileCollide = true;   
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 900;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 0;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                                                           // |
           for (int i = 0; i < 255; i++)
            {
                if (projectile.Distance(Main.player[i].Center) < 64)
                {
                    Main.player[i].AddBuff(mod.BuffType("AquaticBoost"), 60);
                }
            }
           // projectile.velocity.Y = 5;
        }
		public override void Kill(int timeLeft)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (projectile.Distance(Main.npc[i].Center) < explodeRadius)
                    Main.npc[i].StrikeNPC(32, 0f, 0, false, false, false);
            }
            Color rgb = new Color(83, 66, 180);
            for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-explodeRadius, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                if (i % 8 == 0)
                {   //odd
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 76, 0, 0, 0, rgb, 1.1f);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 76, vec.X * 0.5f, vec.Y * 0.5f, 0, rgb, 0.8f);
                }
            }
        }
        
		
    }
}
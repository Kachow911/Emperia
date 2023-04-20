using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Projectiles.Plants
{

    public class plant3 : ModProjectile
    {
		private int explodeRadius = 64;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("AquaticPlant");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 16;      
            Projectile.height = 16;  
            Projectile.friendly = false;      
            Projectile.DamageType = DamageClass.Magic;         
            Projectile.tileCollide = true;   
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 900;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 0;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           // |
           for (int i = 0; i < 255; i++)
            {
                if (Projectile.Distance(Main.player[i].Center) < 64)
                {
                    Main.player[i].AddBuff(ModContent.BuffType<AquaticBoost>(), 60);
                }
            }
            //Projectile.velocity.Y = 5;
        }
		public override void Kill(int timeLeft)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Projectile.Distance(Main.npc[i].Center) < explodeRadius)
                    Main.npc[i].SimpleStrikeNPC(32, 0);
            }
            Color rgb = new Color(83, 66, 180);
            for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-explodeRadius, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                if (i % 8 == 0)
                {   //odd
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 76, 0, 0, 0, rgb, 1.1f);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 76, vec.X * 0.5f, vec.Y * 0.5f, 0, rgb, 0.8f);
                }
            }
        }
        
		
    }
}
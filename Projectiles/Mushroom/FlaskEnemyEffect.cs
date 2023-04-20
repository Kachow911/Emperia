using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{

    public class FlaskEnemyEffect : ModProjectile
    {
		private int explodeRadius = 70;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushroom Gas");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 8;      
            Projectile.height = 8;  
            Projectile.friendly = true;      
            Projectile.DamageType = DamageClass.Magic;         
            Projectile.tileCollide = false;   
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 120;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.5f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {   		// |
			for (int i = 0; i < 5; i++)
			{
				if (Main.rand.Next(3) == 0)
				{
					Dust.NewDust(Projectile.position + new Vector2(Main.rand.Next(-explodeRadius / 2, explodeRadius / 2), Main.rand.Next(-explodeRadius / 2, explodeRadius / 2)), Projectile.width, Projectile.height, 20, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
				}
			}
			for (int i = 0; i < Main.npc.Length; i++)
            {
				if (Projectile.Distance(Main.npc[i].Center) < explodeRadius && Projectile.timeLeft % 24 == 0 && !Main.npc[i].townNPC)
                    Main.npc[i].SimpleStrikeNPC(24, 0);
			}
			
		}

		public override void Kill(int timeLeft)
        {
			//
		}
        
		
    }
}
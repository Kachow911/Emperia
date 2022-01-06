using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{

    public class BigShroom : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Enchanted Mushroom");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 28;  //Projectile height
            Projectile.friendly = false;      //make that the Projectile will not damage you
			Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 240;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
			
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 75;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width / 8, Projectile.height / 8, 20, 0f, 0f, 0, new Color(39, 90, 219), 1.5f);// |
            if (Main.rand.Next(5) == 0)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 41, Projectile.velocity.X * 0.15f, Projectile.velocity.Y * 0.15f);
               
            }
			
        }
        
    }
}
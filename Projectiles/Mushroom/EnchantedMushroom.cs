using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{
	
    public class EnchantedMushroom : ModProjectile
    {
		private bool init = false;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Enchanted Mushroom");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 28;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 180;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.2f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 0;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {             
			if (!init)
			{
				if (Main.rand.NextBool(2))
					Projectile.spriteDirection = -1;
				init = true;
			}
            if (Main.rand.Next(20) == 0)
            {
            	int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width / 8, Projectile.height / 8, DustID.PurificationPowder, 0f, 0f, 0, new Color(39, 90, 219), 1.5f);
            }
			Projectile.alpha++;
        }
        
    }
}
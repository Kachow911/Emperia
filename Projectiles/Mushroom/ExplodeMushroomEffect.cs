using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{

    public class ExplodeMushroomEffect : ModProjectile
    {
		private int explodeRadius = 100;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mushroom Gas");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 8;      
            Projectile.height = 8;  
            Projectile.friendly = false;
			Projectile.hostile = true;			
            Projectile.DamageType = DamageClass.Magic;         
            Projectile.tileCollide = false;   
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 180;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {   		// |
			if (Projectile.timeLeft % 15 == 0)
			{
				for (int i = 0; i < 360; i+= 5)
				{
					if (Main.rand.Next(5) == 0)
					{
						int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width / 8, Projectile.height / 8, 20, 0f, 0f, 0, new Color(39, 90, 219), 1.5f);
						Main.dust[dust].velocity = new Vector2(0, -3).RotatedBy(MathHelper.ToRadians(5 * i));
					}
				}	
			}
			for (int i = 0; i < Main.player.Length; i++)
            {
				if (Projectile.Distance(Main.player[i].Center) < explodeRadius && Projectile.timeLeft % 10 == 0)
                     Main.player[i].Hurt(Terraria.DataStructures.PlayerDeathReason.ByProjectile(Main.player[i].whoAmI, Projectile.whoAmI), Projectile.damage, 0);
			}
			
		}
		public override void Kill(int timeLeft)
        {
			
		}
        
		
    }
}
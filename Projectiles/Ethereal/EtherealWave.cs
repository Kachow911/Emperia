using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Ethereal
{

    public class EtherealWave : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("EtherealWave");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 68;       //Projectile width
            Projectile.height = 36;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 80;   //how many time Projectile Projectile has before disepire
            Projectile.light = 1f;    // Projectile light
            Projectile.ignoreWater = true;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {
            //Projectile.alpha += 4;
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			if (Main.rand.NextBool(2))
			{
				int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 229);
				Main.dust[dust].noGravity = true;
			}
			if (Projectile.timeLeft < 20)
				Projectile.alpha += 10;
			
        }
		 public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.GetGlobalNPC<MyNPC>().etherealDamages.Add(damage/2);
			target.GetGlobalNPC<MyNPC>().etherealCounts.Add(2);
            target.GetGlobalNPC<MyNPC>().etherealSource = Projectile;
        }
        public override void Kill(int timeLeft)
        {
			//
		}
        
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Projectiles
{

    public class DuskProj: ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dusk Blade");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 28;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Melee;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 400;   //how many time this Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the Projectile will face the corect way
        {       
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;	
           if (Projectile.velocity.X > 0)
			{
				Projectile.spriteDirection = 1;
				
			}
			else
			{
				Projectile.spriteDirection = -1;
			}
			
        	if(Projectile.spriteDirection == 1)
        	{
        		Projectile.rotation -= 1.57f;
        	}
			if(Main.rand.Next(2) == 0)
			{
            int num250 = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.velocity.X, Projectile.position.Y - Projectile.velocity.Y), Projectile.width, Projectile.height, 21, (float)(Projectile.direction * 2), 0f, 150, new Color(53f, 67f, 253f), 1.3f);
					Main.dust[num250].noGravity = true;
					Main.dust[num250].velocity *= 0f;
			}
        }
		public override void Kill(int timeLeft)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 21, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<DuskExplosion>(), Projectile.damage / 2, 5f, Projectile.owner);
            }
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			 target.AddBuff(ModContent.BuffType<IndigoInfirmary>(), 240);

		}
    }
}
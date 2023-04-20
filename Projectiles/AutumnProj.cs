using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Projectiles
{
    public class AutumnProj : ModProjectile
    {
		bool init = false;
		Color rgb;
		int timer = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Autumnal Blast");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 30;       //Projectile width
            Projectile.height = 30;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 60;   //how many time this Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the Projectile will face the corect way
        {                                                           // |
           
			if (!init)
			{
				rgb = new Color(50,205,50);

                for (int index1 = 0; index1 < 4; ++index1)
                {
                    int index3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 76, (float)Projectile.velocity.X, (float)Projectile.velocity.Y, 0, rgb, 1.1f);
                    Main.dust[index3].noGravity = true;
                    Main.dust[index3].velocity = Projectile.Center - Main.dust[index3].position;
                    ((Vector2)@Main.dust[index3].velocity).Normalize();
                    Dust dust1 = Main.dust[index3];
                    Vector2 vector2_1 = dust1.velocity * -3f;
                    dust1.velocity = vector2_1;
                    Dust dust2 = Main.dust[index3];
                    Vector2 vector2_2 = dust2.velocity + (Projectile.velocity / 2f);
                    dust2.velocity = vector2_2;
                }
                init = true;
			}
			for (int h = 0; h < 10; ++h)
            {
				int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + Main.rand.Next(32)), (float)(Projectile.position.Y + Main.rand.Next(32))), Projectile.width - 8, Projectile.height - 8, ModContent.DustType<Dusts.GreenBlob>(), (float)(Projectile.velocity.X * 0.200000002980232), (float)(Projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.7f);
				Main.dust[index2].position = Projectile.Center;
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity = Projectile.velocity * 0.5f;
			}
        }
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (Main.rand.Next(3) == 0)
			 target.AddBuff(BuffID.Poisoned, 240);
		}
		public override void Kill(int timeLeft)
        {
			for (int i = -15; i <= 15; i+= 15)
			{
				Vector2 newVel = Projectile.velocity.RotatedBy(MathHelper.ToRadians(i));
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, newVel.X, newVel.Y, ModContent.ProjectileType<VineLeaf>(), Projectile.damage / 2, 1, Main.myPlayer, 0, 0);
        	}

		}
		
    }
}
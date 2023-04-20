using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class SpineVineProj : ModProjectile
    {
		bool init = false;
		Color rgb;
		int timer = 0;
        bool latched = false;

        NPC NPC;
        Vector2 offset;
        float rot;
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Spine Vine");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 30;       //Projectile width
            Projectile.height = 32;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Ranged;
//was thrown pre 1.4         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 2000;   //how many time this Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the Projectile will face the corect way
        {                                                           // |
            timer++;
			if (timer % 5 ==0 && !latched)
			{
				Projectile.velocity.Y += 0.2f;
			}
			if (Projectile.velocity.X > 0)
			{
				Projectile.spriteDirection = 1;
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + (float) (3.14 / 4);
			}
			else if (Projectile.velocity.X < 0)
			{
				Projectile.spriteDirection = -1;
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + (float)((3.14) - (3.14 / 4));
			}
			
			if (!init)
			{
				rgb = new Color(50,205,50);

                for (int index1 = 0; index1 < 4; ++index1)
                {
                    int index3 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Snow, (float)Projectile.velocity.X, (float)Projectile.velocity.Y, 0, rgb, 1.1f);
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
            int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, DustID.Snow, (float)(Projectile.velocity.X * 0.200000002980232), (float)(Projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.7f);
            Main.dust[index2].position += Projectile.velocity.RotatedBy(1.570796, new Vector2());
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = Projectile.velocity.RotatedBy(1.570796, new Vector2()) * 0.33f + Projectile.velocity / 4f;
			int index5 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, DustID.Snow, (float)(Projectile.velocity.X * 0.200000002980232), (float)(Projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.7f);
            Main.dust[index5].position += Projectile.velocity.RotatedBy(1.570796, new Vector2());
            Main.dust[index5].noGravity = true;
            Main.dust[index5].velocity = Projectile.velocity.RotatedBy(-1.570796, new Vector2()) * 0.33f + Projectile.velocity / 4f;
            if (latched)
            {
                if (!NPC.active)
                {
                    Projectile.timeLeft = 0;
                }
                Projectile.velocity = Vector2.Zero;
                Projectile.position = NPC.position + offset;
                Projectile.rotation = rot;
                NPC.GetGlobalNPC<MyNPC>().spineCount += 1;
               // NPC.StrikeNPCNoInteraction(2 * NPC.GetGlobalNPC<MyNPC>().spineCount, 0, 0, false, false, false);
            }
        }
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!latched)
            {
                rot = Projectile.rotation;
                NPC = target;
                offset = Projectile.position - NPC.position;
                latched = true;
                Projectile.timeLeft = 480;
                Projectile.damage = 0;
                Projectile.knockBack = 0f;
				Projectile.tileCollide = false;
            }
            //target.AddBuff(BuffID.Poisoned, 240);
		}
       
    }
}

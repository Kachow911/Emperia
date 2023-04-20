using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Crimson
{
    public class BloodNeedleProj : ModProjectile
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
            Projectile.width = 18;       //Projectile width
            Projectile.height = 54;  //Projectile height
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
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (!latched)
            {
                int index2 = Dust.NewDust(Projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), DustID.GemTopaz);
                Main.dust[index2].noGravity = true;
            }
           // Main.dust[index2].position += Projectile.velocity.RotatedBy(1.570796, new Vector2());
            
            //Main.dust[index2].velocity = Projectile.velocity.RotatedBy(1.570796, new Vector2()) * 0.33f + Projectile.velocity / 4f;
            if (latched)
            {
                if (!NPC.active)
                {
                    Projectile.timeLeft = 0;
                }
                Projectile.velocity = Vector2.Zero;
                Projectile.position = NPC.position + offset;
                Projectile.rotation = rot;
                if (timer % 30 == 0)
                {
                    int index3 = Dust.NewDust(Projectile.Center, 6, 6, DustID.GemTopaz);
                    Dust.NewDust(Projectile.Center, 6, 6, DustID.GemTopaz);
                    Dust.NewDust(Projectile.Center, 6, 6, DustID.GemTopaz);
                    Dust.NewDust(Projectile.Center, 6, 6, DustID.GemTopaz);
                    Dust.NewDust(Projectile.Center, 6, 6, DustID.GemTopaz);
                    Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(Projectile.rotation + 3.14f);
                    Main.dust[index3].velocity = perturbedSpeed;
                    NPC.AddBuff(BuffID.Ichor, 30);
                }
                //NPC.GetGlobalNPC<MyNPC>().spineCount += 1;
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
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; ++i)
            {
                int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GemTopaz, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), .8f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 1f;
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);

        }

    }
}

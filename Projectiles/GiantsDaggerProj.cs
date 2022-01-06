using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Projectiles
{
    public class GiantsDaggerProj : ModProjectile
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
			DisplayName.SetDefault("Giant's Dagger");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 24;       //Projectile width
            Projectile.height = 24;  //Projectile height
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
				Projectile.velocity.Y += 0.05f;
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
			
		
            if (latched)
            {
                if (!NPC.active)
                {
                    Projectile.timeLeft = 0;
                }
                Projectile.velocity = Vector2.Zero;
                Projectile.position = NPC.position + offset;
                Projectile.rotation = rot;
                NPC.GetGlobalNPC<MyNPC>().moreCoins = true;

                // NPC.StrikeNPCNoInteraction(2 * NPC.GetGlobalNPC<MyNPC>().spineCount, 0, 0, false, false, false);
            }
        }
        public override void Kill(int timeLeft)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, (int)Projectile.position.X, (int)Projectile.position.Y, 27);
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 7);
                Vector2 vel = new Vector2(0, -1).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!latched)
            {
                rot = Projectile.rotation;
                NPC = target;
                offset = Projectile.position - NPC.position;
                latched = true;
                Projectile.timeLeft = 360;
                Projectile.damage = 0;
                Projectile.knockBack = 0f;
                NPC.AddBuff(ModContent.BuffType<Bleed>(), 200);
                Projectile.tileCollide = false;
            }
            //target.AddBuff(BuffID.Poisoned, 240);
		}
       
    }
}

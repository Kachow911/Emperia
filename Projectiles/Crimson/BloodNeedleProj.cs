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

        NPC npc;
        Vector2 offset;
        float rot;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spine Vine");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 18;       //projectile width
            projectile.height = 54;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.thrown = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            timer++;
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (!latched)
            {
                int index2 = Dust.NewDust(projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 87);
                Main.dust[index2].noGravity = true;
            }
           // Main.dust[index2].position += projectile.velocity.RotatedBy(1.570796, new Vector2());
            
            //Main.dust[index2].velocity = projectile.velocity.RotatedBy(1.570796, new Vector2()) * 0.33f + projectile.velocity / 4f;
            if (latched)
            {
                if (!npc.active)
                {
                    projectile.timeLeft = 0;
                }
                projectile.velocity = Vector2.Zero;
                projectile.position = npc.position + offset;
                projectile.rotation = rot;
                if (timer % 30 == 0)
                {
                    int index3 = Dust.NewDust(projectile.Center, 6, 6, 87);
                    Dust.NewDust(projectile.Center, 6, 6, 87);
                    Dust.NewDust(projectile.Center, 6, 6, 87);
                    Dust.NewDust(projectile.Center, 6, 6, 87);
                    Dust.NewDust(projectile.Center, 6, 6, 87);
                    Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(projectile.rotation + 3.14f);
                    Main.dust[index3].velocity = perturbedSpeed;
                    npc.AddBuff(BuffID.Ichor, 30);
                }
                //npc.GetGlobalNPC<MyNPC>().spineCount += 1;
               // npc.StrikeNPCNoInteraction(2 * npc.GetGlobalNPC<MyNPC>().spineCount, 0, 0, false, false, false);
            }
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!latched)
            {
                rot = projectile.rotation;
                npc = target;
                offset = projectile.position - npc.position;
                latched = true;
                projectile.timeLeft = 480;
                projectile.damage = 0;
                projectile.knockBack = 0f;
				projectile.tileCollide = false;
            }
            //target.AddBuff(BuffID.Poisoned, 240);
		}
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; ++i)
            {
                int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 87, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), .8f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 1f;
            }
            Main.PlaySound(SoundID.Dig, projectile.Center);

        }

    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class GiantsDaggerProj : ModProjectile
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
			DisplayName.SetDefault("Giant's Dagger");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 24;       //projectile width
            projectile.height = 24;  //projectile height
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
			if (timer % 5 ==0 && !latched)
			{
				projectile.velocity.Y += 0.05f;
			}
			if (projectile.velocity.X > 0)
			{
				projectile.spriteDirection = 1;
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + (float) (3.14 / 4);
			}
			else if (projectile.velocity.X < 0)
			{
				projectile.spriteDirection = -1;
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + (float)((3.14) - (3.14 / 4));
			}
			
		
            if (latched)
            {
                if (!npc.active)
                {
                    projectile.timeLeft = 0;
                }
                projectile.velocity = Vector2.Zero;
                projectile.position = npc.position + offset;
                projectile.rotation = rot;
                npc.GetGlobalNPC<MyNPC>().moreCoins = true;

                // npc.StrikeNPCNoInteraction(2 * npc.GetGlobalNPC<MyNPC>().spineCount, 0, 0, false, false, false);
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 27);
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 7);
                Vector2 vel = new Vector2(0, -1).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
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
                projectile.timeLeft = 360;
                projectile.damage = 0;
                projectile.knockBack = 0f;
                npc.AddBuff(mod.BuffType("Bleed"), 200);
                projectile.tileCollide = false;
            }
            //target.AddBuff(BuffID.Poisoned, 240);
		}
       
    }
}

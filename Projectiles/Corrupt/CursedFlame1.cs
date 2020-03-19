using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Corrupt
{

    public class CursedFlame1 : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Flame");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 16;       //projectile width
            projectile.height = 16;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.melee = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 120;
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
		public override void AI()           //projectile make that the projectile will face the corect way
        {
            for (int i = 0; i < 10; i++)
            {
                int num = Dust.NewDust(projectile.Center, 26, 26, 75, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num].alpha = 0;
                Main.dust[num].position.X = projectile.Center.X - projectile.velocity.X / 10f * (float)i;
                Main.dust[num].position.Y = projectile.Center.Y - projectile.velocity.Y / 10f * (float)i;
                Main.dust[num].noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; ++i)
            {
                int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 3.25f;
            }
            Main.PlaySound(SoundID.Dig, projectile.Center);

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) == 0)
                target.AddBuff(BuffID.CursedInferno, 240);
        }
    }
}
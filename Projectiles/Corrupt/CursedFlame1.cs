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
        {  //Projectile name
            Projectile.width = 16;       //Projectile width
            Projectile.height = 16;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Melee;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
		public override void AI()           //Projectile make that the Projectile will face the corect way
        {
            for (int i = 0; i < 10; i++)
            {
                int num = Dust.NewDust(Projectile.Center, 26, 26, 75, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num].alpha = 0;
                Main.dust[num].position.X = Projectile.Center.X - Projectile.velocity.X / 10f * (float)i;
                Main.dust[num].position.Y = Projectile.Center.Y - Projectile.velocity.Y / 10f * (float)i;
                Main.dust[num].noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; ++i)
            {
                int index2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 75, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 3.25f;
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) == 0)
                target.AddBuff(BuffID.CursedInferno, 240);
        }
    }
}
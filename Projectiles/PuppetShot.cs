using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class PuppetShot : ModProjectile
    {
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Puppeteer's Buckshot");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 14;       //Projectile width
            Projectile.height = 12;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 200;
            Projectile.ignoreWater = true;
			Projectile.alpha = 0;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {
			for (int i =0; i< 3; i++)
			{
				int index2 = Dust.NewDust(Projectile.Center + new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), Projectile.width - 8, Projectile.height - 8, DustID.GoldCoin, 0f, 0f, 0, Color.White, 2f);
				Main.dust[index2].velocity = Vector2.Zero;
			}
        }
        public override void OnKill(int timeLeft)
        {
			for (int i = 0; i < Main.npc.Length; i++)
            {
				if (Projectile.Distance(Main.npc[i].Center) < 64)
                    Main.npc[i].SimpleStrikeNPC(Projectile.damage, 0);
			}
			for (int i = 0; i < 360; i += 5)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-128, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GoldCoin, 0f, 0f, 91, new Color(255, 255, 255), 3f);
                Main.dust[num622].velocity += (vec * 2f);
                Main.dust[num622].noGravity = true;
            }
	     }
    }
}
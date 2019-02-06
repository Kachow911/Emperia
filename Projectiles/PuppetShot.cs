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
			DisplayName.SetDefault("Puppeteer's Buckshot");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 14;       //projectile width
            projectile.height = 12;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 200;
            projectile.ignoreWater = true;
			projectile.alpha = 0;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {
			for (int i =0; i< 3; i++)
			{
				int index2 = Dust.NewDust(projectile.Center + new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), projectile.width - 8, projectile.height - 8, DustID.GoldCoin, 0f, 0f, 0, Color.White, 2f);
				Main.dust[index2].velocity = Vector2.Zero;
			}
        }
        public override void Kill(int timeLeft)
        {
			for (int i = 0; i < Main.npc.Length; i++)
            {
				if (projectile.Distance(Main.npc[i].Center) < 64)
                    Main.npc[i].StrikeNPCNoInteraction(projectile.damage, 0, 0, false, false, false);
			}
			for (int i = 0; i < 360; i += 5)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-128, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.GoldCoin, 0f, 0f, 91, new Color(255, 255, 255), 3f);
                Main.dust[num622].velocity += (vec * 2f);
                Main.dust[num622].noGravity = true;
            }
	     }
    }
}
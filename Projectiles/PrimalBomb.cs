using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class PrimalBomb : ModProjectile
    {
		private int explodeRadius = 100;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Primal Bomb");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 32;       //projectile width
            projectile.height = 32;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 100;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 255;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {       
			if (projectile.timeLeft > 10)
			{
			for (int i = -5; i < 6; i+= 5)
			{
				for (int j = -5; j < 6; j+= 5)
				{
					int dust = Dust.NewDust(new Vector2(projectile.Center.X + i, projectile.Center.Y + j), projectile.width / 4, projectile.height / 4, 89, 0f, 0f, 0, new Color(89, 249, 116), 1.5f);
					Main.dust[dust].velocity = Vector2.Zero;
					Main.dust[dust].scale = 1.5f;
					Main.dust[dust].noGravity = true;
				
					
				} 
			}
			}
		}
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < Main.npc.Length; i++)
            {
                if (projectile.Distance(Main.npc[i].Center) < explodeRadius && !Main.npc[i].townNPC)
					
                     Main.npc[i].StrikeNPC(projectile.damage, 0f, 0, false, false, false);
            }
			for (int i = 0; i < 360; i += 10)
				{
					Vector2 vec = Vector2.Transform(new Vector2(-10, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
					vec.Normalize();
					int num622 = Dust.NewDust(new Vector2(projectile.Center.X, (float) ((double) projectile.Center.Y + (double) projectile.height - 16.0)), projectile.width, 16, 89, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[num622].velocity += (vec *0.2f);
					Main.dust[num622].noGravity = true;
				}
		}
        
    }
}
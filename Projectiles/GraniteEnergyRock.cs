using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class GraniteEnergyRock : ModProjectile
    {
		private int explodeRadius = 100;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energy-Filled Rock");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 8;       //projectile width
            projectile.height = 8;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 200;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			if (Main.rand.Next(5) == 0)
			{
				int num622 = Dust.NewDust(new Vector2(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2), 1, 1, 15, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num622].velocity += projectile.velocity * 0.2f;
			}
		}
		public override void Kill(int timeLeft)
        {
			Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10);
				for (int i = 0; i < Main.npc.Length; i++)
				{
					if (projectile.Distance(Main.npc[i].Center) < explodeRadius)
						Main.npc[i].StrikeNPC(projectile.damage / 2, 0f, 0, false, false, false);
				}
				 for (int i = 0; i < 360; i += 5)
				{
					Vector2 vec = Vector2.Transform(new Vector2(-10, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
					vec.Normalize();
					int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 15, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
					Main.dust[num622].velocity += (vec *1.2f);
				}
		}
        
    }
}
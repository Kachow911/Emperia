using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Granite
{
    public class GraniteRock3 : ModProjectile
    {
		NPC hitNPC;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Granite Rock");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 24;       //projectile width
            projectile.height = 24;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 2000;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			if (Main.rand.Next(5) == 0)
			{
				int num622 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, 240, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num622].velocity += projectile.velocity * 0.2f;
			}
			if (Main.rand.Next(2) == 0)
			{
				int num622 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, 15, 0f, 0f, 74, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num622].velocity += projectile.velocity * 0.4f;
				Main.dust[num622].noGravity = true;
			}
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			hitNPC = target;
		}
		
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 30; ++i)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 15, 0.0f, 0.0f, 15, new Color(53f, 67f, 253f), 2f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 2.7f;
			}
			for (int i = 0; i < Main.npc.Length; i++)
            {
				if (projectile.Distance(Main.npc[i].Center) < 48 && Main.npc[i] != hitNPC)
                    Main.npc[i].StrikeNPC(projectile.damage, 0f, 0, false, false, false);
			}

			
        	
		}
		
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class AutumnProj : ModProjectile
    {
		bool init = false;
		Color rgb;
		int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Autumnal Blast");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 30;       //projectile width
            projectile.height = 30;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 60;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
           
			if (!init)
			{
				rgb = new Color(50,205,50);

                for (int index1 = 0; index1 < 4; ++index1)
                {
                    int index3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 76, (float)projectile.velocity.X, (float)projectile.velocity.Y, 0, rgb, 1.1f);
                    Main.dust[index3].noGravity = true;
                    Main.dust[index3].velocity = projectile.Center - Main.dust[index3].position;
                    ((Vector2)@Main.dust[index3].velocity).Normalize();
                    Dust dust1 = Main.dust[index3];
                    Vector2 vector2_1 = dust1.velocity * -3f;
                    dust1.velocity = vector2_1;
                    Dust dust2 = Main.dust[index3];
                    Vector2 vector2_2 = dust2.velocity + (projectile.velocity / 2f);
                    dust2.velocity = vector2_2;
                }
                init = true;
			}
			for (int h = 0; h < 10; ++h)
            {
				int index2 = Dust.NewDust(new Vector2((float)(projectile.position.X + Main.rand.Next(32)), (float)(projectile.position.Y + Main.rand.Next(32))), projectile.width - 8, projectile.height - 8, mod.DustType("GreenBlob"), (float)(projectile.velocity.X * 0.200000002980232), (float)(projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.7f);
				Main.dust[index2].position = projectile.Center;
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity = projectile.velocity * 0.5f;
			}
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (Main.rand.Next(3) == 0)
			 target.AddBuff(BuffID.Poisoned, 240);
		}
		public override void Kill(int timeLeft)
        {
			for (int i = -15; i <= 15; i+= 15)
			{
				Vector2 newVel = projectile.velocity.RotatedBy(MathHelper.ToRadians(i));
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, newVel.X, newVel.Y, mod.ProjectileType("VineLeaf"), projectile.damage / 2, 1, Main.myPlayer, 0, 0);
        	}

		}
		
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Emperia;

namespace Emperia.Projectiles
{
    public class HauntedRevolver : ModProjectile
    {
        private const int shootRate = 32;
        private float projectileNumber { get { return projectile.ai[1]; } set { projectile.ai[1] = value; } }
		private float rotate2 = 0;
		private Vector2 relativePos;
		private Vector2 targetPosition;
		private String mode = "Hovering";
		private int timer = 0;
		private bool init = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Haunted Revolver");
		}
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 22;
            projectile.friendly = true;
            //projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 1200;
            projectile.light = 0.75f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 75;
			projectile.scale = 0.8f;
            projectile.aiStyle = -1;
        }

        public override void AI()
		{
			timer++;
			Player player = Main.player[projectile.owner];
			projectile.velocity = Vector2.Zero;
			if (!init)
			{
				/*if (projectileNumber == 1)
					relativePos = new Vector2(-64, -64);
				else if (projectileNumber == 2)
					relativePos = new Vector2(64, -64);
				else if (projectileNumber == 3)
					relativePos = new Vector2(-64, 64);
				else if (projectileNumber == 4)
					relativePos = new Vector2(64, 64);
				Vector2 targetPosition = player.Center + relativePos;*/
				relativePos = new Vector2(82, 0).RotatedBy(MathHelper.ToRadians(Main.rand.Next(360)));
				init = true;
			}
			targetPosition = player.Center + relativePos;
			if (projectile.Distance(targetPosition) > 32)
			{
				projectile.velocity += (targetPosition - projectile.Center) * 0.02f;
			}
			else
			{
				projectile.velocity.X = 0;
				projectile.velocity.Y = 0.5f * (float)Math.Cos(MathHelper.ToRadians(timer * 2));
			}
			Vector2 lineVec = player.Center - projectile.Center;
			int length = (int) (Math.Sqrt((lineVec.X * lineVec.X) + (lineVec.Y * lineVec.Y)));
			if (timer % 15 == 0)
			{
				int index2 = Dust.NewDust(new Vector2((float)(projectile.position.X + 4.0), (float)(projectile.position.Y + 4.0)), projectile.width - 8, projectile.height - 8, 15, 0, 0, 0, Color.White, 1.2f);
				Main.dust[index2].position = projectile.Center;
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity = (player.Center - projectile.Center) * 0.1f;
			}
			
			Vector2 rotVector = Main.MouseWorld - projectile.Center;
			if (player.controlUseItem)
			{
				if (timer % 60 == 0)
				{
					rotVector.Normalize();
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, rotVector.X * 12f, rotVector.Y * 12f, mod.ProjectileType("HrP2"), 20, 1, Main.myPlayer, 0, 0);
				}
				if ((double) Main.MouseWorld.X > projectile.position.X)
				{
					projectile.spriteDirection = -1;
					projectile.rotation = (float) Math.Atan2((double) rotVector.Y, (double) rotVector.X);
				}
				else
				{
					projectile.spriteDirection = 1;
					projectile.rotation = (float) Math.Atan2(-(double) rotVector.Y, -(double) rotVector.X);
				}
			}
			else 
			{
				if (projectile.velocity.X > 0)
					projectile.spriteDirection = -1;
				else if (projectile.velocity.X < 0)
					projectile.spriteDirection = 1;
				projectile.rotation = 0;
			}
	
			

        }
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 360; i += 10)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-16, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 15, 0f, 0f, 91, new Color(255, 255, 255), 1.5f);
                Main.dust[num622].velocity += (vec * 2f);
                Main.dust[num622].noGravity = true;
            }
	     }
       /* public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }*/
	
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class BlueSword : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blue Day's Blade");
		}
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.friendly = true;   
            projectile.melee = true;      
            projectile.tileCollide = false;
            projectile.penetrate = 2;     
            projectile.timeLeft = 400;
            projectile.light = 0.75f;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 0.785f;
			projectile.alpha = 100 + (int) (Math.Cos(projectile.timeLeft) * 100);
			if(Main.rand.Next(2) == 0)
			{
				int num250 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 187, (float)(projectile.direction * 2), 0f, 150, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num250].noGravity = true;
				Main.dust[num250].velocity *= 0f;
			}
			Player player = Main.player[projectile.owner];
			if (projectile.Center.Y > player.Center.Y - player.height * 4)
			{
				projectile.tileCollide = true;
			}
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{	
			target.immune[projectile.owner] = 5;
		}
		public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Item10, projectile.position);
            for (int index1 = 4; index1 < 31; ++index1)
            {
              float num1 = (float) (projectile.oldVelocity.X * (30.0 / (double) index1));
              float num2 = (float) (projectile.oldVelocity.Y * (30.0 / (double) index1));
              int index2 = Dust.NewDust(new Vector2((float) projectile.oldPosition.X - num1, (float) projectile.oldPosition.Y - num2), 8, 8, 187, (float) projectile.oldVelocity.X * 2, (float) projectile.oldVelocity.Y * 2, 100, Color.LightBlue, 2f);
              Main.dust[index2].noGravity = true;
              Dust dust1 = Main.dust[index2];
              dust1.velocity = dust1.velocity * 0.5f;
              int index3 = Dust.NewDust(new Vector2((float) projectile.oldPosition.X - num1, (float) projectile.oldPosition.Y - num2), 8, 8, 187, (float) projectile.oldVelocity.X, (float) projectile.oldVelocity.Y, 100, Color.LightBlue, 1.6f);
              Main.dust[index3].noGravity = true;
              Dust dust2 = Main.dust[index3];
              dust2.velocity = dust2.velocity * 0.5f;
            }
        }
    }
}
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
            projectile.penetrate = 1;     
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
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++) {
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
    }
}
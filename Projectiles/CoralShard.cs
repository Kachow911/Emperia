using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class CoralShard : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coral Shard");
		}
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 140;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			//projectile.light = 0.8f;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.position, 0f, 0.3f, 0f);
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (projectile.timeLeft % 2 == 0)
            {
                int dust = Dust.NewDust(projectile.Center, projectile.width / 2, projectile.height / 2, 107, projectile.velocity.X, projectile.velocity.Y, 0, default(Color), 0.6f);
                Main.dust[dust].velocity *= 0.5f;
            }
        
        }
		public override void Kill(int timeLeft)
		{
            Main.PlaySound(SoundID.Item10, projectile.position);
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 46);
			    Main.dust[dust].noGravity = false;
                int dust2 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 107, 0.0f, 0.0f, 15, default(Color), 0.8f);
				Main.dust[dust2].velocity *= 1.5f;
                int dust2copy = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 107, 0.0f, 0.0f, 15, default(Color), 0.8f);
                Vector2 vel = new Vector2(0, -1).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
			}
            //{
				//int index2 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 107, 0.0f, 0.0f, 15, default(Color), 1f);
				//Main.dust[index2].noGravity = true;
				//Main.dust[index2].velocity *= 2f;
			//}
		}
        //public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		//{
		//	spriteBatch.Draw(mod.GetTexture("Projectiles/Cerith_Glow"), projectile.Center - Main.screenPosition, null, Color.White, 0f, new Vector2(11f, 19f), projectile.scale, SpriteEffects.None, 0f);
        //}
    }
}

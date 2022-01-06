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
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 140;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			//Projectile.light = 0.8f;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.position, 0f, 0.3f, 0f);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            if (Projectile.timeLeft % 2 == 0)
            {
                int dust = Dust.NewDust(Projectile.Center, Projectile.width / 2, Projectile.height / 2, 107, Projectile.velocity.X, Projectile.velocity.Y, 0, default(Color), 0.6f);
                Main.dust[dust].velocity *= 0.5f;
            }
        
        }
		public override void Kill(int timeLeft)
		{
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 46);
			    Main.dust[dust].noGravity = false;
                int dust2 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, 107, 0.0f, 0.0f, 15, default(Color), 0.8f);
				Main.dust[dust2].velocity *= 1.5f;
                int dust2copy = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, 107, 0.0f, 0.0f, 15, default(Color), 0.8f);
                Vector2 vel = new Vector2(0, -1).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
			}
            //{
				//int index2 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, 107, 0.0f, 0.0f, 15, default(Color), 1f);
				//Main.dust[index2].noGravity = true;
				//Main.dust[index2].velocity *= 2f;
			//}
		}
        //public override void PostDraw(ref Color lightColor)
		//{
		//	Main.EntitySpriteDraw(Mod.Assets.Request<Texture2D>("Projectiles/Cerith_Glow").Value, Projectile.Center - Main.screenPosition, null, Color.White, 0f, new Vector2(11f, 19f), Projectile.scale, SpriteEffects.None, 0);
        //}
    }
}

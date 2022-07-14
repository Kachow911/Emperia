using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Yeti
{

    public class PineconeGrenade : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pinecone Grenade");
		}
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 40;
            Projectile.ignoreWater = false;
            Projectile.aiStyle = 2;
        }
		public override void Kill(int timeLeft)
		{
            for (int i = 0; i < 7; i++)
            {
			    int smokeDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0.0f, 0.0f, 60, new Color(53f, 67f, 253f), 1.5f);
				Main.dust[smokeDust].velocity *= 3.5f;
				Main.dust[smokeDust].noGravity = true;
			    int sparkDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0.0f, 0.0f, 0, default(Color), 1.5f);
				Main.dust[sparkDust].velocity *= 2f;
                int barkDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 155, 0.0f, 0.0f, 0, default(Color), 1.3f);
				Main.dust[barkDust].velocity *= 4f;
                Main.dust[barkDust].noGravity = true;
            }
            for (int i = 0; i < 4; i++)
            {
                Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.position, new Vector2(0.25f, 0.25f).RotatedByRandom(MathHelper.ToRadians(360)), Main.rand.Next(61, 63), 1f);
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
		}
    }
}

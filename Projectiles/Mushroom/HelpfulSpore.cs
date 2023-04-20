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

namespace Emperia.Projectiles.Mushroom
{
    public class HelpfulSpore : ModProjectile
    {
        private const float explodeRadius = 32;
        private float rotate { get { return Projectile.ai[1]; } set { Projectile.ai[1] = value; } }
		private float rotate2 = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Explosive Spore");
		}
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            //Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.light = 0.75f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 75;
            Projectile.aiStyle = -1;
        }

        public override void AI()
		{
			Player player = Main.player[Projectile.owner];
            Vector2 rotatePosition = Vector2.Transform(new Vector2(128, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(rotate * 60 + rotate2))) + player.Center;
            Projectile.Center = rotatePosition;

            rotate2 += .5f;
			if (Main.rand.Next(20) == 0)
            {
            	int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width / 8, Projectile.height / 8, DustID.PurificationPowder, 0f, 0f, 0, new Color(39, 90, 219), 0.75f);
            }

        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }
		public override void Kill(int timeLeft) 
		{
			MyPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<MyPlayer>();
			modPlayer.sporeCount--;
			for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Projectile.Distance(Main.npc[i].Center) < explodeRadius)
                     Main.npc[i].SimpleStrikeNPC(Projectile.damage, 0);
            }

            for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-explodeRadius, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                if (i % 8 == 0)
                {   //odd
                    Dust.NewDust(Projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), DustID.PurificationPowder);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(Projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), DustID.PurificationPowder, vec.X * 2, vec.Y * 2);
                }
            }

            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);    //bomb explosion sound
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item21, Projectile.Center);    //swishy sound
			
		}
    }
}

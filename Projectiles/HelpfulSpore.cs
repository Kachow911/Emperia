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
    public class HelpfulSpore : ModProjectile
    {
        private const float explodeRadius = 32;
        private float rotate { get { return projectile.ai[1]; } set { projectile.ai[1] = value; } }
		private float rotate2 = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Explosive Spore");
		}
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            //projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.light = 0.75f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 75;
            projectile.aiStyle = -1;
        }

        public override void AI()
		{
			Player player = Main.player[projectile.owner];
            Vector2 rotatePosition = Vector2.Transform(new Vector2(128, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(rotate * 60 + rotate2))) + player.Center;
            projectile.Center = rotatePosition;

            rotate2 += .5f;
			if (Main.rand.Next(20) == 0)
            {
            	int dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width / 8, projectile.height / 8, 20, 0f, 0f, 0, new Color(39, 90, 219), 0.75f);
            }

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }
		public override void Kill(int timeLeft) 
		{
			MyPlayer modPlayer = Main.player[projectile.owner].GetModPlayer<MyPlayer>();
			modPlayer.sporeCount--;
			for (int i = 0; i < Main.npc.Length; i++)
            {
                if (projectile.Distance(Main.npc[i].Center) < explodeRadius)
                     Main.npc[i].StrikeNPC(projectile.damage, 0f, 0, false, false, false);
            }

            for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-explodeRadius, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                if (i % 8 == 0)
                {   //odd
                    Dust.NewDust(projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20, vec.X * 2, vec.Y * 2);
                }
            }

            Main.PlaySound(SoundID.Item, projectile.Center, 14);    //bomb explosion sound
            Main.PlaySound(SoundID.Item, projectile.Center, 21);    //swishy sound
			
		}
    }
}

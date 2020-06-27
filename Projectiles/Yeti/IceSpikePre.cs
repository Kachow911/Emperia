using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Yeti
{

    public class IceSpikePre : ModProjectile
    {
        int timer = 0;
        private Point tileCoordPos { get { return new Point((int)(projectile.position.X / 16), (int)(projectile.position.Y / 16)); } }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ball");
		}
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 90;
            projectile.light = 0.75f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
            projectile.hide = true;
        }
        public override void AI()
        {

            if (timer % 7 == 0)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 10, 0, -1, mod.ProjectileType("IceSpike"), projectile.damage, 1, Main.myPlayer, 0, 0);
            }
            timer++;
            projectile.velocity.Y = 0;
            bool foundbelow = false;
            for (int i = 0; i < 16; i++)
            {
                Tile below = Framing.GetTileSafely(tileCoordPos.X, tileCoordPos.Y + i);

                if (below.active() && below.collisionType > 0)
                {
                    if (i == 0) //if it's inside the tile
                    {
                        bool foundabove = false;
                        for (int j = 1; j <= 3; j++)
                        {
                            Tile above = Framing.GetTileSafely(tileCoordPos.X, tileCoordPos.Y - j);

                            if (!above.active())
                            {
                                projectile.position.Y = (tileCoordPos.Y - j) * 16;
                                foundabove = true;
                                break;
                            }
                        }

                        if (!foundabove)
                            projectile.Kill();
                    }
                    else
                    {
                        projectile.position.Y = (tileCoordPos.Y + i - 1) * 16;
                        foundbelow = true;
                        break;
                    }
                }
            }
            if (!foundbelow)
                projectile.Kill();
        }

        public override bool? CanHitNPC(NPC target)
		{
            return false;
		}
        
    }
    public class IceSpike : ModProjectile
    {
		private bool init = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Spike");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 10;       //projectile width
            projectile.height = 14;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
			projectile.hostile = false;
            projectile.melee = true;         // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many projectile will penetrate
            projectile.timeLeft = 50;   //how many time projectile projectile has before disepire
            projectile.light = 0f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			projectile.alpha = 0;
            drawOriginOffsetY = 2;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {
            projectile.scale = 1.1f;
            projectile.velocity *= .90f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) == 0)
            {
			    target.AddBuff(BuffID.Frostburn, 240);
            }
		}
        public override void Kill(int timeLeft)
		{
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, (float) projectile.velocity.X / 10, (float) projectile.velocity.Y / 10, 0, default(Color), 0.7f);
		}
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Desert
{

    public class CarapaceCrusherProj1 : ModProjectile
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
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 10, 0, -1, mod.ProjectileType("DesertSpike"), projectile.damage / 2, 1, Main.myPlayer, 0, 0);
            }
            timer++;
            //Dust.NewDust(new Vector2(projectile.Hitbox.X, projectile.Hitbox.Y), Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20, projectile.velocity.X, projectile.velocity.Y);
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
        public override void Kill(int timeLeft)
        {
           // Main.PlaySound(SoundID.Item10, projectile.position);
            /*for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 7);
                Vector2 vel = new Vector2(0, -1).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
            }*/
        }

        public override bool? CanHitNPC(NPC target)
		{
            return false;
		}
        
    }
}

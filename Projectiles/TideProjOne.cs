using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles
{
	public class TideProjOne : ModProjectile
	{
		int projNum = 1;
		private Point tileCoordPos { get { return new Point((int)(projectile.position.X / 16), (int)(projectile.position.Y / 16)); } }
		public override void SetDefaults()
		{
			projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = false;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 120;
            projectile.light = 0.75f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;

		}
		
	

		public override void AI()
		{
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
                        //else break; //we can safely break since if foundabove == true it'll already be above tiles.
                    }
                    else
                    {
                        projectile.position.Y = (tileCoordPos.Y + i - 1) * 16;
                        foundbelow = true;
                        break;
                    }
                }
            }
            if (!foundbelow)    //this will only be the case if it's inside a tile.
                projectile.Kill();
			
			if (projectile.timeLeft % 10 == 0)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 10, 0, 1, mod.ProjectileType("Wave"), 10, 1, Main.myPlayer, 0, 0);

			}
		}
	}
	
}
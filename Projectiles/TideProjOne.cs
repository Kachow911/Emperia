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
		private Point tileCoordPos { get { return new Point((int)(Projectile.position.X / 16), (int)(Projectile.position.Y / 16)); } }
		public override void SetDefaults()
		{
			Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.light = 0.75f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;

		}
		
	

		public override void AI()
		{
			Projectile.velocity.Y = 0;
            bool foundbelow = false;
            for (int i = 0; i < 16; i++)
            {
                Tile below = Framing.GetTileSafely(tileCoordPos.X, tileCoordPos.Y + i);

                if (below.HasTile && Main.tileSolid[below.TileType])
                {
                    if (i == 0) //if it's inside the tile
                    {
                        bool foundabove = false;
                        for (int j = 1; j <= 3; j++)
                        {
                            Tile above = Framing.GetTileSafely(tileCoordPos.X, tileCoordPos.Y - j);

                            if (!above.HasTile)
                            {
                                Projectile.position.Y = (tileCoordPos.Y - j) * 16;
                                foundabove = true;
                                break;
                            }
                        }

                        if (!foundabove)
                            Projectile.Kill();
                        //else break; //we can safely break since if foundabove == true it'll already be above tiles.
                    }
                    else
                    {
                        Projectile.position.Y = (tileCoordPos.Y + i - 1) * 16;
                        foundbelow = true;
                        break;
                    }
                }
            }
            if (!foundbelow)    //this will only be the case if it's inside a tile.
                Projectile.Kill();
			
			if (Projectile.timeLeft % 10 == 0)
			{
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y - 10, 0, 1, ModContent.ProjectileType<Wave>(), 10, 1, Main.myPlayer, 0, 0);

			}
		}
	}
	
}
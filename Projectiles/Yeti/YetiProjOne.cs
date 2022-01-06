using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles.Yeti
{
	public class YetiProjOne : ModProjectile
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

                if (below.IsActive && below.CollisionType > 0)
                {
                    if (i == 0) //if it's inside the tile
                    {
                        bool foundabove = false;
                        for (int j = 1; j <= 3; j++)
                        {
                            Tile above = Framing.GetTileSafely(tileCoordPos.X, tileCoordPos.Y - j);

                            if (!above.IsActive)
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
				if (projNum % 2 == 0)
					Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, 0, 1, ModContent.ProjectileType<IcicleA>(), 25, 1, Main.myPlayer, 0, 0);
				else
					Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, 0, 1, ModContent.ProjectileType<IcicleB>(), 25, 1, Main.myPlayer, 0, 0);
				projNum++;
				//Terraria.Audio.SoundEngine.PlaySound(Projectile.position, SoundLoader.GetSoundSlot(SoundType.Custom, "Sounds/Icicle"));
			}
		}
	}
	
}
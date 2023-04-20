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
        private Point tileCoordPos { get { return new Point((int)(Projectile.position.X / 16), (int)(Projectile.position.Y / 16)); } }
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ball");
		}
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90;
            Projectile.light = 0.75f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.hide = true;
        }
        public override void AI()
        {

            if (timer % 7 == 0)
            {
                Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y + 10, 0, -1, ModContent.ProjectileType<IceSpike>(), Projectile.damage, 1, Main.myPlayer, 0, 0);
            }
            timer++;
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
                    }
                    else
                    {
                        Projectile.position.Y = (tileCoordPos.Y + i - 1) * 16;
                        foundbelow = true;
                        break;
                    }
                }
            }
            if (!foundbelow)
                Projectile.Kill();
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
			// DisplayName.SetDefault("Ice Spike");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 10;       //Projectile width
            Projectile.height = 14;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
			Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;         // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many Projectile will penetrate
            Projectile.timeLeft = 50;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 0;
            DrawOriginOffsetY = 2;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {
            Projectile.scale = 1.1f;
            Projectile.velocity *= .90f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(3) == 0)
            {
			    target.AddBuff(BuffID.Frostburn, 240);
            }
		}
        public override void Kill(int timeLeft)
		{
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueCrystalShard, (float) Projectile.velocity.X / 10, (float) Projectile.velocity.Y / 10, 0, default(Color), 0.7f);
		}
    }
}

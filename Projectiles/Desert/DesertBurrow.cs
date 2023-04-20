using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Desert;

namespace Emperia.Projectiles.Desert
{

    public class DesertBurrow : ModProjectile
    {
        NPC NPC;

        bool init = false;
        int timer = 0;
        private Point tileCoordPos { get { return new Point((int)(Projectile.position.X / 16), (int)(Projectile.position.Y / 16)); } }
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Desert Burrow");
		}
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
            Projectile.light = 0.75f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.knockBack = 0;
            //Projectile.damage = 1;
            //Projectile.hide = true;
			Main.projFrames[Projectile.type] = 5;
        }
        public override void AI()
        {
			if(!init)
			{
				init = true;
			    Player player = Main.player[Projectile.owner];
			    Projectile.velocity.X = 2 * player.direction;
                Projectile.spriteDirection = player.direction;
                //Projectile.direction = player.direction;
			}
            //if (timer % 7 == 0)
            //{
            //    Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y + 10, 0, -1, ModContent.ProjectileType<DesertSpike>(), Projectile.damage / 2, 1, Main.myPlayer, 0, 0);
            //}
            timer++;
            //Dust.NewDust(new Vector2(Projectile.Hitbox.X, Projectile.Hitbox.Y), Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20, Projectile.velocity.X, Projectile.velocity.Y);
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
                                Projectile.position.Y = (tileCoordPos.Y - j) * 16 + 6;
                                foundabove = true;
                                break;
                            }
                        }

                        if (!foundabove)
                            Projectile.Kill();
                    }
                    else
                    {
                        Projectile.position.Y = (tileCoordPos.Y + i - 1) * 16 + 4;
                        foundbelow = true;
                        break;
                    }
                }
            }
            if (!foundbelow)
                Projectile.Kill();

			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 10)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 5;
			}
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.desertSpikeDirection = Projectile.direction;
            //Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X + (4 * Projectile.direction), Projectile.Center.Y - 21, 0, 0, ModContent.ProjectileType<DesertSpikeBig>(), 0, 0, Main.myPlayer, 0, 0);
    		Projectile.NewProjectile(Projectile.InheritSource(Projectile), target.Center.X - (2 * Projectile.direction), Projectile.Center.Y - 21, 0, 0, ModContent.ProjectileType<DesertSpikeBig>(), 0, 0, Main.myPlayer, 0, 0);
            NPC = target;
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item70, Projectile.Center);
            if (NPC.knockBackResist > 0f)
            {
                NPC.GetGlobalNPC<MyNPC>().desertSpikeTime = -8;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.CarapaceDust>());
                Vector2 vel = new Vector2(Projectile.velocity.X * -2, -1);
            }
        }
    }
}

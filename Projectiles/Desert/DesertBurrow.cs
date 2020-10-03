using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Desert
{

    public class DesertBurrow : ModProjectile
    {
        NPC npc;

        bool init = false;
        int timer = 0;
        private Point tileCoordPos { get { return new Point((int)(projectile.position.X / 16), (int)(projectile.position.Y / 16)); } }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Burrow");
		}
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = 1;
            projectile.timeLeft = 180;
            projectile.light = 0.75f;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
            projectile.knockBack = 0;
            //projectile.damage = 1;
            //projectile.hide = true;
			Main.projFrames[projectile.type] = 5;
        }
        public override void AI()
        {
			if(!init)
			{
				init = true;
			    Player player = Main.player[projectile.owner];
			    projectile.velocity.X = 2 * player.direction;
                projectile.spriteDirection = player.direction;
                //projectile.direction = player.direction;
			}
            //if (timer % 7 == 0)
            //{
            //    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 10, 0, -1, mod.ProjectileType("DesertSpike"), projectile.damage / 2, 1, Main.myPlayer, 0, 0);
            //}
            timer++;
            //Dust.NewDust(new Vector2(projectile.Hitbox.X, projectile.Hitbox.Y), Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20, projectile.velocity.X, projectile.velocity.Y);
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
                                projectile.position.Y = (tileCoordPos.Y - j) * 16 + 6;
                                foundabove = true;
                                break;
                            }
                        }

                        if (!foundabove)
                            projectile.Kill();
                    }
                    else
                    {
                        projectile.position.Y = (tileCoordPos.Y + i - 1) * 16 + 4;
                        foundbelow = true;
                        break;
                    }
                }
            }
            if (!foundbelow)
                projectile.Kill();

			projectile.frameCounter++;
			if (projectile.frameCounter >= 10)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 5;
			}
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.desertSpikeDirection = projectile.direction;
            //Projectile.NewProjectile(projectile.Center.X + (4 * projectile.direction), projectile.Center.Y - 21, 0, 0, mod.ProjectileType("DesertSpikeBig"), 0, 0, Main.myPlayer, 0, 0);
    		Projectile.NewProjectile(target.Center.X - (2 * projectile.direction), projectile.Center.Y - 21, 0, 0, mod.ProjectileType("DesertSpikeBig"), 0, 0, Main.myPlayer, 0, 0);
            npc = target;
            Main.PlaySound(SoundID.Item70, projectile.Center);
            if (npc.knockBackResist > 0f)
            {
                npc.GetGlobalNPC<MyNPC>().desertSpikeTime = -8;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("CarapaceDust"));
                Vector2 vel = new Vector2(projectile.velocity.X * -2, -1);
            }
        }
    }
}

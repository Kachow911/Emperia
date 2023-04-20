using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;

namespace Emperia.Projectiles
{
	
    public class OsmiumSpike : ModProjectile
    {
        NPC NPC;

		private int initCounter = 0;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Osmium Spike");
            ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type] = true;
		}
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = false;
			Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 60;
            Projectile.light = 0f;
            Projectile.ignoreWater = true;
			Projectile.alpha = 0;
            Projectile.damage = 0;
            Projectile.hide = true;
            Main.projFrames[Projectile.type] = 4;
            //DrawOriginOffsetY = -12;
            DrawOffsetX = -8;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            initCounter++;
            if (initCounter == 1)
            {
                Projectile.direction = modPlayer.desertSpikeDirection;
                Projectile.spriteDirection = modPlayer.desertSpikeDirection;
            }

            if (initCounter == 9)
            {
                Projectile.damage = 18;
           } 

            if (Projectile.penetrate == 1)
            {
                if (NPC.GetGlobalNPC<MyNPC>().desertSpikeTime == 0 && initCounter > 10 && Projectile.timeLeft > 5)
                {
                    Projectile.timeLeft = 5;
                }
            }

            //animation stuff
            Projectile.frameCounter++;

			if (Projectile.frameCounter >= 4 && Projectile.frame < 3 && Projectile.timeLeft >= 4)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 4;
			}

            if (Projectile.timeLeft == 4)
            {
                Projectile.frame = 1;
            }
        }
        public override bool? CanHitNPC(NPC target)
		{
            if (Projectile.penetrate == 1 | initCounter > 10)
            {
                return false;
            }
            else return null;
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            NPC = target;
            NPC.GetGlobalNPC<MyNPC>().impaledDirection = NPC.direction;
		}
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
            NPC = target;
            if (NPC.knockBackResist > 0f)
            {
                NPC.GetGlobalNPC<MyNPC>().desertSpikeTime = 100;
                NPC.GetGlobalNPC<MyNPC>().desertSpikeHeight = Projectile.Top.Y + 28;
            }
		}
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCs.Add(index);
		}
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width / 2, Projectile.height, 6);
                //Vector2 vel = new Vector2(Projectile.velocity.X * -2, -1);
            }
        }
    }
}
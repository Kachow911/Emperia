using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Volcano
{
    public class EmberTyrant : ModNPC
    {
        private enum Move
        {
            Hover,
			Go,
			BigShoot
        }

        private Move move { get { return (Move)npc.ai[0]; } set { npc.ai[0] = (int)value; } }
        private Move prevMove;
		bool init = false;
		private int counter = 300;

		private Vector2 targetPosition;
		
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ember Tyrant");
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 2500;
            npc.damage = 75;
            npc.defense = 20;
            npc.knockBackResist = 0f;
            npc.width = 94;
            npc.height = 100;
			npc.alpha = 0;
            npc.value = Item.buyPrice(0, 5, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = false;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
			npc.ai[3] = 0; //phase: 0 is creation, 1 is first, 2 is second
			npc.frameCounter = 0;
			
            npc.netAlways = true;

        }

        

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 3200;
            npc.damage = 100;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
			npc.TargetClosest(true);
			if (!init)
			{
				move = Move.Hover;
				init = true;
			}
            if (!player.active || player.dead)
			{
				npc.TargetClosest(false);
				npc.velocity.X = 0;
				npc.velocity.Y = 15;
				npc.timeLeft = 10;
			}
			
			
			if (move == Move.Hover)
			{
				counter--;
				npc.velocity.X = 0;
				npc.velocity.Y = 0.5f * (float)Math.Cos(MathHelper.ToRadians(counter * 2));
				if (counter % 100 == 0)
				{
					Vector2 Speed = player.Center - npc.Center;
					Speed.Normalize();
					Speed *= 10f;
					for (int i = 0; i < 3; i++)
					{
						 Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 25, Speed.X * Main.rand.Next(3, 8), Speed.Y * Main.rand.Next(3, 8), mod.ProjectileType("ScorchBlastHost"), 25, 2f, player.whoAmI);
					}
				}
				if (counter <= 0)
				{
					if (Main.rand.NextBool(2))
						SetMove(Move.Go, 0);
					else
						SetMove(Move.BigShoot, 120);
				}
			}
			if (move == Move.Go)
			{
				
				if (npc.Distance(player.Center) < 64)
					SetMove(Move.Hover, 300);
			}
        }
        private void SetMove(Move move, int counter)
        {
            this.prevMove = this.move;
            this.move = move;
			this.counter = counter;
        }

        private bool IsInPhaseTwo()
        {
            return npc.life <= npc.lifeMax * .5;    //50% hp
        }
    }
}

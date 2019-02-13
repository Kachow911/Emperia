using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Inquisitor
{
    public class Inquisitor : ModNPC
    {
        private enum Move
        {
            Hover,
			Teleport,
			Shoot
        }

        private Move move { get { return (Move)npc.ai[0]; } set { npc.ai[0] = (int)value; } }
        private Move prevMove;
		
		private int counter;

		private Vector2 targetPosition;
		
		private int laserSweep = 60;
		private int animationFps = 60 / 10; //first value is tickrate, second is the fps that you want

        private bool phaseStart;
        private bool phaseEnd;
		
		private bool spawnedMasks = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Inquisitor");
			Main.npcFrameCount[npc.type] = 6;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 10000;
            npc.damage = 150;
            npc.defense = 40;
            npc.knockBackResist = 0f;
            npc.width = 94;
            npc.height = 100;
			npc.alpha = 0;
            npc.value = Item.buyPrice(0, 20, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
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

        public override void FindFrame(int frameHeight)
        {
			npc.frameCounter += 0.2f;
			npc.frameCounter %= 6; 
			int frame = (int)npc.frameCounter; 
			npc.frame.Y = frame * frameHeight; 
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 16000;
            npc.damage = 175;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
			npc.TargetClosest(true);
            if (!player.active || player.dead)
			{
				npc.TargetClosest(false);
				npc.velocity.X = -15;
				npc.timeLeft = 10;
			}
			
			if (!spawnedMasks)
			{
				int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AgonyMask"), ai0: npc.whoAmI, ai1: 1);
				spawnedMasks = true;
			}
            if (npc.ai[3] == 0 && npc.life <= npc.lifeMax * .9)
            {
				SetMove(Move.Hover, 240);
                NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, mod.NPCType("EocPuppet"), ai0: player.whoAmI, ai1: npc.whoAmI);
                /*NPC.NewNPC((int)npc.Center.X + 20, (int)npc.Center.Y, mod.NPCType<AgonyMask>(), ai0: player.whoAmI, ai1: npc.whoAmI);*/
                npc.ai[3] = 1;
            }
			if (move == Move.Hover)
			{
				counter--;
				npc.velocity.X = 0;
				npc.velocity.Y = 0.5f * (float)Math.Cos(MathHelper.ToRadians(counter * 2));
				if (counter <= 0)
				{
					if (Main.rand.NextBool(2))
						SetMove(Move.Teleport, 0);
					else
						SetMove(Move.Shoot, 120);
				}
			}
			if (move == Move.Teleport)
			{
				bool sucess = false;
				while (!sucess)
				{
					Vector2 targPos = player.Center + new Vector2(200, 0).RotatedByRandom(MathHelper.ToRadians(360));
					if (targPos.Y < player.Center.Y)
					{
						sucess = true;
						for (int i = 0; i < 50; ++i) //Create dust b4 teleport
						{
							int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.GoldCoin);
							int dust1 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.GoldCoin);
							Main.dust[dust1].scale = 1.5f;
							Main.dust[dust1].velocity *= 1.5f;
							int dust2 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.GoldCoin);
							Main.dust[dust2].scale = 1.5f;
						}
						npc.position = targPos;
						for (int i = 0; i < 50; ++i) //Create dust after teleport
						{
							int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.GoldCoin);
							int dust1 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.GoldCoin);
							Main.dust[dust1].scale = 1.5f;
							Main.dust[dust1].velocity *= 1.5f;
							int dust2 = Dust.NewDust(npc.position, npc.width, npc.height, DustID.GoldCoin);
							Main.dust[dust2].scale = 1.5f;
						}
					}
					SetMove(Move.Hover, 240);
				}
			}
			if (move == Move.Shoot)
			{
				npc.velocity = Vector2.Zero;
				counter--;
				for (int i = 0; i < 360; i+= 5)
				{
					int dust = Dust.NewDust(new Vector2(npc.Center.X , npc.Center.Y) + new Vector2(0, -100).RotatedBy(MathHelper.ToRadians(i)), npc.width / 8, npc.height / 8, 58, 0f, 0f, 0, Color.White, 1.5f);
					Main.dust[dust].velocity = Vector2.Zero;
				}
				if (counter % 20 == 0)
				{
					Vector2 placePosition = npc.Center + new Vector2(0, -100).RotatedByRandom(MathHelper.ToRadians(360));
					Vector2 direction = (Main.player[npc.target].Center - placePosition);
					direction.Normalize();
					Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * 7f, direction.Y * 7f, mod.ProjectileType("FearBolt"), 30, 1, Main.myPlayer, 0, 0);	
				}
				if (counter <= 0)
				{
					SetMove(Move.Hover, 240);
				}
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

using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;
using static Terraria.ModLoader.ModContent;

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

        private Move move { get { return (Move)NPC.ai[0]; } set { NPC.ai[0] = (int)value; } }
        private Move prevMove;
		
		private int counter;

		private Vector2 targetPosition;
		
		private int laserSweep = 60;
		private int animationFps = 60 / 10; //first value is tickrate, second is the fps that you want

        private bool phaseStart;
        private bool phaseEnd;

		private int numMasks = 1;
		private int masksToSpawn = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Inquisitor");
			Main.npcFrameCount[NPC.type] = 6;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 10000;
            NPC.damage = 100;
            NPC.defense = 40;
            NPC.knockBackResist = 0f;
            NPC.width = 94;
            NPC.height = 100;
			NPC.alpha = 0;
            NPC.value = Item.buyPrice(0, 20, 0, 0);
            NPC.npcSlots = 1f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
			NPC.ai[3] = 0; //phase: 0 is creation, 1 is first, 2 is second
			NPC.frameCounter = 0;
			
            NPC.netAlways = true;

        }

        public override void FindFrame(int frameHeight)
        {
			NPC.frameCounter += 0.2f;
			NPC.frameCounter %= 6; 
			int frame = (int)NPC.frameCounter; 
			NPC.frame.Y = frame * frameHeight; 
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 16000 * (Math.Min((numPlayers - 1) * 2, 1));
            NPC.damage = 175;
			numMasks *= numPlayers;
			masksToSpawn = numMasks;
        }
		
        public override void AI()
        {
            Player player = Main.player[NPC.target];
			NPC.TargetClosest(true);
            if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				NPC.velocity.X = -15;
				NPC.timeLeft = 10;
			}
			
			if (masksToSpawn > 0)
			{
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<AgonyMask>(), ai0: NPC.whoAmI, ai1: masksToSpawn);
				masksToSpawn--;
			}
            if (NPC.ai[3] == 0 && IsInPhaseOne())
            {
				SetMove(Move.Hover, 240);
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 20, (int)NPC.Center.Y, NPCType<EocPuppet>(), ai0: player.whoAmI, ai1: NPC.whoAmI);
                /*NPC.NewNPC((int)NPC.Center.X + 20, (int)NPC.Center.Y, NPCType<<AgonyMask>(), ai0: player.whoAmI, ai1: NPC.whoAmI);*/
                NPC.ai[3] = 1;
				masksToSpawn = numMasks;
            } else if (NPC.ai[3] == 1 && IsInPhaseTwo())
			{
				SetMove(Move.Shoot, 480);
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 20, (int)NPC.Center.Y, NPCType<EocPuppet>(), ai0: player.whoAmI, ai1: NPC.whoAmI);
				NPC.ai[3] = 2;
				numMasks *= 3;
				masksToSpawn = numMasks;
			}
			if (move == Move.Hover)
			{
				counter--;
				NPC.velocity.X = 0;
				NPC.velocity.Y = 0.5f * (float)Math.Cos(MathHelper.ToRadians(counter * 2));
				if (counter <= 0)
				{
					if (Main.rand.NextBool(2))
						SetMove(Move.Teleport, 0);
					else
						SetMove(Move.Shoot, 120);
				}
			} else if (move == Move.Teleport)
			{
				bool success = false;
				while (!success)
				{
					Vector2 targPos = player.Center + new Vector2(200, 0).RotatedByRandom(MathHelper.ToRadians(180));
					if (targPos.Y < player.Center.Y)
					{
						success = true;
						for (int i = 0; i < 50; ++i) //Create dust b4 teleport
						{
							int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldCoin);
							int dust1 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldCoin);
							Main.dust[dust1].scale = 1.5f;
							Main.dust[dust1].velocity *= 1.5f;
							int dust2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldCoin);
							Main.dust[dust2].scale = 1.5f;
						}
						NPC.position = targPos;
						for (int i = 0; i < 50; ++i) //Create dust after teleport
						{
							int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldCoin);
							int dust1 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldCoin);
							Main.dust[dust1].scale = 1.5f;
							Main.dust[dust1].velocity *= 1.5f;
							int dust2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldCoin);
							Main.dust[dust2].scale = 1.5f;
						}
					}
					SetMove(Move.Hover, 120);
				}
			} else if (move == Move.Shoot)
			{
				NPC.velocity = Vector2.Zero;
				counter--;
				for (float i = 0; i < 360; i+= 7.5f)
				{
					int dust = Dust.NewDust(new Vector2(NPC.Center.X , NPC.Center.Y) + new Vector2(0, -100).RotatedBy(MathHelper.ToRadians(i)), NPC.width / 8, NPC.height / 8, 58, 0f, 0f, 0, Color.White, 1.5f);
					Main.dust[dust].velocity = Vector2.Zero;
				}
				if (counter % 10 == 0)
				{
					Vector2 placePosition = NPC.Center + new Vector2(0, -100).RotatedByRandom(MathHelper.ToRadians(360));
					Vector2 direction = (Main.player[NPC.target].Center - placePosition);
					direction.Normalize();
					Projectile.NewProjectile(NPC.GetSource_FromAI(), placePosition.X, placePosition.Y, direction.X * 7f, direction.Y * 7f, ModContent.ProjectileType<FearBolt>(), 30, 1, Main.myPlayer, 0, 0);	
				}
				if (counter <= 0)
				{
					SetMove(Move.Hover, 120);
				}
			}
        }
        private void SetMove(Move move, int counter)
        {
            this.prevMove = this.move;
            this.move = move;
			this.counter = counter;
        }

		private bool IsInPhaseOne()
		{
			return NPC.life <= NPC.lifeMax * .9;    //90% hp
		}

		private bool IsInPhaseTwo()
        {
            return NPC.life <= NPC.lifeMax * .5;    //50% hp
        }
    }
}

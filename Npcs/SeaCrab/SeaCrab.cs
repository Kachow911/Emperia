using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.SeaCrab
{
    public class SeaCrab: ModNPC
    {
		
		private enum Move
        {
		   Walk,
		   ShellSpinAnim, 
		   ShellSpin,
		   Aggro
        }
		private int counter = 0;

		private Move move;
        private Move prevMove;
        private Vector2 targetPosition;
		private bool init;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seashell Hermit");
			Main.npcFrameCount[NPC.type] = 7;
		}
        public override void SetDefaults()
        {
            NPC.lifeMax = 720;
            NPC.damage = 15;
            NPC.defense = 3;
            NPC.knockBackResist = 0.6f;
            NPC.width = 56;
            NPC.height = 62;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.npcSlots = 1f;
            NPC.boss = false;
            NPC.lavaImmune = false;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
			NPC.scale = 1f;
			NPC.friendly = true;
			NPC.aiStyle = 7;
        }
		public override void FindFrame(int frameHeight)
		{	
			if (move == Move.Walk)
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 4;
				int frame = (int)NPC.frameCounter;
				NPC.frame.Y = frame * frameHeight;
			}
			else if (move == Move.Walk)
            {
				NPC.frameCounter += 0.07f;
				NPC.frameCounter %= 3;
				int frame = (int)NPC.frameCounter + 4;
				NPC.frame.Y = frame * frameHeight;
			}
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 1000;
            NPC.damage = 20;
        }

        public override void AI()
		{
			Player player = Main.player[NPC.target];
			if (!init)
			{
				SetMove(Move.Walk, Main.rand.Next(300, 400));
				init = true;
			}
			if (move == Move.Walk)
			{
				Main.NewText("Big");
				counter--;
				NPC.aiStyle = 3;
				AIType = 508;
				if (NPC.velocity.X > 0)
				{
					NPC.spriteDirection = 1;
				}
				else if (NPC.velocity.X < 0)
				{
					NPC.spriteDirection = -1;
				}
				if (counter <= 0)
				{
					SetMove(Move.ShellSpinAnim, 45);
				}
			}
			else if (move == Move.ShellSpinAnim)
			{
				counter--;
				if (player.Center.X > NPC.Center.X)
				{
					NPC.spriteDirection = 1;
				}
				else
				{
					NPC.spriteDirection = -1;
				}
				NPC.velocity = Vector2.Zero;
				if (counter <= 0)
				{
					

				}
			}
		}
		
		private void SetMove(Move toMove, int counter)
        {
            prevMove = move;
            move = toMove;
            this.counter = counter;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.spawnTileX;
			int y = spawnInfo.spawnTileY;
			int tile = Main.tile[x, y].type;
			return 0f;
		}
		

       

       /* private void SmoothMoveToPosition(Vector2 toPosition, float addSpeed, float maxSpeed, float slowRange = 64, float slowBy = .95f)
        {
            if (Math.Abs((toPosition - NPC.Center).Length()) >= slowRange)
            {
                NPC.velocity += Vector2.Normalize((toPosition - NPC.Center) * addSpeed);
                NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -maxSpeed, maxSpeed);
                NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -maxSpeed, maxSpeed);
            }
            else
            {
                NPC.velocity *= slowBy;
            }
        }*/
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			
		}
        
    }
}

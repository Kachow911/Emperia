using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Items.Sets.PreHardmode.Seashell;
using Terraria.GameContent.ItemDropRules;


namespace Emperia.Npcs.SeaCrab
{
    public class SeaCrab: ModNPC
    {
		
		private enum Move
        {
		   Walk,
		   ShellSpinAnim, 
		   ShellSpin,
		   ShellSpinAnimB,
		   ShellSpinB,
		   ShellSpinOutAnim,
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
            NPC.lifeMax = 860;
            NPC.damage = 23;
            NPC.defense = 10;
            NPC.knockBackResist = 0.6f;
            NPC.width = 38;
            NPC.height = 38;
            NPC.value = 4000;
            NPC.npcSlots = 1f;
            NPC.boss = false;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1; //57 //20 //antlion, derpling
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
			NPC.scale = 1.2f;
			//NPC.friendly = true;
			//NPC.aiStyle = 3;
			NPC.knockBackResist = 0f;
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
			else if (move == Move.ShellSpinAnim && counter <= 8 || move == Move.ShellSpinOutAnim && counter <= 8 || move == Move.ShellSpinAnimB && counter <= 8)
			{
				int frame = 4;
				NPC.frame.Y = frame * frameHeight;
			}
			else if (move == Move.ShellSpinAnimB && counter <= 36 && counter > 8)
			{
				if (counter == 36) NPC.frameCounter = Math.Floor(NPC.frameCounter + 1);//+= 1f;
				NPC.frameCounter += 0.08f;
				NPC.frame.Y = (int)NPC.frameCounter % 4 * frameHeight;
			}
			else if (move == Move.ShellSpin || move == Move.ShellSpinB || move == Move.ShellSpinOutAnim)
			{
				NPC.frameCounter += 0.05f + 0.02f * Math.Abs(NPC.velocity.X);
				NPC.frameCounter %= 2;
				int frame = (int)NPC.frameCounter + 5;
				NPC.frame.Y = frame * frameHeight;
			}
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 1240;
            NPC.damage = 36;
        }
		Vector2 crabHitbox = Vector2.Zero;
		Vector2 shellHitbox = Vector2.Zero;

		float noVanillaAIJump;
		float oldVelocityX = 0;
		Vector2 oldPos;
        public override bool PreAI()
        {
			noVanillaAIJump = NPC.velocity.Y;
			return true;
        }
        public override void AI()
		{
			NPC.aiStyle = 3;
			Player player = Main.player[NPC.target];
			NPC.velocity.Y = noVanillaAIJump;
			NPC.velocity.X = oldVelocityX;
			if (!init)
            {
                crabHitbox = new Vector2(NPC.width, NPC.height);
                shellHitbox = new Vector2(NPC.width * 0.7f, NPC.height * 0.85f);
				SetMove(Move.Walk, Main.rand.Next(300, 400));
				init = true;
			}
			if (player.Center.X > NPC.Center.X)
			{
				NPC.direction = 1;
			}
			else
			{
				NPC.direction = -1;
			}
			if (Math.Abs(NPC.velocity.X) < 3.3f)
			{
				NPC.velocity.X += 0.1f * NPC.direction;
				if (Math.Abs(NPC.velocity.X) > 3.3f)
                {
					NPC.velocity.X = 3.3f * NPC.direction;
				}
			}
			if (NPC.velocity.X * NPC.direction < 0) NPC.velocity.X *= 0.98f; //runs if velocity.x and npc.direction are different directions. yes this could just be two if statements. math!
			//Main.NewText(NPC.velocity.X.ToString(), 0);
			if (move == Move.Walk) 
			{
				counter--;
				//if (Math.Abs(player.Center.X - NPC.Center.X) > 112) counter--; if ranged has too easy of a time
				if (NPC.velocity.Y == 0 && player.Bottom.Y + 32 < NPC.Bottom.Y && Math.Abs(player.Center.X - NPC.Center.X) < 112 || oldPos == NPC.position)
                {
					NPC.velocity.Y = -9f;
                }
				if (counter <= 0 && NPC.velocity.Y == 0)
				{
					if (Main.rand.NextBool(3)) SetMove(Move.ShellSpinAnimB, 80);
					else SetMove(Move.ShellSpinAnim, 24);
				}
			}
			else if (move == Move.ShellSpinAnim)
			{
				counter--;
				NPC.velocity.X *= 0.95f;
				if (counter <= 0)
				{
					SetMove(Move.ShellSpin, 600);
					NPC.width = (int)shellHitbox.X;
					NPC.height = (int)shellHitbox.Y;
				}
			}
			else if (move == Move.ShellSpin)
			{
				counter--;

				if (Math.Abs(NPC.velocity.X) < 7) NPC.velocity.X += 0.08f * NPC.direction;
				
				if (counter <= 0)
				{
					SetMove(Move.ShellSpinOutAnim, 30);
				}
			}
			else if (move == Move.ShellSpinAnimB)
			{
				counter--;
				if (counter > 36) NPC.velocity.X *= 0.95f;
				if (counter == 36)
				{
					NPC.velocity.Y = -5f;
					NPC.velocity.X = 2.8f * NPC.direction;
				}
				//Main.NewText(counter.ToString());
				if (counter <= 0)
				{
					SetMove(Move.ShellSpinB, 600);
					NPC.width = (int)shellHitbox.X;
					NPC.height = (int)shellHitbox.Y;
				}
			}
			else if (move == Move.ShellSpinB)
			{
				counter--;

				if (Math.Abs(NPC.velocity.X) < 7) NPC.velocity.X += 0.06f * NPC.direction;
				if (NPC.velocity.Y == 0 && Math.Abs(player.Center.X - NPC.Center.X) < 64)
				{
					NPC.velocity.Y = -8f;
				}

				if (counter <= 0)
				{
					SetMove(Move.ShellSpinOutAnim, 30);
				}
			}
			else if (move == Move.ShellSpinOutAnim)
			{
				counter--;
				NPC.velocity.X *= 0.95f;

				if (counter <= 0)
				{
					NPC.width = (int)crabHitbox.X;
					NPC.height = (int)crabHitbox.Y;
					SetMove(Move.Walk, Main.rand.Next(300, 400));
				}
			}

			//Main.NewText(NPC.velocity.X.ToString(), 255, 0);
			oldVelocityX = NPC.velocity.X;
			oldPos = NPC.position;
			NPC.spriteDirection = NPC.direction * -1;
		}

		private void SetMove(Move toMove, int counter)
        {
            prevMove = move;
            move = toMove;
            this.counter = counter;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.SpawnTileX;
			int y = spawnInfo.SpawnTileY;
			int tile = Main.tile[x, y].TileType;
			return 0f;
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			if (move == Move.ShellSpin) counter = 0;
				
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
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SeaCrystal>()));
		}
	}
}

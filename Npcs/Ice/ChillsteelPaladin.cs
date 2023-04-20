using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Ice;

namespace Emperia.Npcs.Ice
{
    public class ChillsteelPaladin : ModNPC
    {
		
		private enum Move
        {
           Walk, 
		   Swordcast
        }
		private int counter = 0;

		private Move move;
        private Move prevMove;
        private Vector2 targetPosition;
		private bool init;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chillsteel Paladin");
			Main.npcFrameCount[NPC.type] = 8;
		}
        public override void SetDefaults()
        {
            NPC.lifeMax = 180;
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
			NPC.aiStyle = 3;
			AIType = 508;
        }
		public override void FindFrame(int frameHeight)
		{	
			if (move == Move.Walk)
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 7; 
				int frame = (int)NPC.frameCounter; 
				NPC.frame.Y = frame * frameHeight; 
			}
			else if (move == Move.Swordcast)
			{
				int frame = 8; 
				NPC.frame.Y = frame * frameHeight; 
			}
		}

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = 550;
            NPC.damage = 20;
        }

        public override void AI()
		{
			Player player = Main.player[NPC.target];
			if (!init)
			{
				move = Move.Walk;
				counter = 300;
				init = true;
			}
			if (move == Move.Walk)
			{
				//Main.NewText("Big");
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
					SetMove(Move.Swordcast, 45);
				}
			}
			else if (move == Move.Swordcast)
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
					int type = ModContent.ProjectileType<ChillSword>();
					SetMove(Move.Walk, 300);
					for (int i = 0; i < 3; i++)
					{
						Vector2 placePosition = NPC.Center + new Vector2(Main.rand.Next(-100, 100), -NPC.height - Main.rand.Next(50));
						Vector2 direction = Main.player[NPC.target].Center - placePosition;
						direction.Normalize();
						Projectile.NewProjectile(NPC.GetSource_FromAI(), placePosition.X, placePosition.Y, direction.X * 12f, direction.Y * 12f, type, 15, 1, Main.myPlayer, 0, 0);
					}
					SetMove(Move.Walk, 300);

				}
			}
		}
		
		private void SetMove(Move toMove, int counter)
        {
            prevMove = move;
            move = toMove;
            this.counter = counter;
		}
		/*public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.SpawnTileX;
			int y = spawnInfo.SpawnTileY;
			int tile = Main.tile[x, y].TileType;
			return 0f;
		}*/
		

       

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

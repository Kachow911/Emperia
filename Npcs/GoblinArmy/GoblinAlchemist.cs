using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Flasks;

namespace Emperia.Npcs.GoblinArmy
{
    public class GoblinAlchemist : ModNPC
    {
		
		private enum Move
        {
           Walk, 
		   Throw
        }
		private int counter { get { return (int)NPC.ai[0]; } set { NPC.ai[0] = value; } }

        private Move move { get { return (Move)NPC.ai[1]; } set { NPC.ai[1] = (int)value; } }
        private Move prevMove;
        private Vector2 targetPosition;
		private bool init;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Alchemist");
			Main.npcFrameCount[NPC.type] = 11;
		}
        public override void SetDefaults()
        {
            NPC.lifeMax = 1000;
            NPC.damage = 10;
            NPC.defense = 3;
            NPC.knockBackResist = 0.6f;
            NPC.width = 96;
            NPC.height = 56;
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
				NPC.frameCounter %= 8; 
				int frame = (int)NPC.frameCounter; 
				NPC.frame.Y = frame * frameHeight; 
			}
			else if (move == Move.Throw)
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 3; 
				int frame = (int)NPC.frameCounter + 8; 
				NPC.frame.Y = frame * frameHeight; 
			}
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 1250;
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
				counter--;
				NPC.aiStyle = 3;
				AIType = 508;
				if (NPC.velocity.X > 0)
					NPC.spriteDirection = 1;
				else if (NPC.velocity.X < 0)
					NPC.spriteDirection = -1;
				if (counter <= 0)
				{
					SetMove(Move.Throw, 15);
				}
			}
			else if (move == Move.Throw)
			{
				counter--;
				if (player.Center.X > NPC.Center.X)
					NPC.spriteDirection = 1;
				else
					NPC.spriteDirection = -1;
				NPC.velocity = Vector2.Zero;
				if (counter <= 0)
				{
					int x = Main.rand.Next(3);
					int type = 0;
					if (x == 0)
						type = ModContent.ProjectileType<GoblinFlask1>();
					else if (x == 1)
						type = ModContent.ProjectileType<GoblinFlask2>();
					else if (x == 2)
						type = ModContent.ProjectileType<GoblinFlask3>();
					SetMove(Move.Walk, 300);
					Vector2 placePosition = NPC.Center + new Vector2(0, -NPC.height / 2);
					Vector2 direction = Main.player[NPC.target].Center - placePosition;
					direction.Normalize();
					Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), NPC.Center.X, NPC.Center.Y - NPC.height/2, direction.X * 12f, direction.Y * 12f, type, 70, 1, Main.myPlayer, 0, 0);
					
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
			int tile = Main.tile[x, y].TileType;
			return (Main.hardMode && Main.invasionType == 1) ? 0.2f : 0;
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
			if (Main.rand.Next(10) == 0)
				{
					Item.NewItem(NPC.GetItemSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Weapons.GoblinArmy.AlchemistFlask>());
				}
		}
        
    }
}

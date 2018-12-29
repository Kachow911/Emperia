using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.GoblinArmy
{
    public class GoblinAlchemist : ModNPC
    {
		
		private enum Move
        {
           Walk, 
		   Throw
        }
		private int counter { get { return (int)npc.ai[0]; } set { npc.ai[0] = value; } }

        private Move move { get { return (Move)npc.ai[1]; } set { npc.ai[1] = (int)value; } }
        private Move prevMove;
        private Vector2 targetPosition;
		private bool init;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Alchemist");
			Main.npcFrameCount[npc.type] = 11;
		}
        public override void SetDefaults()
        {
            npc.lifeMax = 1000;
            npc.damage = 10;
            npc.defense = 3;
            npc.knockBackResist = 0.6f;
            npc.width = 96;
            npc.height = 56;
            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.npcSlots = 1f;
            npc.boss = false;
            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
			npc.scale = 1f;
			npc.aiStyle = 3;
			aiType = 508;
        }
		public override void FindFrame(int frameHeight)
		{	
			if (move == Move.Walk)
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 8; 
				int frame = (int)npc.frameCounter; 
				npc.frame.Y = frame * frameHeight; 
			}
			else if (move == Move.Throw)
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 3; 
				int frame = (int)npc.frameCounter + 8; 
				npc.frame.Y = frame * frameHeight; 
			}
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1250;
            npc.damage = 20;
        }

        public override void AI()
		{
			Player player = Main.player[npc.target];
			if (!init)
			{
				move = Move.Walk;
				counter = 300;
				init = true;
			}
			if (move == Move.Walk)
			{
				counter--;
				npc.aiStyle = 3;
				aiType = 508;
				if (npc.velocity.X > 0)
					npc.spriteDirection = 1;
				else if (npc.velocity.X < 0)
					npc.spriteDirection = -1;
				if (counter <= 0)
				{
					SetMove(Move.Throw, 15);
				}
			}
			else if (move == Move.Throw)
			{
				counter--;
				if (player.Center.X > npc.Center.X)
					npc.spriteDirection = 1;
				else
					npc.spriteDirection = -1;
				npc.velocity = Vector2.Zero;
				if (counter <= 0)
				{
					int x = Main.rand.Next(3);
					int type = 0;
					if (x == 0)
						type = mod.ProjectileType("GoblinFlask1");
					else if (x == 1)
						type = mod.ProjectileType("GoblinFlask2");
					else if (x == 2)
						type = mod.ProjectileType("GoblinFlask3");
					SetMove(Move.Walk, 300);
					Vector2 placePosition = npc.Center + new Vector2(0, -npc.height / 2);
					Vector2 direction = Main.player[npc.target].Center - placePosition;
					direction.Normalize();
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y - npc.height/2, direction.X * 12f, direction.Y * 12f, type, 70, 1, Main.myPlayer, 0, 0);
					
				}
			}
		}
		
		private void SetMove(Move toMove, int counter)
        {
            prevMove = move;
            move = toMove;
            this.counter = counter;
		}
		

       

       /* private void SmoothMoveToPosition(Vector2 toPosition, float addSpeed, float maxSpeed, float slowRange = 64, float slowBy = .95f)
        {
            if (Math.Abs((toPosition - npc.Center).Length()) >= slowRange)
            {
                npc.velocity += Vector2.Normalize((toPosition - npc.Center) * addSpeed);
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -maxSpeed, maxSpeed);
                npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -maxSpeed, maxSpeed);
            }
            else
            {
                npc.velocity *= slowBy;
            }
        }*/
		public override void NPCLoot()
		{
			
		}
        
    }
}

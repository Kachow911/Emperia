﻿using System;
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
			Main.npcFrameCount[npc.type] = 7;
		}
        public override void SetDefaults()
        {
            npc.lifeMax = 720;
            npc.damage = 15;
            npc.defense = 3;
            npc.knockBackResist = 0.6f;
            npc.width = 56;
            npc.height = 62;
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
			npc.friendly = true;
			npc.aiStyle = 7;
        }
		public override void FindFrame(int frameHeight)
		{	
			if (move == Move.Walk)
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 4;
				int frame = (int)npc.frameCounter;
				npc.frame.Y = frame * frameHeight;
			}
			else if (move == Move.Walk)
            {
				npc.frameCounter += 0.07f;
				npc.frameCounter %= 3;
				int frame = (int)npc.frameCounter + 4;
				npc.frame.Y = frame * frameHeight;
			}
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1000;
            npc.damage = 20;
        }

        public override void AI()
		{
			Player player = Main.player[npc.target];
			if (!init)
			{
				SetMove(Move.Walk, Main.rand.Next(300, 400));
				init = true;
			}
			if (move == Move.Walk)
			{
				Main.NewText("Big");
				counter--;
				npc.aiStyle = 3;
				aiType = 508;
				if (npc.velocity.X > 0)
				{
					npc.spriteDirection = 1;
				}
				else if (npc.velocity.X < 0)
				{
					npc.spriteDirection = -1;
				}
				if (counter <= 0)
				{
					SetMove(Move.ShellSpinAnim, 45);
				}
			}
			else if (move == Move.ShellSpinAnim)
			{
				counter--;
				if (player.Center.X > npc.Center.X)
				{
					npc.spriteDirection = 1;
				}
				else
				{
					npc.spriteDirection = -1;
				}
				npc.velocity = Vector2.Zero;
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

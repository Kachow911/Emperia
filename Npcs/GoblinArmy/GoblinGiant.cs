using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.GoblinArmy
{
    public class GoblinGiant : ModNPC
    {
        private enum Move
        {
           Walk, 
		   Shoot
        }

        private int counter { get { return (int)npc.ai[0]; } set { npc.ai[0] = value; } }

        private Move move { get { return (Move)npc.ai[1]; } set { npc.ai[1] = (int)value; } }
        private Move prevMove;
        private Vector2 targetPosition;
		
		private bool init = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Giant");
			Main.npcFrameCount[npc.type] = 11;
		}
        public override void SetDefaults()
        {
            npc.lifeMax = 2000;
            npc.damage = 30;
            npc.defense = 12;
            npc.knockBackResist = 0f;
            npc.width = 128;
            npc.height = 132;
            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = false;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
			npc.scale = 1f;
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
			else if (move == Move.Shoot)
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 3; 
				int frame = (int)npc.frameCounter + 8; 
				npc.frame.Y = frame * frameHeight; 
			}
			
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2250;
            npc.damage = 55;
        }

        public override void AI()
		{
			
			if (npc.velocity.X < 0)
				npc.spriteDirection = -1;
			else if (npc.velocity.X > 0)
				npc.spriteDirection = 1;
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			if (!player.active || player.dead)
			{
				move = Move.Walk;
				npc.TargetClosest(false);
				npc.velocity.Y = 25;
				npc.noTileCollide = true;
				if (npc.timeLeft > 10)
				{
					npc.timeLeft = 10;
				}
			}
			
			if (!init)
			{
				move = Move.Walk;
				counter = 600;
				init = true;
			}
			if (move == Move.Walk)
            { 
				counter--;
				npc.aiStyle = 3;
				aiType = 508;
				if (npc.velocity.X > 2f)
					npc.velocity.X = 2f;
				if (npc.velocity.X < -2f)
					npc.velocity.X = -2f;
				if (counter <= 0)
				{
					SetMove(Move.Shoot, 15);
				}
			}
			if (move == Move.Shoot)
			{
				counter--;
				if (player.Center.X > npc.Center.X)
					npc.spriteDirection = 1;
				else
					npc.spriteDirection = -1;
				npc.velocity = Vector2.Zero;
				if (counter <= 0)
				{
					
					SetMove(Move.Walk, 600);
					Vector2 placePosition = npc.Center + new Vector2(0, -npc.height / 2);
					Vector2 direction = Main.player[npc.target].Center + new Vector2(0, -125) - placePosition;
					direction.Normalize();
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y - npc.height/2, direction.X * 7f, direction.Y * 7f, mod.ProjectileType("GoblinBomb"), npc.damage, 1, Main.myPlayer, 0, 0);
					
				}
			}
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

        private bool IsBelowPhaseTwoThreshhold()
        {
            return npc.life <= npc.lifeMax / 2;     
        }

        private void SetMove(Move toMove, int counter)
        {
            prevMove = move;
            move = toMove;
            this.counter = counter;
		}
        /*public override void NPCLoot()
		{
			Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Yeti/gore1"), 1f);
			Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Yeti/gore2"), 1f);
			Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Yeti/gore3"), 1f);
			Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Yeti/gore4"), 1f);
			Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Yeti/gore5"), 1f);
			/*if (!EmperialWorld.downedMushor)
			{
            	Main.NewText("The guardian of the mushroom biome has fallen...", 0, 75, 161, false);
				EmperialWorld.downedMushor = true;
			}
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("YetiTrophy"));
			}
			if (Main.expertMode)
			{
				npc.DropBossBags();
			}
			else
			{
				
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MammothineClub"));
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HuntersSpear"));
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BigGameHunter"));
				}
				
				if (Main.rand.Next(7) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("YetiMask"));
				}
				if (Main.rand.Next(10) == 0)
				{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ChilledFootprint"));
				}
				if (Main.rand.Next(2) == 0)
				{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ArcticIncantation"));
				}
			}
		}*/

    }
}

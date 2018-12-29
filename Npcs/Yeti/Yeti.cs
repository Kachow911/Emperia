using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Yeti
{
    public class Yeti : ModNPC
    {
        private enum Move
        {
           Walk, 
		   JumpStart,
		   Jump,
		   Snowball
        }

        private int counter { get { return (int)npc.ai[0]; } set { npc.ai[0] = value; } }

        private Move move { get { return (Move)npc.ai[1]; } set { npc.ai[1] = (int)value; } }
        private Move prevMove;
        private Vector2 targetPosition;

        private int side { get { return (int)npc.ai[2]; } set { npc.ai[2] = value; } }
		
        private bool phase2Active;
		private bool init = false;
		private int walkTimer = 1;
		private int jumpDir = 1;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Yeti");
			Main.npcFrameCount[npc.type] = 18;
		}
        public override void SetDefaults()
        {
            npc.lifeMax = 1500;
            npc.damage = 50;
            npc.defense = 12;
            npc.knockBackResist = 0f;
            npc.width = 55;
            npc.height = 62;
            npc.value = Item.buyPrice(0, 8, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            npc.netAlways = true;
			npc.scale = 2f;
			bossBag = mod.ItemType("YetiBag");
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
			else if (move == Move.JumpStart && counter >= 70)
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 4; 
				int frame = (int)npc.frameCounter + 8; 
				npc.frame.Y = frame * frameHeight; 
			}
			else if (move == Move.Jump)
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 2; 
				int frame = (int)npc.frameCounter + 12; 
				npc.frame.Y = frame * frameHeight; 
			}
			else if (move == Move.Snowball && counter >= 5)
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 4; 
				int frame = (int)npc.frameCounter + 14; 
				npc.frame.Y = frame * frameHeight; 
			}
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1750;
            npc.damage = 60;
        }

        public override void AI()
		{
			if (IsBelowPhaseTwoThreshhold() && !phase2Active)
			{
				NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-200, 200), (int)npc.Center.Y - 150, mod.NPCType("Yetiling"));
				phase2Active = true;
			}
			if (npc.velocity.X < 0)
				npc.spriteDirection = 1;
			else if (npc.velocity.X > 0)
				npc.spriteDirection = -1;
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
				walkTimer++;
				counter--;
				npc.aiStyle = 3;
				aiType = 508;
		
				if (!IsBelowPhaseTwoThreshhold() && counter % 400 == 0)
				{
					NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-200, 200), (int)npc.Center.Y - 150, mod.NPCType("Yetiling"));
				}
				if (npc.velocity.X > 2f)
					npc.velocity.X = 2f;
				if (npc.velocity.X < -2f)
					npc.velocity.X = -2f;
				if (Main.rand.Next(2) == 0 && counter <= 0 && Main.expertMode && walkTimer > 400)
				{
					SetMove(Move.Snowball, 25);
				}
				else if (counter <= 0 && walkTimer > 400)
				{
					SetMove(Move.JumpStart, 90);
				}
			}
			if (move == Move.JumpStart)
			{
				npc.velocity.X = 0;
				counter--;
				npc.aiStyle = -1;
				Color rgb = new Color(160, 243, 255);
				int index2 = Dust.NewDust(npc.position, npc.width, npc.height, 76, (float) npc.velocity.X, (float) npc.velocity.Y, 0, rgb, 0.9f);
				if (counter <= 0)
				{
				npc.noTileCollide = true;
					SetMove(Move.Jump, 1);
					if (player.Center.X > npc.Center.X)
					{
						npc.velocity.Y = -12;
						npc.velocity.X = 5;
						jumpDir = 1;
					}
					else
					{
						npc.velocity.Y = -12;
						npc.velocity.X = -5;
						jumpDir = -1;
					}
				}
			}
			if (move == Move.Jump)
			{
				counter ++;
				if (counter < 40)
				{
					if (jumpDir == 1)
						npc.velocity.X = Math.Abs(player.Center.X - npc.Center.X) / 20;
					else
						npc.velocity.X = -1 * Math.Abs(player.Center.X - npc.Center.X) / 20;
				}
				if (npc.velocity.Y > 0)
					npc.noTileCollide = false;
				if (npc.velocity.Y == 0)
				{
					Main.PlaySound(SoundID.Dig, npc.Center, 1);
					Main.PlaySound(SoundID.Item, npc.Center, 107);
					if (IsBelowPhaseTwoThreshhold())
					{
						walkTimer = 1;
						SetMove(Move.Walk, 500);
					}
					else
					{
						walkTimer = 1;
						SetMove(Move.Walk, 600);
					}
					Color rgb = new Color(160, 243, 255);
					for (int i = - 50; i < 50; i++)
					{
						int index2 = Dust.NewDust(npc.position + new Vector2(i, 0), npc.width, npc.height, 76, npc.velocity.X / 5, (float) npc.velocity.Y, 0, rgb, 0.9f);
					}
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y + npc.height / 2, 2, 0, mod.ProjectileType("YetiProjOne"), 0, 1, Main.myPlayer, 0, 0);
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y + npc.height / 2, -2, 0, mod.ProjectileType("YetiProjOne"), 0, 1, Main.myPlayer, 0, 0);
					//go back lool
				}
			}
			if (move == Move.Snowball)
			{
				counter--;
				if (player.Center.X > npc.Center.X)
					npc.spriteDirection = 1;
				else
					npc.spriteDirection = -1;
				npc.velocity = Vector2.Zero;
				if (counter <= 0)
				{
					walkTimer = 1;
					SetMove(Move.Walk, 200);
					Vector2 placePosition = npc.Center + new Vector2(0, -npc.height / 2);
					Vector2 direction = Main.player[npc.target].Center + new Vector2(0, -125) - placePosition;
					direction.Normalize();
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y - npc.height/2, direction.X * 7f, direction.Y * 7f, mod.ProjectileType("YetiSnowball"), npc.damage / 2, 1, Main.myPlayer, 0, 0);
					
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
		public override void NPCLoot()
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
			}*/
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
		}
        
    }
}

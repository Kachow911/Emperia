using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Mushor
{
	[AutoloadBossHead]
    public class Mushor : ModNPC
    {
        private enum Move
        {
           Chase,
		   Charge,
		   ThrowBombs,
		   SporeStorm,
		   SpawnMinions,
		   Shielding
        }

        private int counter { get { return (int)npc.ai[0]; } set { npc.ai[0] = value; } }

        private Move move { get { return (Move)npc.ai[1]; } set { npc.ai[1] = (int)value; } }
        private Move prevMove;
        private Vector2 targetPosition;

        private int side { get { return (int)npc.ai[2]; } set { npc.ai[2] = value; } }

        private bool phase2Active;
		private bool init = false;
		private int shieldCount = 1;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushor");
			Main.npcFrameCount[npc.type] = 13;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 7500;
            npc.damage = 50;
            npc.defense = 13;
            npc.knockBackResist = 0f;
            npc.width = 128;
            npc.height = 128;
            npc.value = Item.buyPrice(0, 8, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            Main.npcFrameCount[npc.type] = 13;
            npc.netAlways = true;
			bossBag = mod.ItemType("MushorBag");
        }
      
        public override void FindFrame(int frameHeight)
        {
            if (phase2Active)
            {
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 8;
				int frame = (int)npc.frameCounter + 5;
				npc.frame.Y = frame * frameHeight;
            }
			else
            {
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 5;
				int frame = (int)npc.frameCounter;
				npc.frame.Y = frame * frameHeight;
			}
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 9000;
            npc.damage = 60;
        }

        public override void AI()
		{
			if (npc.ai[3] > 0)
                npc.dontTakeDamage = true;
            else 
				npc.dontTakeDamage = false;
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			if (!init)
			{
				move = Move.Chase;
				counter = 300;
				init = true;
			}
			if (!player.active || player.dead)
			{
				npc.TargetClosest(false);
				npc.velocity.Y = -25;
				
				if (npc.timeLeft > 10)
					{
						npc.timeLeft = 10;
					}
			}
			

            if (IsBelowPhaseTwoThreshhold() && !phase2Active)
            {
				SetMove(Move.Shielding, 120);
				shieldCount = 0;
                phase2Active = true;
            }
			if (move == Move.Chase)
			{
				Vector2 targetPos = new Vector2(player.Center.X, player.Center.Y - 50);
				npc.velocity += Vector2.Normalize((targetPos - npc.Center) * .05f);
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -3f, 3f);
                npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -3f, 3f);
				counter--;
				
				if (counter <= 0)
				{
					if (Main.expertMode && npc.ai[3] <= 0 && Main.rand.Next(6) == 0)
					{
						SetMove(Move.Shielding, 120);
					}
					if (prevMove == Move.Shielding)
					{
						SetMove(Move.SporeStorm, 1);
					}
					if (prevMove == Move.ThrowBombs || prevMove == Move.Charge || prevMove == Move.SporeStorm)
					{
						SetMove(Move.SpawnMinions, 300);
					}
					else
					{
						
						if (npc.Distance(player.Center) > 300)
						{
							SetMove(Move.Charge, 30);
							Vector2 direction = Main.player[npc.target].Center - npc.Center;
							direction.Normalize();
							if (!phase2Active)
							{
								npc.velocity.X = direction.X *= 15f;
								npc.velocity.Y = direction.Y *= 15f;
							}
							else
							{
								npc.velocity.X = direction.X *= 20f;
								npc.velocity.Y = direction.Y *= 20f;
							}
							Main.PlaySound(SoundID.Roar, npc.Center, 0);    //for maximum noisyness
						}
						else
						{
							if (!phase2Active)
								SetMove(Move.ThrowBombs, 1);
							else	
								SetMove(Move.SporeStorm, 1);
							
						}
					}
				}
			}
			else if (move == Move.Charge)
			{
				npc.velocity *= .99f;
				counter--;
				if (counter <= 0)
				{
					SetMove(Move.Chase, 120);
				}
				
			}
			else if (move == Move.ThrowBombs)
			{
				counter--;
				
				for (int i = 0; i < 3; i++)
				{
					Vector2 direction = (Main.player[npc.target].Center - npc.Center).RotatedBy(MathHelper.ToRadians(-10 + 10 * i));
					direction.Normalize();
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X * 7f, direction.Y * 7f, mod.ProjectileType("ExplodeMushroom"), 30, 1, Main.myPlayer, 0, 0);	
				}
				
				if (counter <= 0)
				{
					SetMove(Move.Chase, 120);
				}
				
				
			}
			else if (move == Move.SpawnMinions)
			{
				
				npc.velocity = Vector2.Zero;
				if (Main.rand.Next(5) == 0)
				{
					int dust = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width / 32, npc.height / 32, 20, 0f, 0f, 0, new Color(39, 90, 219), 1.5f);
				}
				if (counter % 60 == 0)
				{
					if (Main.rand.NextBool(3))
						NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-200, 200), (int)npc.Center.Y + Main.rand.Next(-200, 200), mod.NPCType("MushorMinionShoot"));
					else
						NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-200, 200), (int)npc.Center.Y + Main.rand.Next(-200, 200), mod.NPCType("MushorMinionExplode"));
				}
				counter--;
				if (counter <= 0)
				{
					SetMove(Move.Chase, 180);
				}
				
				
			}
   
           else if (move == Move.Shielding)
            {
				
                if (counter % 20 == 0 && npc.ai[3] <= 6)
                {
                    if (Main.netMode != 1)
                    {
					int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("MushorMinionShield"), ai0: npc.whoAmI, ai1: shieldCount);
						shieldCount++;
                        npc.ai[3]++;
						Main.npc[n].ai[3] = counter;
                    }
                }
                counter--;
                npc.velocity = Vector2.Zero;
                
                if (counter <= 0)
                {
                    SetMove(Move.Chase, 180);
                }
            }
			else if (move == Move.SporeStorm)
			{
				counter--;
				if (Main.rand.NextBool(2))
				{
					for (int i = 0; i < 10; i++)
					{
						Projectile.NewProjectile(player.Center.X - 300, player.Center.Y - 500 + 100 * i, 5f, 0, mod.ProjectileType("BigShroom2"), 30, 1, Main.myPlayer, 0, 0);	
					}
				}
				else
				{
					for (int i = 0; i < 10; i++)
					{
						Projectile.NewProjectile(player.Center.X + 300, player.Center.Y - 500 + 100 * i, -5f, 0, mod.ProjectileType("BigShroom2"), 30, 1, Main.myPlayer, 0, 0);	
					}
				}
				SetMove(Move.Chase, 80);
			}
        }
		

        public override void PostDraw(SpriteBatch batch, Color drawColor)
        {
            if (phase2Active)
                batch.Draw(mod.GetTexture("Npcs/Mushor/MushorGlow"), new Rectangle(npc.Hitbox.X - (int)Main.screenPosition.X, (npc.Hitbox.Y - (int)Main.screenPosition.Y) + 8, npc.width, npc.height), Color.White);    //GLOWY STUFF
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
			/*if (!EmperialWorld.downedMushor)
			{
            	Main.NewText("The guardian of the mushroom biome has fallen...", 0, 75, 161, false);
				EmperialWorld.downedMushor = true;
			}*/
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MushorTrophy"));
			}
			if (Main.expertMode)
			{
				npc.DropBossBags();
			}
			else
			{
				
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Shroomer"));
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Shroomerang"));
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Shroomflask"));
				}
				
				if (Main.rand.Next(7) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MushorMask"));
				}
				if (Main.rand.Next(2) == 0)
				{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Mushdisc"));
				}
			}
		}
    }
}

using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Mushroom;
using Emperia.Items.Weapons.Mushor;
using static Terraria.ModLoader.ModContent;

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

        private int counter { get { return (int)NPC.ai[0]; } set { NPC.ai[0] = value; } }

        private Move move { get { return (Move)NPC.ai[1]; } set { NPC.ai[1] = (int)value; } }
        private Move prevMove;
        private Vector2 targetPosition;

        private int side { get { return (int)NPC.ai[2]; } set { NPC.ai[2] = value; } }

        private bool phase2Active;
		private bool init = false;
		private int shieldCount = 1;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mushor");
			Main.npcFrameCount[NPC.type] = 13;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 7500;
            NPC.damage = 50;
            NPC.defense = 13;
            NPC.knockBackResist = 0f;
            NPC.width = 128;
            NPC.height = 128;
            NPC.value = Item.buyPrice(0, 8, 0, 0);
            NPC.npcSlots = 1f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            Main.npcFrameCount[NPC.type] = 13;
            NPC.netAlways = true;
			BossBag = ModContent.ItemType<Items.MushorBag>();
        }
      
        public override void FindFrame(int frameHeight)
        {
            if (phase2Active)
            {
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 8;
				int frame = (int)NPC.frameCounter + 5;
				NPC.frame.Y = frame * frameHeight;
            }
			else
            {
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 5;
				int frame = (int)NPC.frameCounter;
				NPC.frame.Y = frame * frameHeight;
			}
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 9000;
            NPC.damage = 60;
        }

        public override void AI()
		{
			if (NPC.ai[3] > 0)
                NPC.dontTakeDamage = true;
            else 
				NPC.dontTakeDamage = false;
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			if (!init)
			{
				move = Move.Chase;
				counter = 300;
				init = true;
			}
			if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				NPC.velocity.Y = -25;
				
				if (NPC.timeLeft > 10)
					{
						NPC.timeLeft = 10;
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
				NPC.velocity += Vector2.Normalize((targetPos - NPC.Center) * .05f);
                NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -3f, 3f);
                NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -3f, 3f);
				counter--;
				
				if (counter <= 0)
				{
					if (Main.expertMode && NPC.ai[3] <= 0 && Main.rand.Next(6) == 0)
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
						
						if (NPC.Distance(player.Center) > 300)
						{
							SetMove(Move.Charge, 30);
							Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
							direction.Normalize();
							if (!phase2Active)
							{
								NPC.velocity.X = direction.X *= 15f;
								NPC.velocity.Y = direction.Y *= 15f;
							}
							else
							{
								NPC.velocity.X = direction.X *= 20f;
								NPC.velocity.Y = direction.Y *= 20f;
							}
							Terraria.Audio.SoundEngine.PlaySound(SoundID.Roar, NPC.Center, 0);    //for maximum noisyness
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
				NPC.velocity *= .99f;
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
					Vector2 direction = (Main.player[NPC.target].Center - NPC.Center).RotatedBy(MathHelper.ToRadians(-10 + 10 * i));
					direction.Normalize();
					Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), NPC.Center.X, NPC.Center.Y, direction.X * 7f, direction.Y * 7f, ModContent.ProjectileType<ExplodeMushroom>(), 30, 1, Main.myPlayer, 0, 0);	
				}
				
				if (counter <= 0)
				{
					SetMove(Move.Chase, 120);
				}
				
				
			}
			else if (move == Move.SpawnMinions)
			{
				
				NPC.velocity = Vector2.Zero;
				if (Main.rand.Next(5) == 0)
				{
					int dust = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width / 32, NPC.height / 32, 20, 0f, 0f, 0, new Color(39, 90, 219), 1.5f);
				}
				if (counter % 60 == 0)
				{
					if (Main.rand.NextBool(3))
						NPC.NewNPC((int)NPC.Center.X + Main.rand.Next(-200, 200), (int)NPC.Center.Y + Main.rand.Next(-200, 200), NPCType<MushorMinionShoot>());
					else																												 
						NPC.NewNPC((int)NPC.Center.X + Main.rand.Next(-200, 200), (int)NPC.Center.Y + Main.rand.Next(-200, 200), NPCType<MushorMinionExplode>());
				}
				counter--;
				if (counter <= 0)
				{
					SetMove(Move.Chase, 180);
				}
				
				
			}
   
           else if (move == Move.Shielding)
            {
				
                if (counter % 20 == 0 && NPC.ai[3] <= 6)
                {
                    if (Main.netMode != 1)
                    {
					int n = NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, NPCType<MushorMinionShield>(), ai0: NPC.whoAmI, ai1: shieldCount);
						shieldCount++;
                        NPC.ai[3]++;
						Main.npc[n].ai[3] = counter;
                    }
                }
                counter--;
                NPC.velocity = Vector2.Zero;
                
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
						Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), player.Center.X - 300, player.Center.Y - 500 + 100 * i, 5f, 0, ModContent.ProjectileType<BigShroom2>(), 30, 1, Main.myPlayer, 0, 0);	
					}
				}
				else
				{
					for (int i = 0; i < 10; i++)
					{
						Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), player.Center.X + 300, player.Center.Y - 500 + 100 * i, -5f, 0, ModContent.ProjectileType<BigShroom2>(), 30, 1, Main.myPlayer, 0, 0);	
					}
				}
				SetMove(Move.Chase, 80);
			}
        }
		

        public override void PostDraw(SpriteBatch batch, Vector2 Vector2, Color drawColor)
        {
            if (phase2Active)
                batch.Draw(Mod.Assets.Request<Texture2D>("Npcs/Mushor/MushorGlow").Value, new Rectangle(NPC.Hitbox.X - (int)Main.screenPosition.X, (NPC.Hitbox.Y - (int)Main.screenPosition.Y) + 8, NPC.width, NPC.height), Color.White);    //GLOWY STUFF
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

        private bool IsBelowPhaseTwoThreshhold()
        {
            return NPC.life <= NPC.lifeMax / 2;     
        }

        private void SetMove(Move toMove, int counter)
        {
            prevMove = move;
            move = toMove;
            this.counter = counter;
        }
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			/*if (!EmperialWorld.downedMushor)
			{
            	Main.NewText("The guardian of the mushroom biome has fallen...", 0, 75, 161, false);
				EmperialWorld.downedMushor = true;
			}*/
			//if (Main.rand.Next(10) == 0)
			//{
			//	Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<MushorTrophy>());
			//}
			if (Main.expertMode)
			{
				NPC.DropBossBags();
			}
			else
			{
				
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Shroomer>());
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Weapons.Mushor.Shroomerang>());
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Shroomflask>());
				}
				
				//if (Main.rand.Next(7) == 0)
				//{
				//	Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<MushorMask>());
				//}
				if (Main.rand.Next(2) == 0)
				{
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Mushdisc>());
				}
			}
		}
    }
}

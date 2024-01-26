using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Items.Sets.PreHardmode.Seashell;
using Terraria.GameContent.ItemDropRules;
using static Terraria.Audio.SoundEngine;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using System.Collections.Generic;



namespace Emperia.Npcs.SeaCrab
{
    public class SeaCrab: ModNPC
    {
		
		private enum Move
        {
		   Emerge,
		   Walk,
		   ShellSpinAnim, 
		   ShellSpin,
		   ShellSpinAnimB,
		   ShellSpinB,
		   ShellSpinOutAnim,
		   BurrowAnim,
		   Burrow,
		   Aggro
        }
		private int counter = 0;

		private Move move;
        private Move prevMove;
        private Vector2 targetPosition;
		private bool init;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crystacean");
			Main.npcFrameCount[NPC.type] = 7;
			NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true; //frostburn arrows just make this boss comparatively easy lol
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
			//NPCID.Sets.ShouldBeCountedAsBoss[Type] = true;
			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Velocity = -1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
				Direction = -1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
			};
		}
		public override void SetDefaults()
        {
            NPC.lifeMax = 1340;
            NPC.damage = 20;
            NPC.defense = 18;
			NPC.knockBackResist = 0f;
			NPC.width = 36;
            NPC.height = 38;
            NPC.value = 4000;
            NPC.npcSlots = 5f;
            NPC.boss = false;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit22; //antlion 31, derpling 22, giant shelly 38
            NPC.DeathSound = SoundID.NPCDeath25;
            NPC.netAlways = true;
			NPC.scale = 1.2f;
			NPC.despawnEncouraged = false;
			DrawOffsetY = 0.5f;
        }
		public override void FindFrame(int frameHeight)
		{	
			if (move == Move.Walk || NPC.IsABestiaryIconDummy)
			{
				NPC.frameCounter += 0.175f;
				NPC.frameCounter %= 4;
				int frame = (int)NPC.frameCounter;
				NPC.frame.Y = frame * frameHeight;
			}
			else if (move == Move.ShellSpinAnim && counter <= 8 || move == Move.ShellSpinAnimB && counter <= 8 || move == Move.ShellSpinOutAnim && counter <= 12)
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
			else if (move == Move.ShellSpin || move == Move.ShellSpinB)
			{
				NPC.frameCounter += 0.05f + 0.02f * Math.Abs(NPC.velocity.X);
				NPC.frameCounter %= 2;
				int frame = (int)NPC.frameCounter + 5;
				NPC.frame.Y = frame * frameHeight;
			}
			else if (move == Move.ShellSpinOutAnim)
			{
				NPC.frameCounter += 0.05f * (0.01f * (40 + counter));
				NPC.frameCounter %= 2;
				int frame = (int)NPC.frameCounter + 5;
				NPC.frame.Y = frame * frameHeight;
			}
			else if (move == Move.Burrow || move == Move.Emerge)
			{
				NPC.frameCounter += 0.15f;
				NPC.frameCounter %= 2;
				int frame = (int)NPC.frameCounter + 5;
				NPC.frame.Y = frame * frameHeight;
			}
		}

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = 1840;
            NPC.damage = 23;
			if (Main.masterMode)
            {
				NPC.lifeMax = 2240;
				NPC.damage = 30;
			}
			damageScale = NPC.damage;
			if (numPlayers < 10) NPC.lifeMax = (int)(NPC.lifeMax * (0.5f + (0.5f * numPlayers))); //this may be unnecessary considering sea crystal drop isn't client side. maybe it should be
			else NPC.lifeMax = (int)(NPC.lifeMax * (0.5f + (0.5f * 10)));
		}

		Vector2 crabHitbox = Vector2.Zero;
		Vector2 shellHitbox = Vector2.Zero;
		float heightDifference = 0;

		float noVanillaAIJump; //removes any Y velocity given by the vanilla aiStyle 3 attempting to jump
		float oldVelocityX = 0; //removes deceleration caused by vanilla AI
		Vector2 oldPos;

        float? preJumpHeight;
		float? jumpPeak;
        bool trapped = false;

		float speedScale = 1;
		float damageScale;

		int burrowStage = 0;
		int emergeDelay = 0;
		int? randomDelay = null;

        int spinChance = 80;
		int spinBChance;
		int burrowChance = 20;

		bool lockedDirection = false;

		bool inShell = false;

		bool notEmerging = false;
		public override bool PreAI()
        {
			noVanillaAIJump = NPC.velocity.Y;
			return true;
        }
        public override void AI()
		{
			Player player = Main.player[NPC.target];
			NPC.velocity.Y = noVanillaAIJump;
			NPC.velocity.X = oldVelocityX;
			NPC.DiscourageDespawn(200);
			if (!init)
            {
				NPC.aiStyle = 3;
				crabHitbox = new Vector2(NPC.width, NPC.height);
                shellHitbox = new Vector2(NPC.width * 0.7f, NPC.height * 0.95f); //previous height was 0.85
				heightDifference = crabHitbox.Y - shellHitbox.Y;
				PlaySound(SoundID.NPCHit23, NPC.Center);
				SetMove(Move.Emerge, 44);
				init = true;
			}
			if (Main.expertMode)
			{
				speedScale = 1 + (float)(NPC.lifeMax - NPC.life) / NPC.lifeMax / 4; //set to / 10 if too hard
				NPC.damage = (int)(damageScale * (1 + (float)(NPC.lifeMax - NPC.life) / NPC.lifeMax / 1.5f)); //set to / 2 if too hard
			}

			if (player.Center.X > NPC.Center.X && !lockedDirection)
			{
				NPC.direction = 1;
			}
			else if (!lockedDirection)
			{
				NPC.direction = -1;
			}
			if (move != Move.ShellSpinOutAnim && Math.Abs(NPC.velocity.X) < 3.1f * (1 + (speedScale - 1) / 2)) //so speedscale has a weaker effect on walking
			{
				NPC.velocity.X += (move != Move.Walk ? 0.1f : 0.08f) * NPC.direction;
				if (Math.Abs(NPC.velocity.X) > 3.1f * (1 + (speedScale - 1) / 2)) NPC.velocity.X = 3.1f * NPC.direction * (1 + (speedScale - 1) / 2);
			}
			if (NPC.velocity.X * NPC.direction < 0) NPC.velocity.X *=  0.98f; //runs if velocity.x and npc.direction are different directions. yes this could just be two if statements. math!

			//Main.NewText(NPC.velocity.X.ToString(), 0);
			switch (move)
            {
				case Move.Emerge: //rises up. only happens when spawning
				{
					counter--;
					NPC.width = (int)shellHitbox.X;
					NPC.height = (int)shellHitbox.Y;
					NPC.noTileCollide = true;
					NPC.behindTiles = true;
					NPC.noGravity = true;
					NPC.aiStyle = 0;
					if (NPC.velocity.Y > 0) NPC.velocity.Y = -0.03f;
					else NPC.velocity.Y -= 0.03f;
					NPC.velocity.X = 0;
					if (counter <= 0)
					{
						NPC.width = (int)crabHitbox.X;
						NPC.height = (int)crabHitbox.Y;
						NPC.position.Y -= heightDifference;
						NPC.position.X -= 7;
						NPC.noTileCollide = false;
						NPC.behindTiles = false;
						NPC.noGravity = false;
						NPC.aiStyle = 3;
						SetMove(Move.ShellSpinOutAnim, 50);
					}
					break;
				}
				case Move.Walk:
				{
					counter--;
					if (player.dead) counter = 0;
				
					int chosenMove = Main.rand.Next(100) + 1;
					int lostChancePercent;
				
					if (counter <= 0 && NPC.velocity.Y == 0)
					{
						if (trapped || player.dead)
						{
							SetMove(Move.BurrowAnim, 24);
							trapped = false;
							lostChancePercent = (int)Math.Floor(burrowChance / 6f) * 2;
							burrowChance -= lostChancePercent;
						}
						else
						{
							//makes his move pool less random. while testing i found if it was true random he often didn't use all his attacks
							if (chosenMove <= spinBChance && NPC.life <= NPC.lifeMax - 200)
				            {
								SetMove(Move.ShellSpinAnimB, 90);
								lostChancePercent = (int)Math.Floor(spinBChance / 6f) * 2;
								spinBChance -= lostChancePercent;
							}
							else if (chosenMove <= spinBChance + burrowChance && NPC.life <= NPC.lifeMax - 400) 
							{
								SetMove(Move.BurrowAnim, 24);
								//if (NPC.life <= NPC.lifeMax / 2) lostChancePercent = (int)Math.Floor(spinBChance / 8f) * 2;
								//else lostChancePercent = (int)Math.Floor(burrowChance / 6f) * 2;
								lostChancePercent = (int)Math.Floor(burrowChance / 8f) * 2;
								burrowChance -= lostChancePercent;
							}
							else
							{
								SetMove(Move.ShellSpinAnim, 24);
								lostChancePercent = (int)Math.Floor(spinChance / 6f) * 2;
								spinChance -= lostChancePercent;
							}
						}
						if (move != Move.ShellSpinAnim) spinChance += lostChancePercent / 2 ;
						if (move != Move.ShellSpinAnimB) spinBChance += lostChancePercent / 2;
						if (move != Move.BurrowAnim) burrowChance += lostChancePercent / 2;
						/*Main.NewText(move.ToString());
						Main.NewText(spinChance.ToString(), 0);
						Main.NewText(spinBChance.ToString(), 0, 0);
						Main.NewText(burrowChance.ToString(), 50, 50, 100);*/
					}
					//everything below in walk is just stuff to help it prevent being cheesed or getting stuck
					else if (NPC.velocity.Y == 0 && player.Bottom.Y + 32 < NPC.Bottom.Y && Math.Abs(player.Center.X - NPC.Center.X) < 112 || oldPos == NPC.position && NPC.collideX) //if on the ground and player is close and above it, or if it's stuck on a wall
					{
						if (preJumpHeight != null && Math.Abs((float)preJumpHeight - NPC.Bottom.Y) <= 16 && NPC.Bottom.Y - 128 > player.Bottom.Y) trapped = true; //if attempting to jump, and at the same height (or within a block) it was before its previous jump, and the player is further than its jump height out of reach
						preJumpHeight = NPC.Bottom.Y;
						jumpPeak = NPC.Bottom.Y;
						NPC.velocity.Y = -9f;
					}
					else if (Math.Abs(player.Center.X - NPC.Center.X) > 144 && NPC.velocity.Y == 0 && Math.Abs(NPC.velocity.X) < 4.25f && Math.Abs(NPC.velocity.X) > 3) // lunge to chase far away players
					{
						NPC.velocity.Y = -3.5f;
						NPC.velocity.X = 4.85f * NPC.direction * (Math.Abs(player.Center.X - NPC.Center.X) / 144);
						if (Math.Abs(NPC.velocity.X) > 7f) NPC.velocity.X = 7f * NPC.direction;
					}
					if (Math.Abs(NPC.velocity.X) > 3.1f * (1 + (speedScale - 1) / 2)) NPC.velocity.X *= 0.994f;
				
				
					if (jumpPeak != null && NPC.Bottom.Y < jumpPeak) jumpPeak = NPC.Bottom.Y;
					if (jumpPeak != null && NPC.Bottom.Y > jumpPeak)
					{
						if (NPC.collideY) trapped = true; //if it bonks its head mid-jump //just, if (collideY && NPC.velocity < 0) might work ?
						jumpPeak = null;
					}
					if (NPC.velocity.Y == 0 && NPC.Bottom.Y < player.Top.Y && Math.Abs(player.Center.X - NPC.Center.X) < 32)
					{
						trapped = true; //if it's above the player, on ground, can't get down, and the player is close horizontally
					}
					break;
				}
				case Move.ShellSpinAnim:
				{
					counter--;
					NPC.velocity.X *= 0.95f;
					if (counter <= 0)
					{
						SetMove(Move.ShellSpin, 300);
						NPC.width = (int)shellHitbox.X;
						NPC.height = (int)shellHitbox.Y;
						NPC.position.Y += heightDifference;
					}
					break;
				}
				case Move.ShellSpin:
				{
					counter--;
					if (Math.Abs(NPC.velocity.X) < 8 * speedScale) NPC.velocity.X += 0.13f * NPC.direction * speedScale;
					if (counter <= 0 && NPC.velocity.Y == 0 && Math.Abs(NPC.velocity.X) < 1 || counter < -100) SetMove(Move.ShellSpinOutAnim, 120);
					else if (oldPos == NPC.position && NPC.collideX) NPC.velocity.Y = -9f;
					break;
				}
				case Move.ShellSpinAnimB:
				{
					counter--;
					if (counter > 36) NPC.velocity.X *= 0.95f;
					if (counter == 36)
					{
						NPC.velocity.Y = -5.5f;
						NPC.velocity.X = 2.8f * NPC.direction;
					}
					if (counter <= 0)
					{
						SetMove(Move.ShellSpinB, 300);
						NPC.width = (int)shellHitbox.X;
						NPC.height = (int)shellHitbox.Y;
						NPC.position.Y += heightDifference;
					}
					break;
				}
				case Move.ShellSpinB:
				{
					counter--;
					if (Math.Abs(NPC.velocity.X) < 7 * speedScale) NPC.velocity.X += 0.08f * NPC.direction * speedScale;
				
					if (counter <= 0 && NPC.velocity.Y == 0 && Math.Abs(NPC.velocity.X) < 1.25f || counter < -120) SetMove(Move.ShellSpinOutAnim, 120);
					else if (NPC.velocity.Y == 0 && Math.Abs(player.Center.X - NPC.Center.X) < 64) //&& Math.Abs(NPC.velocity.X) > 2)
					{
						NPC.velocity.Y = -8.5f;
					}
					else if (oldPos == NPC.position && NPC.collideX) NPC.velocity.Y = -9f;
					break;
				}
				case Move.ShellSpinOutAnim:
				{
					counter--;
					NPC.velocity.X *= 0.95f;
					//NPC.aiStyle = 0;
					if (counter > 12 && notEmerging)
				    {
						if (counter % 3 == 0)
						{
							int dust1 = Dust.NewDust(new Vector2(NPC.position.X - (int)(NPC.width * 0.2f), NPC.position.Y), (int)(NPC.width * 1.4f), NPC.height, DustID.RainbowMk2, 0.0f, -2.75f, 0, new Color(60, 255, 20), 1.1f);
							Main.dust[dust1].noGravity = true;
							Main.dust[dust1].velocity.X = 0f;
						}
						/*if (counter % 3 == 0)
						{
							int dust1 = Dust.NewDust(new Vector2(NPC.position.X - (int)(NPC.width * 0.2f), NPC.position.Y), (int)(NPC.width * 1.4f), NPC.height, 284, 0.0f, -2.75f, 0, new Color(120, 255, 10), 1.25f);
							Main.dust[dust1].noGravity = true;
							Main.dust[dust1].velocity.X = 0f;
						}*/
						//NPC.ReflectProjectiles(NPC.Hitbox);
						NPC.reflectsProjectiles = true;
						NPC.GetGlobalNPC<MyNPC>().reflectsProjectilesCustom = true;
						NPC.GetGlobalNPC<MyNPC>().reflectVelocity = 2f;
					}
					else {
						NPC.reflectsProjectiles = false;
						NPC.GetGlobalNPC<MyNPC>().reflectsProjectilesCustom = false;
						NPC.GetGlobalNPC<MyNPC>().reflectVelocity = 0f;
					}
					if (counter <= 0)
					{
						//NPC.aiStyle = 3;
						NPC.width = (int)crabHitbox.X;
						NPC.height = (int)crabHitbox.Y;
						NPC.position.Y -= heightDifference + 1;
						notEmerging = true;
						SetMove(Move.Walk, Main.rand.Next(300, 400));
					}
					break;
				}
				case Move.BurrowAnim:
				{
					counter--;
					NPC.velocity.X *= 0.95f;
					if (counter <= 0)
					{
						SetMove(Move.Burrow, 1000);
						NPC.width = (int)shellHitbox.X;
						NPC.height = (int)shellHitbox.Y;
						NPC.position.Y += heightDifference;
				
						NPC.noTileCollide = true;
						NPC.noGravity = true;
						NPC.behindTiles = true;
						NPC.aiStyle = 0;
				
						burrowStage = 1;
					}
					break;
				}
				case Move.Burrow:
				{
					counter--;
					float emergePointX = player.Center.X + (25 * player.velocity.X); //crab emerges a bit ahead of the player after a delay. this code is meant to make it emerge with where the player will end up after the delay, accounting for speed //28 for randomdelay
					//if (player.velocity.X != 0) emergePointX += (player.velocity.X > 0 ? 14 : -14); //this gives a small extra to make it easier if you're moving slow. idk if needed
					if (player.dead) emergePointX = Main.maxTilesX * 16 - ((Main.maxTilesX * 16 - player.Center.X < Main.maxTilesX * 16 / 2) ? 0 : Main.maxTilesX * 16); //my most evil and fucked up code yet. makes crab burrow to closest world edge. one line, baby
				
				
					if (burrowStage != 3) NPC.velocity.X = 0;
					
					if (counter % 20 == 0 || counter % 10 == 0 && Math.Abs(NPC.velocity.X) > 7) //i dont think this can divide by zero but
					{
						if (Submerged() != 1 && burrowStage < 4) PlaySound(SoundID.WormDig, NPC.Center);
					}	
				
					switch (burrowStage)
					{
						case 1: //tunnel down
							NPC.velocity.Y += 0.075f;
							if (counter == 965) burrowStage++;
						break;
						case 2: //if still in open air, tunnel down until underground
							if (Submerged() == 0 && counter <= 950) burrowStage++;
							else if (Submerged() != 0) NPC.velocity.Y += 0.3f;
						break;
						case 3: //move horizontally to track the player
							NPC.aiStyle = 3;
							NPC.velocity.Y = 0;
							if (Math.Abs(emergePointX - NPC.Center.X) < Math.Abs(emergePointX - player.Center.X) + 16) lockedDirection = true;
							else lockedDirection = false;
				
							if (Math.Abs(NPC.velocity.X) < 20) NPC.velocity.X += 0.4f * NPC.direction;
							if (Submerged() != 0) NPC.position.Y += (5 - Submerged()) * 16;
							//Main.NewText(randomDelay.ToString());
							if (randomDelay != null && randomDelay > 0) randomDelay--; //randomdelay i took out. it does nothing rn
							if (Math.Abs(emergePointX - NPC.Center.X) < 16) //when within range of player, lock into emergePointX position, rise, raise dust at the surface
					        {
								//NPC.position.X = emergePointX - NPC.width / 2;
								//NPC.velocity.X = 0;
								//if (randomDelay == null) randomDelay = Main.rand.Next(60);
								//else if (randomDelay <= 0)
								{
									randomDelay = null;
									NPC.position.X = emergePointX - NPC.width / 2;
									NPC.velocity.X = 0;
									lockedDirection = false;
									emergeDelay = counter;
				
									int surfaceHeight = Submerged(1, 100) - 2;
									for (int x = -1; x <= 2; x++)
									{
										Tile tile = Framing.GetTileSafely((int)(NPC.BottomLeft.X / 16) + x, (int)(NPC.BottomLeft.Y / 16) - surfaceHeight);
										for (int i = 0; i <= 9; i++)
										{
											Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust((int)(NPC.BottomLeft.X / 16) + x, (int)(NPC.BottomLeft.Y / 16) - surfaceHeight, tile)];
											dust.velocity.X = Main.rand.NextFloat(-1, 1);
											dust.velocity.Y = Main.rand.NextFloat(-2, -8);
											if (i % 2 == 0)
											{
												Dust dust2 = Main.dust[WorldGen.KillTile_MakeTileDust((int)(NPC.BottomLeft.X / 16) + x, (int)(NPC.BottomLeft.Y / 16) - surfaceHeight, tile)];
												dust2.velocity.X = Main.rand.NextFloat(-2f, 2f);
												dust2.velocity.Y = -2f;
											}
										}
										PlaySound(SoundID.NPCHit23, NPC.Center);
										PlaySound(SoundID.WormDig, NPC.Center);
									}
									burrowStage++;
								}
							}
						break;
						case 4: //wait, then rise up to player position, speed scaling with how far above the player is to make sure it hits
							if (emergeDelay - 18 > counter) //if (emergeDelay - (int)(18 / speedScale ) > counter) Maybe... Maybe. //21 for randomdelay
							{
								if (Submerged() == 1)
								{
									if (NPC.behindTiles) PlaySound(SoundID.WormDig, NPC.Center);
									NPC.behindTiles = false;
								}
								if (NPC.Bottom.Y > player.Bottom.Y)
								{
									NPC.velocity.Y = -16f * (NPC.Center.Y - player.Center.Y) / 50;
									if (NPC.velocity.Y > -9f) NPC.velocity.Y = -10f;
								}
								else burrowStage++;
							}
						break;
						case 5:
							NPC.noTileCollide = false;
							NPC.noGravity = false;
							NPC.behindTiles = false;
							lockedDirection = false;
							if (NPC.velocity.Y == 0) counter = 0;
						break;
					}
					if (counter == 180)
				    {
						//Main.NewText("This message should never ever display", 255, 0, 0);
						NPC.aiStyle = 3;
						burrowStage = 4;
				    }				
					if (counter <= 0) SetMove(Move.ShellSpinOutAnim, 120);
					break;
				}
			}

			if (move == Move.ShellSpin || move == Move.ShellSpinB || move == Move.ShellSpinOutAnim && counter >= 12|| move == Move.Burrow || move == Move.Emerge)
			{
				inShell = true;
				NPC.HitSound = SoundID.NPCHit2;
				NPC.defense = 18;
			}
			else
			{
				inShell = false;
				NPC.HitSound = SoundID.NPCHit22;
				NPC.defense = 8;
			}

			//Main.NewText(NPC.velocity.X.ToString(), 255, 0);
			oldVelocityX = NPC.velocity.X;
			oldPos = NPC.position;
			NPC.spriteDirection = NPC.direction * -1;
		}

		private int Submerged(int xLimit = 4, int yLimit = 4)
        {
			for (int y = 1; y <= yLimit; y++)
			{
				for (int x = 1; x <= xLimit; x++)
				{
					Tile tile = Framing.GetTileSafely((int)(NPC.BottomLeft.X / 16) + x - 1, (int)(NPC.BottomLeft.Y / 16) - y + 1);
					if (tile.HasTile && Main.tileSolid[tile.TileType])
					{
						//Main.NewText(tile.ToString(), 50);
						//Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(16 * ((int)(NPC.BottomLeft.X / 16) + x - 1), 16 * ((int)(NPC.BottomLeft.Y / 16) - y + 1)), Vector2.Zero, ModContent.ProjectileType<RedPixel>(), 0, 0);
					}
					else return y;
				}
			}
			return 0;
		}

		private void SetMove(Move toMove, int counter)
        {
            prevMove = move;
            move = toMove;
            this.counter = counter;
		}

        /*public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
			if (inShell)
            {
				damage = (int)(damage * 0.8f);
				//Main.NewText();
            }
		}*/
		public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
			if (Main.player[projectile.owner].heldProj == projectile.whoAmI) ModifyHitByItemOrHeldProj();
		}
		public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
			ModifyHitByItemOrHeldProj();
		}
		public void ModifyHitByItemOrHeldProj()
        {
			if (inShell)
			{
				NPC.defense = 8;
				if (move == Move.ShellSpinOutAnim && counter > 12 && notEmerging) NPC.defense = 0; //unsure about this
				PlaySound(SoundID.NPCHit22, NPC.Center);
				//damage += 3;
				//Main.NewText();
			}
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return !NPC.behindTiles;
		}

		public override bool? CanFallThroughPlatforms()
		{
			if (Main.player[NPC.target].Bottom.Y - 16 > NPC.Bottom.Y)
			{
				return true;
			}
			else
			{
				return false;

			}
		}
        public override void HitEffect(NPC.HitInfo hit)
		{
			if (Main.netMode == NetmodeID.Server)
			{
				return;
			}

			if (NPC.life <= 0)
			{
				int headGoreType = Mod.Find<ModGore>("SeaCrab_Head").Type;
				int shellGoreType = Mod.Find<ModGore>("SeaCrab_Shell").Type;
				int clawGoreType = Mod.Find<ModGore>("SeaCrab_Claw").Type;
				int legsGoreType1 = Mod.Find<ModGore>("SeaCrab_Legs1").Type;
				int legsGoreType2 = Mod.Find<ModGore>("SeaCrab_Legs2").Type;
				var entitySource = NPC.GetSource_Death();
				Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), headGoreType, NPC.scale);
				Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), shellGoreType, NPC.scale);
				Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), clawGoreType, NPC.scale);
				Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), legsGoreType1, NPC.scale);
				Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5)), legsGoreType2, NPC.scale);
			}
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<SeaCrystal>()));

			LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
			notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SeaCrystal>()));
			npcLoot.Add(notExpertRule);

		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) // can delete?
		{
			int x = spawnInfo.SpawnTileX;
			int y = spawnInfo.SpawnTileY;
			int tile = Main.tile[x, y].TileType;
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
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// Makes it so whenever you beat the boss associated with it, it will also get unlocked immediately
			bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[ModContent.NPCType<SeaCrab>()], quickUnlock: true);

			bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
				new FlavorTextBestiaryInfoElement("This giant crab likes to slumber under a layer of sand, but the crystal embedded in the tip of its shell pokes out, luring many an unsuspecting adventurer to their demise.")
			});
		}
	}
}

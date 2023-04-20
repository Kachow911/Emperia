using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Npcs.GoblinArmy
{
    public class GoblinSorceress : ModNPC
    {
        private enum Move
        {
           Walk, 
		   Shoot
        }

        private int counter;

        private Move move;
        private Move prevMove;
        private Vector2 targetPosition;
		
		private bool init = false;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Goblin Sorceress");
			Main.npcFrameCount[NPC.type] = 9;
		}
        public override void SetDefaults()
        {
            NPC.lifeMax = 275;
            NPC.damage = 30;
            NPC.defense = 5;
            NPC.knockBackResist = 0f;
            NPC.width = 42;
            NPC.height = 64;
            NPC.value = Item.buyPrice(0, 0, 50, 0);
            NPC.npcSlots = 1f;
            NPC.boss = false;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
			NPC.scale = 1f;
        }
		public override void FindFrame(int frameHeight)
		{
			if (move == Move.Walk)
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 5; 
				int frame = (int)NPC.frameCounter; 
				NPC.frame.Y = frame * frameHeight; 
			}
			else if (move == Move.Shoot)
			{
				NPC.frameCounter += 0.1f;
				NPC.frameCounter %= 3; 
				int frame = (int)NPC.frameCounter + 5; 
				NPC.frame.Y = frame * frameHeight; 
			}
			
		}

        public override void AI()
		{
			if (NPC.velocity.X < 0)
				NPC.spriteDirection = -1;
			else if (NPC.velocity.X > 0)
				NPC.spriteDirection = 1;
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			if (!init)
			{
				move = Move.Walk;
				counter = 250;
				init = true;
			}
			if (move == Move.Walk)
            { 
				counter--;
				NPC.aiStyle = 3;
				AIType = 508;
				if (NPC.velocity.X > 2f)
					NPC.velocity.X = 2f;
				if (NPC.velocity.X < -2f)
					NPC.velocity.X = -2f;
				if (counter <= 0)
				{
					SetMove(Move.Shoot, 30);
				}
			}
			if (move == Move.Shoot)
			{
				counter--;
				if (player.Center.X > NPC.Center.X)
					NPC.spriteDirection = 1;
				else
					NPC.spriteDirection = -1;
				NPC.velocity.X = 0;
				int xOff = 0;
				if (NPC.spriteDirection == 1) xOff = -5;
				else xOff = 5;
				Vector2 placePosition = NPC.Center + new Vector2(-xOff, -NPC.height / 2);
				for (int index1 = 0; index1 < 3; ++index1)
				{
					Vector2 vel = new Vector2(2, 0).RotatedByRandom(MathHelper.ToRadians(360));
					int index2 = Dust.NewDust(placePosition + vel, NPC.width, NPC.height, DustID.Shadowflame, 0.0f, 0.0f, 100, new Color(), 0.8f);
					
				}
				if (counter <= 0)
				{	
					SetMove(Move.Walk, 250);
					if (Main.rand.Next(5) == 0)
					{
						for (int i = -1; i <= 1; i++)
						{
							Vector2 placePosition1 = new Vector2(player.Center.X + 100 * i, player.Center.Y - 600);
							Vector2 direction1 = player.Center - placePosition1;
							direction1.Normalize();
							Projectile.NewProjectile(NPC.GetSource_FromAI(), placePosition1.X, placePosition1.Y, direction1.X * 10f, direction1.Y * 10f, ModContent.ProjectileType<ShadowBoltHostile>(), 10, 1, Main.myPlayer, 0, 0);
						}
					}
					else
					{
						Vector2 direction = Main.player[NPC.target].Center - placePosition;
						direction.Normalize();
						int p = Projectile.NewProjectile(NPC.GetSource_FromAI(), placePosition.X, placePosition.Y, direction.X * 8f, direction.Y * 8f, ModContent.ProjectileType<ShadowBoltHostile>(), 22, 1, Main.myPlayer, 0, 0);
					}
					
				}
			}
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
		/*public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.SpawnTileX;
			int y = spawnInfo.SpawnTileY;
			int tile = Main.tile[x, y].TileType;
			return Main.invasionType == 1 ? 0.05f : 0;
		}*/
		/*public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/Yeti/gore1"), 1f);
			/*if (!EmperialWorld.downedMushor)
			{
            	Main.NewText("The guardian of the mushroom biome has fallen...", 0, 75, 161, false);
				EmperialWorld.downedMushor = true;
			}
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<YetiTrophy>());
			}
			if (Main.expertMode)
			{
				NPC.DropBossBags();
			}
			else
			{
				
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<MammothineClub>());
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<HuntersSpear>());
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<BigGameHunter>());
				}
				
				if (Main.rand.Next(7) == 0)
				{
					Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<YetiMask>());
				}
				if (Main.rand.Next(10) == 0)
				{
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<ChilledFootprint>());
				}
				if (Main.rand.Next(2) == 0)
				{
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<ArcticIncantation>());
				}
			}
		}*/

	}
}

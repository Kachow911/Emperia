using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;
using Emperia.Items.Weapons.GoblinArmy;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Npcs.GoblinArmy
{
    public class GoblinGiant : ModNPC
    {
        private enum Move
        {
           Walk, 
		   Shoot
        }

		private int counter = 0;

        private Move move { get { return (Move)NPC.ai[1]; } set { NPC.ai[1] = (int)value; } }
        private Move prevMove;
        private Vector2 targetPosition;
		private int goblinCounter = 600;
		private bool init = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Giant");
			Main.npcFrameCount[NPC.type] = 11;
		}
        public override void SetDefaults()
        {
            NPC.lifeMax = 2000;
            NPC.damage = 60;
            NPC.defense = 12;
            NPC.knockBackResist = 0f;
            NPC.width = 128;
            NPC.height = 132;
            NPC.value = Item.buyPrice(0, 2, 0, 0);
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
				NPC.frameCounter %= 8; 
				int frame = (int)NPC.frameCounter; 
				NPC.frame.Y = frame * frameHeight; 
			}
			else if (move == Move.Shoot)
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 3; 
				int frame = (int)NPC.frameCounter + 8; 
				NPC.frame.Y = frame * frameHeight; 
			}
			
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 2250;
            NPC.damage = 55;
        }

        public override void AI()
		{
			goblinCounter--;
			if (goblinCounter <= 0)
            {
				NPC.NewNPC((int)NPC.Center.X + Main.rand.Next(-25, 25), (int)NPC.Center.Y + 40, NPCType<GoblinRamCarrier>());
				NPC.NewNPC((int)NPC.Center.X + Main.rand.Next(-25, 25), (int)NPC.Center.Y + 40, NPCType<GoblinRamCarrier>());
				NPC.NewNPC((int)NPC.Center.X + Main.rand.Next(-25, 25), (int)NPC.Center.Y + 40, NPCType<GoblinRamCarrier>());
				goblinCounter = 600;
            }
			if (NPC.velocity.X < 0)
				NPC.spriteDirection = -1;
			else if (NPC.velocity.X > 0)
				NPC.spriteDirection = 1;
			NPC.TargetClosest(true);
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
				if (NPC.velocity.X > 1.2f)
					NPC.velocity.X = 1.2f;
				if (NPC.velocity.X < -1.2f)
					NPC.velocity.X = -1.2f;
				if (counter <= 0)
				{
					SetMove(Move.Shoot, 15);
				}
			}
			if (move == Move.Shoot)
			{
				Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, player.position);
				counter--;
				if (player.Center.X > NPC.Center.X)
					NPC.spriteDirection = 1;
				else
					NPC.spriteDirection = -1;
				NPC.velocity = Vector2.Zero;
				if (counter <= 0)
				{
					SetMove(Move.Walk, 300);
					Vector2 placePosition = NPC.Center + new Vector2(0, -NPC.height / 2);
					Vector2 direction = Main.player[NPC.target].Center + new Vector2(0, -10) - placePosition;
				
					direction.Normalize();
					for (int index = 0; index < 10; ++index)
						Dust.NewDust(placePosition, NPC.width, NPC.height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
					for (int index1 = 0; index1 < 10; ++index1)
					{
						int index2 = Dust.NewDust(placePosition, NPC.width, NPC.height, 6, 0.0f, 0.0f, 100, new Color(), 2.5f);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].velocity *= 3f;
						int index3 = Dust.NewDust(placePosition, NPC.width, NPC.height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
						Main.dust[index3].velocity *= 2f;
					}
					Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), NPC.Center.X, NPC.Center.Y - NPC.height/2, direction.X * 12f, direction.Y * 12f, ModContent.ProjectileType<GoblinBomb>(), NPC.damage / 3, 1, Main.myPlayer, 0, 0);
					
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
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.spawnTileX;
			int y = spawnInfo.spawnTileY;
			int tile = Main.tile[x, y].TileType;
			return Main.invasionType == 1 ? 0.02f : 0;
		}

        public override void OnKill()
        {
			Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/Goblin/GoblinGiantGoreHead").Type, 1f);
			Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/Goblin/GoblinGiantGoreLeg").Type, 1f);
			Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/Goblin/GoblinGiantGoreArm").Type, 1f);
			Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/Goblin/GoblinGiantGoreCannon_4").Type, 1f);
			Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/Goblin/GoblinGiantGoreCannon_3").Type, 1f);
			Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/Goblin/GoblinGiantGoreCannon_1").Type, 1f);

			for (int i = 0; i < 25; i++)
			{
				int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 7);
				Vector2 vel = new Vector2(0, -5).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
			}
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.GiantPlating>(), Main.rand.Next(3, 8));
				
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<GiantsDagger>());
				}
				if (Main.rand.Next(5) == 0)
				{
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<GiantsDevastator>());
				}
				if (Main.rand.Next(5) == 0)
				{
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<OversizedFemur>());
				}
			if (Main.rand.Next(5) == 0)
			{
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<GiantsHead>());
			}
		}

	}
}

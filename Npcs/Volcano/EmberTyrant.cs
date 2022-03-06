using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Npcs.Volcano
{
    public class EmberTyrant : ModNPC
    {
        private enum Move
        {
            Hover,
			Go,
			Slam,
            BigShoot
        }

        private Move move { get { return (Move)NPC.ai[0]; } set { NPC.ai[0] = (int)value; } }
        private Move prevMove;
		bool init = false;
		private int counter = 300;

		private Vector2 targetPosition;
		
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ember Tyrant");
			Main.npcFrameCount[NPC.type] = 1;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 2500;
            NPC.damage = 75;
            NPC.defense = 20;
            NPC.knockBackResist = 0f;
            NPC.width = 94;
            NPC.height = 100;
			NPC.alpha = 0;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.npcSlots = 1f;
            NPC.boss = false;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
			NPC.ai[3] = 0; //phase: 0 is creation, 1 is first, 2 is second
			NPC.frameCounter = 0;
			
            NPC.netAlways = true;

        }

        

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 3200;
            NPC.damage = 100;
        }

        public override void AI()
        {
			NPC.dontTakeDamage = false;
            Player player = Main.player[NPC.target];
			NPC.TargetClosest(true);
			if (!init)
			{
				move = Move.Hover;
				init = true;
			}
            if (!player.active || player.dead)
			{
				NPC.TargetClosest(false);
				NPC.velocity.X = 0;
				NPC.velocity.Y = 15;
				NPC.timeLeft = 10;
			}
			
			
			if (move == Move.Hover)
			{
				counter--;
				NPC.velocity.X = 0;
				NPC.velocity.Y = 0.5f * (float)Math.Cos(MathHelper.ToRadians(counter * 2));
				if (counter % 100 == 0)
				{
					Vector2 Speed = player.Center - NPC.Center;
					Speed.Normalize();
					Speed *= 10f;
					for (int i = 0; i < 3; i++)
					{
						 Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), NPC.Center.X, NPC.Center.Y - 25, Speed.X * Main.rand.Next(3, 8), Speed.Y * Main.rand.Next(3, 8), ModContent.ProjectileType<ScorchBlastHost>(), 25, 2f, player.whoAmI);
					}
				}
				if (counter <= 0)
				{
                    if (Main.rand.NextBool(2))
                   {
                        SetMove(Move.Go, 0);
                        targetPosition = player.Center + new Vector2(0, -450);
                   }
                   else
                      SetMove(Move.BigShoot, 300);
				}
			}
			if (move == Move.Go)
			{
				counter++;
                NPC.noTileCollide = true;
                SmoothMoveToPosition(targetPosition, 2f, 6f);
				if (NPC.Distance(targetPosition) < 32 || counter > 80)
                {
                    SetMove(Move.Slam, 0);
                    NPC.velocity.Y = 12;
                    
                }
					
			}
            if (move == Move.Slam)
            {
                NPC.velocity.X = 0;
                NPC.noTileCollide = false;
                if (NPC.velocity.Y <= 0)
                {
                    for (int i = 0; i < 50; ++i) 
                    {
                        int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 258);
                        int dust1 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 258);
                        Main.dust[dust1].scale = 1.5f;
                        Main.dust[dust1].velocity *= 1.5f;
                        int dust2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 258);
                        Main.dust[dust2].scale = 1.5f;
                    }
                    SetMove(Move.Hover, 300);
                }
            }
			if (move == Move.BigShoot)
			{
				if (counter == 300)
				{
					int n = NPC.NewNPC(NPC.GetSpawnSourceForNPCFromNPCAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<EmberTyrantHead>(), ai0: NPC.whoAmI);
					NPC.ai[3]++;
				}
				counter--;
				NPC.velocity = Vector2.Zero;
                NPC.noTileCollide = false;
				NPC.dontTakeDamage = true;
				if (NPC.ai[3] <= 0)
				{
					SetMove(Move.Hover, 300);
				}
				if (counter <= 0)
				{
					for (int i = 0; i < 360; i += 36)
					{
					Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
					vec.Normalize();
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 258, 0f, 0f, 158, new Color(53f, 67f, 253f), 1f);
					Main.dust[num622].velocity += (vec *2f);
					Main.dust[num622].noGravity = true;
					}
					SetMove(Move.Hover, 300);
				}
			}
        }
        private void SetMove(Move move, int counter)
        {
            this.prevMove = this.move;
            this.move = move;
			this.counter = counter;
        }

        private bool IsInPhaseTwo()
        {
            return NPC.life <= NPC.lifeMax * .5;    //50% hp
        }
        private void SmoothMoveToPosition(Vector2 toPosition, float addSpeed, float maxSpeed, float slowRange = 64, float slowBy = .95f)
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
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem(NPC.GetItemSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Weapons.Volcano.EmberTyrantStaff>());
            }
        }
    }
}

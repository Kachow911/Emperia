using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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

        private Move move { get { return (Move)npc.ai[0]; } set { npc.ai[0] = (int)value; } }
        private Move prevMove;
		bool init = false;
		private int counter = 300;

		private Vector2 targetPosition;
		
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ember Tyrant");
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 2500;
            npc.damage = 75;
            npc.defense = 20;
            npc.knockBackResist = 0f;
            npc.width = 94;
            npc.height = 100;
			npc.alpha = 0;
            npc.value = Item.buyPrice(0, 5, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = false;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
			npc.ai[3] = 0; //phase: 0 is creation, 1 is first, 2 is second
			npc.frameCounter = 0;
			
            npc.netAlways = true;

        }

        

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 3200;
            npc.damage = 100;
        }

        public override void AI()
        {
			npc.dontTakeDamage = false;
            Player player = Main.player[npc.target];
			npc.TargetClosest(true);
			if (!init)
			{
				move = Move.Hover;
				init = true;
			}
            if (!player.active || player.dead)
			{
				npc.TargetClosest(false);
				npc.velocity.X = 0;
				npc.velocity.Y = 15;
				npc.timeLeft = 10;
			}
			
			
			if (move == Move.Hover)
			{
				counter--;
				npc.velocity.X = 0;
				npc.velocity.Y = 0.5f * (float)Math.Cos(MathHelper.ToRadians(counter * 2));
				if (counter % 100 == 0)
				{
					Vector2 Speed = player.Center - npc.Center;
					Speed.Normalize();
					Speed *= 10f;
					for (int i = 0; i < 3; i++)
					{
						 Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 25, Speed.X * Main.rand.Next(3, 8), Speed.Y * Main.rand.Next(3, 8), mod.ProjectileType("ScorchBlastHost"), 25, 2f, player.whoAmI);
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
                npc.noTileCollide = true;
                SmoothMoveToPosition(targetPosition, 2f, 6f);
				if (npc.Distance(targetPosition) < 32 || counter > 80)
                {
                    SetMove(Move.Slam, 0);
                    npc.velocity.Y = 12;
                    
                }
					
			}
            if (move == Move.Slam)
            {
                npc.velocity.X = 0;
                npc.noTileCollide = false;
                if (npc.velocity.Y <= 0)
                {
                    for (int i = 0; i < 50; ++i) 
                    {
                        int dust = Dust.NewDust(npc.position, npc.width, npc.height, 258);
                        int dust1 = Dust.NewDust(npc.position, npc.width, npc.height, 258);
                        Main.dust[dust1].scale = 1.5f;
                        Main.dust[dust1].velocity *= 1.5f;
                        int dust2 = Dust.NewDust(npc.position, npc.width, npc.height, 258);
                        Main.dust[dust2].scale = 1.5f;
                    }
                    SetMove(Move.Hover, 300);
                }
            }
			if (move == Move.BigShoot)
			{
				if (counter == 300)
				{
					int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("EmberTyrantHead"), ai0: npc.whoAmI);
					npc.ai[3]++;
				}
				counter--;
				npc.velocity = Vector2.Zero;
                npc.noTileCollide = false;
				npc.dontTakeDamage = true;
				if (npc.ai[3] <= 0)
				{
					SetMove(Move.Hover, 300);
				}
				if (counter <= 0)
				{
					for (int i = 0; i < 360; i += 36)
					{
					Vector2 vec = Vector2.Transform(new Vector2(-1, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
					vec.Normalize();
					int num622 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 258, 0f, 0f, 158, new Color(53f, 67f, 253f), 1f);
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
            return npc.life <= npc.lifeMax * .5;    //50% hp
        }
        private void SmoothMoveToPosition(Vector2 toPosition, float addSpeed, float maxSpeed, float slowRange = 64, float slowBy = .95f)
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
        }
        public override void NPCLoot()
        {
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EmberTyrantStaff"));
            }
        }
    }
}

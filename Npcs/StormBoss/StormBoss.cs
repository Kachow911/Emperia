using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Npcs.StormBoss
{
	[AutoloadBossHead]
    public class StormBoss : ModNPC
    {
        private enum Move
        {
           LightningDash,
           DashRecovery,
		   Shockwave,
		   SweepingBeams,
		   Lightning
        }

		private int counter = 0;

		private Move move;
        private Move prevMove;
        private Vector2 targetPosition;


        private bool phase2Active;
		private bool init = false;
        public Vector2 initialPosition;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Tezlord");
			Main.npcFrameCount[NPC.type] = 6;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 12500;
            NPC.damage = 75;
            NPC.defense = 18;
            NPC.knockBackResist = 0f;
            NPC.width = 176;
            NPC.height = 176;
            NPC.value = Item.buyPrice(0, 8, 0, 0);
            NPC.npcSlots = 1f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
           // NPC.HitSound = SoundID.NPCHit1; 
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            NPC.netAlways = true;
			//bossBag = ModContent.ItemType<StormBag>();
        }
      
        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.2f;
            NPC.frameCounter %= 6;
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = 25500;
            NPC.damage = 100;
        }

        public override void AI()
		{
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            if (!init)
            {
                move = Move.LightningDash;
                counter = 100;
                init = true;
            }
            if (move == Move.LightningDash)
            {
                //Main.NewText("Big");
                counter--;
                Vector2 toPlayer = new Vector2(player.Center.X - NPC.Center.X, player.Center.Y - NPC.Center.Y);
                toPlayer.Normalize();
                
                //LookAt(player.Center);
                NPC.rotation = (float)Math.Atan2((double)toPlayer.Y, (double)toPlayer.X) - 1.57f;
                float distToPlayer = (float)Math.Sqrt(Math.Abs(NPC.Center.X - player.Center.X) * Math.Abs(NPC.Center.X - player.Center.X) + Math.Abs(NPC.Center.Y - player.Center.Y) * Math.Abs(NPC.Center.Y - player.Center.Y));
                int multFactor;
                if (distToPlayer < 100)
                {
                    multFactor = 30;
                }
                else if (distToPlayer < 800)
                {
                    multFactor = 40;
                }
                else
                {
                    multFactor = 60;
                }
                    if (counter <= 0)
                {
                    NPC.velocity = toPlayer.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-10, 10))) * (float)multFactor; // jets in somewhat of a lightning pattern towards the player
                    initialPosition = NPC.Center;
                    SetMove(Move.DashRecovery, 180);
                }
                
            }
            else if (move == Move.DashRecovery)
            {
                if (LineIntersectsRect(new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(initialPosition.X, initialPosition.Y), player.Hitbox))
                {
                    Main.NewText("Intersection");
                }
               // Line line1 = new Line(NPC.Center, player.Center);
               
                Vector2 toBoss = new Vector2(NPC.Center.X - initialPosition.X, NPC.Center.Y - initialPosition.Y);
                
                int numDusts = 100;
                for (float i = 0; i <= numDusts; i++)
                {
                    if (Main.rand.Next(1, 10) <= 3) {
                        int dust1 = Dust.NewDust(new Vector2(initialPosition.X + ((NPC.Center.X - initialPosition.X) * (i / numDusts)), initialPosition.Y + ((NPC.Center.Y - initialPosition.Y) * (i / numDusts))), 8, 8, DustID.Electric);
                        Main.dust[dust1].noGravity = true;
                    }
                }
                //if (counter % 2 == 0)
                NPC.velocity *= .95f;
                counter--;
                if (counter <= 0)
                {
                    SetMove(Move.Lightning, 60);

                }
            }
            else if (move == Move.Lightning)
            {
                int dust = Dust.NewDust(NPC.Center + new Vector2(0, 25), 8, 8, DustID.Electric);
                if (counter == 0)
                {

                    SetMove(Move.LightningDash, 100);
                }
                counter--;
            } else if (move == Move.Shockwave)
            {
                counter--;
            } else if (move == Move.SweepingBeams)
            {                
                counter--;
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
        public static bool LineIntersectsRect(Vector2 p1, Vector2 p2, Rectangle r)
        {
            return LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y), new Vector2(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y), new Vector2(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y + r.Height), new Vector2(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y + r.Height), new Vector2(r.X, r.Y)) ||
                   r.Contains(new Point((int)(p1.X), (int)(p1.Y))) && r.Contains(new Point((int)(p2.X), (int)(p2.Y)));
        }

        private static bool LineIntersectsLine(Vector2 l1p1, Vector2 l1p2, Vector2 l2p1, Vector2 l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }
        //private Vector2[] getJaggedPoints(Vector2 p1, Vector2 p2)
        //{
           // return [new Vector2(0, 0)];
        //}
       
    }
    
}


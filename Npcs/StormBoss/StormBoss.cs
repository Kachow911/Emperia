using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.StormBoss
{
	//[AutoloadBossHead]
    public class StormBoss : ModNPC
    {
        private enum Move
        {
           LightningDash,
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
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tezlord");
			Main.npcFrameCount[npc.type] = 6;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 12500;
            npc.damage = 75;
            npc.defense = 18;
            npc.knockBackResist = 0f;
            npc.width = 176;
            npc.height = 176;
            npc.value = Item.buyPrice(0, 8, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; 
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            Main.npcFrameCount[npc.type] = 13;
            npc.netAlways = true;
			bossBag = mod.ItemType("StormBag");
        }
      
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.2f;
            npc.frameCounter %= 6;
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 25500;
            npc.damage = 100;
        }

        public override void AI()
		{
            Player player = Main.player[npc.target];
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
                Vector2 toPlayer = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
                if (toPlayer.Length() > 4 && counter % 25 == 0)
                {
                    npc.velocity = (toPlayer / 4).RotatedBy(Main.rand.Next(-15, 15)); // jets in somewhat of a lightning pattern towards the player
                }
                move = Move.Lightning;
            }
            else if (move == Move.Lightning)
            {
                counter--;
            } else if (move == Move.Shockwave)
            {
                counter--;
            } else if (move == Move.SweepingBeams)
            {
                counter--;
            }
        }
		

        public override void PostDraw(SpriteBatch batch, Color drawColor)
        {
           
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
			
			
		}

        public Vector2 rotateVector(Vector2 v, float degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            float sin = (float)Math.Sin(radians);
            float cos = (float)Math.Cos(radians);

            float tx = v.X;
            float ty = v.Y;

            return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
        }
    }
}

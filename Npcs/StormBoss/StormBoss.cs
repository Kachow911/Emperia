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
           Chase,
		   Charge,
		   ThrowBombs,
		   SporeStorm,
		   SpawnMinions,
		   Shielding
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
            
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
        }

        public override void AI()
		{
			
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
    }
}

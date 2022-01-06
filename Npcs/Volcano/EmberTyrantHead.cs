using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Volcano
{
    public class EmberTyrantHead : ModNPC
    {
		int timer = 0;
        private NPC parent { get { return Main.npc[(int)NPC.ai[0]]; } set { NPC.ai[0] = value.whoAmI; } }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magmous Shield");
			Main.npcFrameCount[NPC.type] = 1;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 250;
            NPC.damage = 0;
            //NPC.defense = 7;
            NPC.knockBackResist = 0f;
            NPC.width = 34;
            NPC.height = 54;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.npcSlots = 0f;
            NPC.boss = false;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;

            NPC.netAlways = true;
        }
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 250;
            NPC.damage = 10;
        }
        public override void AI() {
			NPC.alpha = 255;
            NPC.Center = parent.Center + new Vector2(0, 15);
			timer++;
			if (timer >= 300)
				NPC.life = 0;
			
        }

        public override bool CheckDead()
        {
            if (NPC.life <= 0)
            {
                parent.ai[3] = 0;
            }
            return true;
        }
    }
    
}

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
        private NPC parent { get { return Main.npc[(int)npc.ai[0]]; } set { npc.ai[0] = value.whoAmI; } }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magmous Shield");
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 250;
            npc.damage = 0;
            //npc.defense = 7;
            npc.knockBackResist = 0f;
            npc.width = 34;
            npc.height = 54;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 0f;
            npc.boss = false;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;

            npc.netAlways = true;
        }
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 250;
            npc.damage = 10;
        }
        public override void AI() {
			npc.alpha = 255;
            npc.Center = parent.Center + new Vector2(0, 15);
			timer++;
			if (timer >= 300)
				npc.life = 0;
			
        }

        public override bool CheckDead()
        {
            if (npc.life <= 0)
            {
                parent.ai[3] = 0;
            }
            return true;
        }
    }
    
}

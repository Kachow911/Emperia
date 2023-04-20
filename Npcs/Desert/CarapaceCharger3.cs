using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace Emperia.Npcs.Desert
{    
    public class CarapaceCharger3 : ModNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Carapage Charger");
			Main.npcFrameCount[NPC.type] = 6;
		}
        public override void SetDefaults()
        {
            NPC.width = 42;     //this is where you put the NPC sprite width.     important
            NPC.height = 32;      //this is where you put the NPC sprite height.   important
            NPC.damage = 10;
            NPC.defense = 1;
            NPC.lifeMax = 1;
            NPC.knockBackResist = 0.0f;
            NPC.behindTiles = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.dontCountMe = true;
            NPC.HitSound = SoundID.NPCHit1;
        }
 
        public override bool PreAI()
        {
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
                NPC.timeLeft = 300;
 
            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)NPC.ai[1]].active)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    
                }
            }
 
            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                // We're getting the center of this NPC.
                Vector2 npcCenter = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
                // Then using that center, we calculate the direction towards the 'parent NPC' of this NPC.
                float dirX = Main.npc[(int)NPC.ai[1]].position.X + (float)(Main.npc[(int)NPC.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)NPC.ai[1]].position.Y + (float)(Main.npc[(int)NPC.ai[1]].height / 2) - npcCenter.Y;
                // We then use Atan2 to get a correct rotation towards that parent NPC.
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                // We also get the length of the direction vector.
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY) * 2;
                // We calculate a new, correct distance.
                float dist = (length - (float)NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;
 
                // Reset the velocity of this NPC, because we don't want it to move on its own
                NPC.velocity = Vector2.Zero;
                // And set this NPCs position accordingly to that of this NPCs parent NPC.
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;
            }
            return false;
        }
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.2f;
			NPC.frameCounter %= 6; 
			int frame = (int)NPC.frameCounter; 
			NPC.frame.Y = frame * frameHeight;
		}
        /*public override bool PreDraw(Microsoft.Xna.Framework.Graphics.ref Color drawColor)
        {
            Texture2D texture = Main.npcTexture[NPC.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Main.EntitySpriteDraw(texture, NPC.Center - Main.screenPosition, new Rectangle?(), drawColor, NPC.rotation, origin, NPC.scale, SpriteEffects.None, 0);
            return false;
        }*/
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
 
            return false;      //this make that the NPC does not have a health bar
        }
		
    }
}
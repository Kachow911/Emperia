using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Npcs.Yeti
{
    public class YetilingInit : ModNPC
    {
        int counter = 60;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yetiling");
			Main.npcFrameCount[NPC.type] = 1;
		}
        public override void SetDefaults()
        {
            NPC.lifeMax = 1;
            NPC.damage = 0;
            NPC.defense = 10;
            NPC.knockBackResist = 0.2f;
            NPC.width = 58;
            NPC.height = 56;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.npcSlots = 1f;
            NPC.boss = false;
            NPC.lavaImmune = false;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
			NPC.scale = 1f;
            NPC.dontTakeDamage = true;
        }

        public override void AI()
		{
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            NPC.velocity = Vector2.Zero;
            if (counter == 60)
            {
                if (player.position.X < NPC.Center.X)
                    NPC.spriteDirection = -1;
                else if (player.position.X > NPC.Center.X)
                    NPC.spriteDirection = 1;
            }
            counter--;
            if (counter==0)
            {
                for (int i = -50; i < 50; i++)
                {
                    Color rgb = new Color(255, 255, 255);
                    int index2 = Dust.NewDust(NPC.Center, NPC.width, NPC.height, 76, NPC.velocity.X / 5, (float)NPC.velocity.Y, 0, rgb, 0.9f);
                }
                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<Yetiling>());
                NPC.life = 0;
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
		
        
    }
}

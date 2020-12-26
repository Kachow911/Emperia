using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Yeti
{
    public class YetilingInit : ModNPC
    {
        int counter = 60;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yetiling");
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.lifeMax = 1;
            npc.damage = 0;
            npc.defense = 10;
            npc.knockBackResist = 0.2f;
            npc.width = 58;
            npc.height = 56;
            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.npcSlots = 1f;
            npc.boss = false;
            npc.lavaImmune = false;
            npc.noGravity = true;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
			npc.scale = 1f;
            npc.dontTakeDamage = true;
        }

        public override void AI()
		{
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            npc.velocity = Vector2.Zero;
            if (counter == 60)
            {
                if (player.position.X < npc.Center.X)
                    npc.spriteDirection = -1;
                else if (player.position.X > npc.Center.X)
                    npc.spriteDirection = 1;
            }
            counter--;
            if (counter==0)
            {
                for (int i = -50; i < 50; i++)
                {
                    Color rgb = new Color(255, 255, 255);
                    int index2 = Dust.NewDust(npc.Center, npc.width, npc.height, 76, npc.velocity.X / 5, (float)npc.velocity.Y, 0, rgb, 0.9f);
                }
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("Yetiling"));
                npc.life = 0;
            }
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
		
        
    }
}

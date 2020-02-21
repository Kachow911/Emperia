using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Inquisitor
{
    public class EocPuppet : ModNPC
    {
		private NPC parent { get { return Main.npc[(int)npc.ai[1]]; } }
		int move = 1;
		int counter = 120;
		int dashCount = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eye of Cthulhu Puppet");
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 500;
            npc.damage = 50;
            npc.defense = 7;
            npc.knockBackResist = 100f;
            npc.width = 28;
            npc.height = 40;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 0f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; //57 //true
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            npc.netAlways = true;
        }
		 public override void AI()
        {
			npc.TargetClosest(true);
			npc.dontTakeDamage = true;
			Player player = Main.player[npc.target];
			if (move == 1)
			{
				LookToPlayer();
				Vector2 direction1 = Main.player[npc.target].Center - npc.Center;
				direction1.Normalize();
				npc.velocity = direction1 * 3f;
				counter--;
				if (counter > 20)
				{
					dashCount = 0;
				}
				if (counter <= 0)
				{
					dashCount++;
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
					direction.Normalize();
					npc.velocity = direction * 26f;
					move = 2;
					counter = 30;
				}
			}
			if (move == 2)
			{
				counter--;
				npc.velocity *= .9f;
				if (counter <= 0)
				{
					move = 1;
					if (dashCount < 5) {
						counter = 5;
					} else {
						counter = 120;
					}
				}
			}
			if (!parent.active) {
                npc.life = 0;
            }
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
			for(int k = 0; k < npc.oldPos.Length; k++)
			{
				Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
				Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
				spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
			}
            return true;
        }

		private void LookToPlayer()
		{
			Vector2 look = Main.player[npc.target].Center - npc.Center;
			LookInDirection(look);
		}

		private void LookInDirection(Vector2 look)
		{
			float angle = 0.5f * (float)Math.PI;
			if (look.X != 0f)
			{
				angle = (float)Math.Atan(look.Y / look.X);
			}
			else if (look.Y < 0f)
			{
				angle += (float)Math.PI;
			}
			if (look.X < 0f)
			{
				angle += (float)Math.PI;
			}
			npc.rotation = angle;
		}
    }
    
}

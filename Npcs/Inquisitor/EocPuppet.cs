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
		private NPC parent { get { return Main.npc[(int)NPC.ai[1]]; } }
		int move = 1;
		int counter = 120;
		int dashCount = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eye of Cthulhu Puppet");
			Main.npcFrameCount[NPC.type] = 1;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 500;
            NPC.damage = 50;
            NPC.defense = 7;
            NPC.knockBackResist = 100f;
            NPC.width = 28;
            NPC.height = 40;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.npcSlots = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1; //57 //true
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            NPC.netAlways = true;
        }
		 public override void AI()
        {
			NPC.TargetClosest(true);
			NPC.dontTakeDamage = true;
			Player player = Main.player[NPC.target];
			if (move == 1)
			{
				LookToPlayer();
				Vector2 direction1 = Main.player[NPC.target].Center - NPC.Center;
				direction1.Normalize();
				NPC.velocity = direction1 * 3f;
				counter--;
				if (counter > 20)
				{
					dashCount = 0;
				}
				if (counter <= 0)
				{
					dashCount++;
					Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
					direction.Normalize();
					NPC.velocity = direction * 26f;
					move = 2;
					counter = 30;
				}
			}
			if (move == 2)
			{
				counter--;
				NPC.velocity *= .9f;
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
                NPC.life = 0;
            }
		}
		public override bool PreDraw(SpriteBatch spritebatch, Vector2 Vector2, Color lightColor)
        {
			Vector2 drawOrigin = new Vector2(Mod.Assets.Request<Texture2D>("Npcs/Inquisitor/StickyHandChain").Value.Width * 0.5f, NPC.height * 0.5f);
			for(int k = 0; k < NPC.oldPos.Length; k++)
			{
				Vector2 drawPos = NPC.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, NPC.gfxOffY);
				Color color = NPC.GetAlpha(lightColor) * ((float)(NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
				Main.EntitySpriteDraw(Mod.Assets.Request<Texture2D>("Npcs/Inquisitor/EocPuppet").Value, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);
			}
            return true;
        }

		private void LookToPlayer()
		{
			Vector2 look = Main.player[NPC.target].Center - NPC.Center;
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
			NPC.rotation = angle;
		}
    }
    
}

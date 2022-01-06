using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Kraken
{
    public class DepthCharge : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Depth Charge");
			Main.npcFrameCount[NPC.type] = 1;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 80;
            NPC.damage = 32;
            NPC.defense = 7;
            NPC.knockBackResist = 0f;
            NPC.width = 16;
            NPC.height = 16;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.npcSlots = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            NPC.netAlways = true;
			
			NPC.scale = 2f;
        }
		 public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
        }
		public override void AI()
        {
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			float num1 = player.Center.X;
			float num2 = player.Center.Y;
			float num3 = Math.Abs(NPC.Center.X - num1) + Math.Abs(NPC.Center.Y - num2);
			if (num3 < 100f)
			{
				for (int i = 0; i < Main.player.Length; i++)
				{
                if (NPC.Distance(Main.player[i].Center) < 100f)
                    Main.player[i].Hurt(Terraria.DataStructures.PlayerDeathReason.ByNPC(NPC.whoAmI), NPC.damage, 0);
					Main.player[i].velocity.Y = 50f;
				}
				for (int i = 0; i < 360; i++)
				{
                Vector2 vec = Vector2.Transform(new Vector2(-100, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                if (i % 8 == 0)
                {   //odd
                    Dust.NewDust(NPC.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 103);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(NPC.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 103, vec.X * 2, vec.Y * 2);
                }
				}
				NPC.life = 0;
			}
		}
    }
    
}

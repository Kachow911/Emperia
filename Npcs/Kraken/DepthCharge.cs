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
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 80;
            npc.damage = 32;
            npc.defense = 7;
            npc.knockBackResist = 0f;
            npc.width = 16;
            npc.height = 16;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 0f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            npc.netAlways = true;
			
			npc.scale = 2f;
        }
		 public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
        }
		public override void AI()
        {
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			float num1 = player.Center.X;
			float num2 = player.Center.Y;
			float num3 = Math.Abs(npc.Center.X - num1) + Math.Abs(npc.Center.Y - num2);
			if (num3 < 100f)
			{
				for (int i = 0; i < Main.player.Length; i++)
				{
                if (npc.Distance(Main.player[i].Center) < 100f)
                    Main.player[i].Hurt(Terraria.DataStructures.PlayerDeathReason.ByNPC(npc.whoAmI), npc.damage, 0);
					Main.player[i].velocity.Y = 50f;
				}
				for (int i = 0; i < 360; i++)
				{
                Vector2 vec = Vector2.Transform(new Vector2(-100, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                if (i % 8 == 0)
                {   //odd
                    Dust.NewDust(npc.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 103);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(npc.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 103, vec.X * 2, vec.Y * 2);
                }
				}
				npc.life = 0;
			}
		}
    }
    
}

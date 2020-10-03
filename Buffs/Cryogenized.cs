using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Buffs
{
    public class Cryogenized : ModBuff
    {
        public override void SetDefaults()
        {
           DisplayName.SetDefault("Cryogenized");
			Description.SetDefault("Frozen Solid");         
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<MyNPC>().cryogenized = true;
            npc.velocity = Vector2.Zero;
            if (Main.rand.Next(10) == 0)
            {
                int num1 = Dust.NewDust(npc.position, npc.width, npc.height, 92, 0f, 0f, 0, default(Color), 0.75f);
            }
            //Main.dust[num1].noGravity = false;
            //Main.dust[num1].velocity.Y *= 2f;
        }
    }
}

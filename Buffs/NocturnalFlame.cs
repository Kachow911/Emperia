using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Emperia;
using System;

namespace Emperia.Buffs
{
    public class NocturnalFlame: ModBuff
    {
        public override void SetStaticDefaults()
        {
           // DisplayName.SetDefault("Nocturnal Flames");
			// Description.SetDefault("You are engulfed in dark flames that grow hotter every second");         
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC NPC, ref int buffIndex)
        {
            if (Main.rand.Next(12 - NPC.GetGlobalNPC<MyNPC>().nightFlame) == 0)
            {
                if (Main.rand.Next(3) == 0) Dust.NewDust(NPC.position, NPC.width, NPC.height, 14, 0f, 0f, 180, default(Color), 1.4f);
                int dust = Dust.NewDust(NPC.position, NPC.width, NPC.height, 27, NPC.velocity.X * 0.2f * NPC.direction, NPC.velocity.Y * 0.2f, 0, default(Color), 1f);
                Main.dust[dust].noGravity = true;
            }
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (Main.rand.Next(11 - player.GetModPlayer<MyPlayer>().nightFlame) == 0)
            {
                if (Main.rand.Next(3) == 0) Dust.NewDust(player.position, player.width, player.height, 14, 0f, 0f, 180, default(Color), 1.4f);
                int dust = Dust.NewDust(player.position, player.width, player.height, 27, player.velocity.X * 0.2f * player.direction, player.velocity.Y * 0.2f, 0, default(Color), 1f);
                Main.dust[dust].noGravity = true;
            }
        }


    }
}

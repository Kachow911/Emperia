using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Buffs
{
    public class ElecHostile : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Electrified");
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.Electric);
            npc.lifeRegen -= 30;
            npc.GetGlobalNPC<MyNPC>().electrified = true;
        }
    }
}

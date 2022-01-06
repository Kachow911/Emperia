using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Buffs
{
    public class ElecHostile : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electrified");
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(NPC NPC, ref int buffIndex)
        {
            Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Electric);
            NPC.lifeRegen -= 30;
            NPC.GetGlobalNPC<MyNPC>().electrified = true;
        }
    }
}

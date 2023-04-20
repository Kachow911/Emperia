using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;

namespace Emperia.Buffs
{
    public class CuttingLeaves : ModBuff
    {
        public override void SetStaticDefaults()
        {
           // DisplayName.SetDefault("Cutting Leaves");
			// Description.SetDefault("dps over time on god");         
            Main.debuff[Type] = true;   //Tells the game if this is a buf or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not. 
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;

        }

        public override void Update(NPC NPC, ref int buffIndex)
        {
            NPC.GetGlobalNPC<MyNPC>().cuttingLeaves = true;
        }
        

    }
}

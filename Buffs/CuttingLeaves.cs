using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;

namespace Emperia.Buffs
{
    public class CuttingLeaves : ModBuff
    {
        public override void SetDefaults()
        {
           DisplayName.SetDefault("Cutting Leaves");
			Description.SetDefault("dps over time on god");         
            Main.debuff[Type] = true;   //Tells the game if this is a buf or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not. 
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<MyNPC>().cuttingLeaves = true;
        }
        

    }
}

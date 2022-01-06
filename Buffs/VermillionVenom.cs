using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;

namespace Emperia.Buffs
{
    public class VermillionVenom : ModBuff
    {
        public override void SetStaticDefaults()
        {
           DisplayName.SetDefault("Vermillion Venom");
			Description.SetDefault("Decreased Contact Damage");         
            Main.debuff[Type] = true;   //Tells the game if this is a buf or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not. 
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC NPC, ref int buffIndex)
        {
            NPC.GetGlobalNPC<MyNPC>().vermillionVenom = true;
        }
        

    }
}

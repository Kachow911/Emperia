using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;

namespace Emperia.Buffs
{
    public class VermillionVenom : ModBuff
    {
        public override void SetDefaults()
        {
           DisplayName.SetDefault("Vermillion Venom");
			Description.SetDefault("Decreased Contact Damage");         
            Main.debuff[Type] = true;   //Tells the game if this is a buf or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not. 
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<MyNPC>(mod).vermillionVenom = true;
        }
        

    }
}

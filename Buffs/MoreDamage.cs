using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;

namespace Emperia.Buffs
{
    public class MoreDamage : ModBuff
    {
        public override void SetStaticDefaults()
        {
           // DisplayName.SetDefault("More Damage");
			// Description.SetDefault("Moar Damage");         
            Main.debuff[Type] = true;   //Tells the game if this is a buf or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not. 
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC NPC, ref int buffIndex)
        {
            NPC.GetGlobalNPC<MyNPC>().moreDamage = true;
        }
        

    }
}

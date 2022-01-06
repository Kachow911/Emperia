using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;

namespace Emperia.Buffs
{
    public class CrushingFreeze : ModBuff
    {
        bool init = false;
        int damag = 0;
        public override void SetStaticDefaults()
        {
           DisplayName.SetDefault("Crushing Freeze");
			Description.SetDefault("hhhh");         
            Main.debuff[Type] = true;   //Tells the game if this is a buf or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not. 
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC NPC, ref int buffIndex)
        {
            NPC.GetGlobalNPC<MyNPC>().crushFreeze = true;
            Dust.NewDust(NPC.position, NPC.width, NPC.height, 67);
        }
        

    }
}

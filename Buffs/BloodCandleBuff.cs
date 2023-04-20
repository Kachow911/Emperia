using Terraria;
using Terraria.ModLoader;

namespace Emperia.Buffs
{
    public class BloodCandleBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
           // DisplayName.SetDefault("Blood Candle");
			// Description.SetDefault("Greatly increased monster spawn rate");         
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}

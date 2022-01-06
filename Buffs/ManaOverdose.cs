using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Emperia.Buffs
{
    public class ManaOverdose : ModBuff
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Mana Overdose");
			Description.SetDefault("Cannot consume any more mana restoring items");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
    }
}
